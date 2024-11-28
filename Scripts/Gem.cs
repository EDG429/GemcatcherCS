using Godot;
using System;

public partial class Gem : Area2D
{	
	// Signal
	[Signal] public delegate void OnGemOffScreenEventHandler();

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
		CheckHitBottom();		
	}

	private void CheckHitBottom()
	{
		if (Position.Y > 675)
		{	
			EmitSignal(SignalName.OnGemOffScreen);						
			QueueFree();
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Add this gem to the "gems" group
		AddToGroup("gems");

		// Connect the "area_entered" signal
		Connect("area_entered", new Callable(this, nameof(OnAreaEntered)));
	}

	// Handles collision with the paddle
	private void OnAreaEntered(Node area)
	{
		if (area is Paddle)
		{
			// Emit an event or let the paddle handle logic
			QueueFree(); // Remove the gem when it touches the paddle
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		HandleMovement(delta); // Call movement logic
	}
}
