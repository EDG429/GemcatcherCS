using Godot;
using System;

public partial class Gem : Area2D
{	
	// Gem variables
	private float _speed = 300.0f;

	// Movement logic
	private void HandleMovement(double delta)
	{
		// Calculate the movement vector
		Vector2 movement = new Vector2(0, _speed * (float)delta);

		// Move the gem
		Position += movement;
		
		// Check if the gem goes beyond the Y-axis threshold (675)
		if (Position.Y > 675)
		{
			QueueFree();
		}
		
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AddToGroup("gems");
		
		// Connect the "body_entered" signal dynamically
		Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
	}

	// Handles paddle collision
	private void OnBodyEntered(Node body)
	{
		if (body is Paddle)
		{
			QueueFree();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		HandleMovement(delta); // Call movement logic
	}
}
