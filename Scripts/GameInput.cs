using Godot;
using System;

public partial class GameInput : Node {
    [Signal] public delegate void InteractActionPressedEventHandler();

	public override void _Input(InputEvent inputEvent)
	{
	    if (inputEvent.IsActionPressed("interact")) {
            EmitSignal(SignalName.InteractActionPressed);
		}
	}

	public Vector2 GetMovementVectorNormalized() {
		Vector2 inputVector = Input.GetVector("player_right", "player_left", "player_down", "player_up");

		return inputVector;
	}
}
