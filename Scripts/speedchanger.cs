using Godot;
using System;

public partial class SpeedChanger : Area2D
{
    // Define the signal using the [Signal] attribute
    [Signal]
    public delegate void SpeedChangedEventHandler(float newLength);

    public override void _Ready()
    {
        // Add to the "length_changers" group
        AddToGroup("speed_changers");
    }

    private void _on_Area2D_body_entered(Node body)
    {
        // Check if the entered body is the Paddle
        if (body is Paddle paddle)
        {
            // Emit the LengthChanged signal with the new length (212.0f in this case)
            EmitSignal(nameof(SpeedChanged), 212.0f);
        }
    }
}
