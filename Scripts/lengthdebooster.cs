using Godot;
using System;

public partial class LengthDebooster : Gem
{
    // Define the signal using the [Signal] attribute
    [Signal]
    public delegate void LengthChangedEventHandler(float newLength);

    public override void _Ready()
    {
        // Add to the "length_changers" group
        AddToGroup("length_changers");
        AddToGroup("gems");
        GD.Print($"LengthChanger added to group: length_changers");
    }

    private void _on_Area2D_body_entered(Node body)
    {
        // Check if the entered body is the Paddle
        if (body is Paddle)
        {
            // Emit the LengthChanged signal with the new length (212.0f in this case)
            EmitSignal(nameof(LengthChanged), 53.0f);
        }
    }
}
