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

	// Reference to the ScoreLabel and score variable
	private Label _scoreLabel;
	private int _score = 0;

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

		// Get reference to the ScoreLabel node
		_scoreLabel = GetNode<Label>("ScoreLabel");

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
			Gem gemInstance = selectedGemScene.Instantiate<Gem>();
			gemInstance.Position = spawnPosition;
			AddChild(gemInstance);
			gemInstance.OnGemOffScreen += GameOver;
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

	private void OnScored()
	{
		_score++;
		UpdateScoreLabel();
	}

	private void UpdateScoreLabel()
	{
		if (_scoreLabel != null)
		{
			_scoreLabel.Text = $"Score = {_score}";
		}
	}

	private void GameOver()
	{
		Global global = (Global)GetNode("/root/Global");
		global.PlayerScore = _score;
		foreach (Node child in GetChildren())
		{
			child.QueueFree();
		}
		GetTree().ChangeSceneToFile("res://Scenes/GameOverScreen.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
