using Godot;
using System;

public partial class GameInput : Node {
	public Vector2 GetMovementVectorNormalized() {
		Vector2 inputVector = Input.GetVector("player_right", "player_left", "player_down", "player_up");

		return inputVector;
	}
}
