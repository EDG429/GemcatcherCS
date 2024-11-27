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
		get => _speed; // Returns the current speed
		set => _speed = Math.Clamp(value, 250f, 1000.0f);
	}
		
	// Property to get and set length with clamping
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
		// Adjust the paddle's size based on its current length
		UpdatePaddleSize();

		// Connect to the LengthChanger's signal dynamically
		foreach (Node node in GetTree().GetNodesInGroup("length_changers")) // Need to add lengthchangers to groups in the IDE
		{
			if (node is LengthChanger lengthChanger)
			{
				 lengthChanger.Connect(nameof(LengthChanger.LengthChanged), new Callable(this, nameof(OnLengthChanged)));
			}
		}
	}

	// Signal handler for changing length
	private void OnLengthChanged(float newLength)
	{
    	Length = newLength; // Update the paddle's length
	}
	

	// Movement logic
	private void HandleMovement(double delta)
	{
		// Calculate the movement vector
		Vector2 movement = new Vector2(Speed * (float)delta, 0);

		// Get viewport bounds
		float ViewportSizeX = 1152.0f;
		float halfLength = Length / 2.0f;
		float minX = halfLength; // Minimum position on the left side edge of the screen
		float maxX = ViewportSizeX - halfLength;

		// Apply movement based on input
		if(Input.IsActionPressed("ui_right") == true && Position.X < maxX)
		{
			Position += movement;
		}
		if(Input.IsActionPressed("ui_left") == true && Position.X > minX)
		{
			Position -= movement;
		}

		// Clamp the paddle
		Position = new Vector2(Math.Clamp(Position.X, minX, maxX), Position.Y);
	}

	// Update paddle size logic
	private void UpdatePaddleSize()
	{
		if (GetNodeOrNull<Sprite2D>("Sprite2D") is Sprite2D sprite)
		{
			// Scale the paddle sprite along the x-axis to match the current length
			float defaultLength = 106.0f; // Original sprite length
			sprite.Scale = new Vector2(Length / defaultLength, sprite.Scale.Y);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		HandleMovement(delta); // Call movement logic		
	}

}
