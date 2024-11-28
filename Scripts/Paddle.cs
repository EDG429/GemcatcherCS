using Godot;
using System;

public partial class Paddle : Area2D
{
	// Paddle variables
	private float _speed = 500.0f; // Default speed
	private float _length = 106.0f; // Default length of the paddle

	// Property to get and set speed with clamping
	public float Speed
	{
		get => _speed; 
		set 
		{
			_speed = Math.Clamp(value, 250f, 1000.0f);
			UpdatePaddleSpeed();
		} 
	}

	public float Length
	{
		get => _length; 
		set 
		{
			_length = Math.Clamp(value, 53.0f, 212.0f);
			UpdatePaddleSize();
		} 
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		UpdatePaddleSize();
		UpdatePaddleSpeed();

		// Connect to the "area_entered" signal dynamically
		Connect("area_entered", new Callable(this, nameof(OnAreaEntered)));
	}

	// Handle gem collision
	private void OnAreaEntered(Node area)
	{ 
		// Check if the area is a Gem
		if (area is Gem gem)
		{
			// Handle different types of gems
			if (gem is LengthBooster)
			{
				// Increase the paddle's length
				Length = 212.0f;
			}
			else if (gem is LengthDebooster)
			{
				// Decrease the paddle's length
				Length = 53.0f;
			}
			else if (gem is SpeedBooster)
			{
				// Increase the paddle's speed
				Speed = 1000.0f;
			}
			else if (gem is SpeedDebooster)
			{
				// Decrease the paddle's speed
				Speed = 250.0f;
			}

			// Free the gem after applying its effect
			gem.QueueFree();
		}
	}

	// Logic to update the paddle's speed
	private void UpdatePaddleSpeed()
	{
		var boostSound = GetNode<AudioStreamPlayer2D>("SpeedorLengthBoostSound");
		var decreaseSound = GetNode<AudioStreamPlayer2D>("SpeedorLengthDecreaseSound");

		if (_speed >= 750.0f)
		{
			if (!boostSound.Playing)
			{
				boostSound.Play();
			}
		}
		else if (_speed <= 350.0f)
		{
			if (!decreaseSound.Playing)
			{
				decreaseSound.Play();
			}
		}
	}

	// Logic to update the paddle's size
	private void UpdatePaddleSize()
	{
		if (GetNodeOrNull<Sprite2D>("Sprite2D") is Sprite2D sprite && GetNodeOrNull<CollisionShape2D>("CollisionShape2D") is CollisionShape2D collisionShape)
		{
			float defaultLength = 106.0f;
			sprite.Scale = new Vector2(Length / defaultLength, sprite.Scale.Y);

			// Ensure the CollisionShape matches the sprite's size pixel-perfectly
			if (collisionShape.Shape is RectangleShape2D rectangleShape && sprite.Texture != null)
			{
				// Get the original size of the sprite's texture in pixels
				Vector2 textureSize = sprite.Texture.GetSize();

				// Calculate the scaled size of the sprite
				float scaledWidth = textureSize.X * sprite.Scale.X;
				float scaledHeight = textureSize.Y * sprite.Scale.Y;

				// Update the CollisionShape2D to match the scaled size
				rectangleShape.Size = new Vector2(scaledWidth, scaledHeight);
			}	
			
		}
	}

	public override void _Process(double delta)
	{
		Vector2 movement = new Vector2(Speed * (float)delta, 0);

		float ViewportSizeX = 1152.0f;
		float halfLength = Length / 2.0f;
		float minX = halfLength;
		float maxX = ViewportSizeX - halfLength;

		if (Input.IsActionPressed("ui_right") && Position.X < maxX)
		{
			Position += movement;
		}
		if (Input.IsActionPressed("ui_left") && Position.X > minX)
		{
			Position -= movement;
		}

		Position = new Vector2(Math.Clamp(Position.X, minX, maxX), Position.Y);
	}
}