using Godot;
using System;
using System.Numerics;

public partial class Game : Node2D
{	
	// Exported properties, IDE doesn't need the path
	[Export] private PackedScene gem;
	[Export] private PackedScene lengthboostergem;
	[Export] private PackedScene lengthdeboostergem;
	[Export] private PackedScene speedboostergem;
	[Export] private PackedScene speeddeboostergem;

	// Timer for spawning gems
	private Timer _spawnTimer;

	// Random number generator
	private RandomNumberGenerator _rng = new RandomNumberGenerator();
	
	public override void _Ready()
	{		
		// Initialize the random number generator
		_rng.Randomize();

		// Create and configure the timer
		_spawnTimer = new Timer();
		_spawnTimer.WaitTime = 2.5f;
		_spawnTimer.OneShot = false; // Repeating timer
		_spawnTimer.Connect("timeout", new Callable(this, nameof(SpawnRandomGem)));
		AddChild(_spawnTimer);

		// Start the timer
		_spawnTimer.Start();

	}

	private void SpawnRandomGem()
	{
		// Randomly select a gem type
		PackedScene selectedGemScene = GetRandomGemScene();

		// Spawn gem within the desired range
		float xPosition = _rng.RandfRange(10, 1140);
		Godot.Vector2 spawnPosition = new Godot.Vector2(xPosition, 0);

		// Instantiate the selected gem and add it to the scene
		if (selectedGemScene != null)
		{
			Node2D gemInstance = selectedGemScene.Instantiate<Node2D>();
			gemInstance.Position = spawnPosition;
			AddChild(gemInstance);
		}
	}

	private PackedScene GetRandomGemScene()
	{
		// Randomly select one of the 5 gem scenes
		int gemType = _rng.RandiRange(0, 4);
		switch (gemType)
		{
			case 0: return gem;
			case 1: return lengthboostergem;
			case 2: return lengthdeboostergem;
			case 3: return speedboostergem;
			case 4: return speeddeboostergem;
			default: return null;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
