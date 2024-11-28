using Godot;
using System;

public partial class GameOverScreen : Control
{
    // Reference to the score label
    private Label _scoreLabel;

    public override void _Ready()
    {
        // Get the Score label
        _scoreLabel = GetNode<Label>("MarginContainer/VBoxContainer/Score");

		// Get the score from the Global singleton
		Global global = (Global)GetNode("/root/Global");
		int score = global.PlayerScore;

		// Update the score label
		if (_scoreLabel != null)
		{
			_scoreLabel.Text = $"Your Score = {score}";
		}

        // Connect button signals
        Button restartButton = GetNode<Button>("MarginContainer/VBoxContainer/HBoxContainer/RestartGame");
        Button quitButton = GetNode<Button>("MarginContainer/VBoxContainer/HBoxContainer/QuitGame");

        restartButton.Pressed += OnRestartGamePressed;
        quitButton.Pressed += OnQuitGamePressed;
    }

    // Sets the score on the Game Over screen
    public void SetScore(int score)
    {
        if (_scoreLabel != null)
        {
            _scoreLabel.Text = $"Your Score: {score}";
        }
    }

    // Restart the game
    private void OnRestartGamePressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/game.tscn");
    }

    // Quit the game
    private void OnQuitGamePressed()
    {
        GetTree().Quit();
    }
}
