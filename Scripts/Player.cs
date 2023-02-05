using Godot;
using System;

public partial class Player : Node3D {
	[Export] private float moveSpeed = 7f;
    [Export] private GameInput gameInput;

    private bool isWalking;
	
	public override void _Process(double delta) {
		Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        if (inputVector.Length() > 0f) {
            Vector3 moveDir = new Vector3(inputVector.X, 0f, inputVector.Y);
            Position += moveDir * moveSpeed * (float)delta;

            float rotateSpeed = 10f;
            LookAt(Position - Basis.Z.Slerp(moveDir, (float)delta * rotateSpeed));

            isWalking = true;
        } else {
            isWalking = false;
        }
	}

    public bool IsWalking() {
        return isWalking;
    }
}
