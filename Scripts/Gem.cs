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
		
		
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AddToGroup("gems");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		HandleMovement(delta); // Call movement logic
	}
}
