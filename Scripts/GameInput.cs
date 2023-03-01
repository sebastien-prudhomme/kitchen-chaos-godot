using Godot;
using System;

public partial class GameInput : Node {
    [Signal] public delegate void InteractActionPressedEventHandler();
    [Signal] public delegate void InteractAlternateActionPressedEventHandler();

	public override void _Input(InputEvent inputEvent)
	{
	    if (inputEvent.IsActionPressed("interact")) {
            EmitSignal(SignalName.InteractActionPressed);
		}

	    if (inputEvent.IsActionPressed("interact_alternate")) {
            EmitSignal(SignalName.InteractAlternateActionPressed);
		}
	}

	public Vector2 GetMovementVectorNormalized() {
		Vector2 inputVector = Input.GetVector("player_right", "player_left", "player_down", "player_up");

		return inputVector;
	}
}
