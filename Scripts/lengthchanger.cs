using Godot;
using System;

public partial class LengthChanger : Area2D
{
    // Define the signal using the [Signal] attribute
    [Signal]
    public delegate void LengthChangedEventHandler(float newLength);

    public override void _Ready()
    {
        // No additional setup needed for the signal
    }

    private void _on_Area2D_body_entered(Node body)
    {
        // Check if the entered body is the Paddle
        if (body is Paddle paddle)
        {
            // Emit the LengthChanged signal with the new length (212.0f in this case)
            EmitSignal(nameof(LengthChanged), 212.0f);
        }
    }
}
