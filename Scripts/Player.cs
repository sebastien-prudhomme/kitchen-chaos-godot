using Godot;
using System;

public partial class Player : Node3D {
	[Export]
    private float moveSpeed = 7f;

    private bool isWalking;
	
	public override void _Process(double delta) {
		Vector2 inputVector = new Vector2(0f ,0f);

        if (Input.IsKeyPressed(Key.Z)) {
            inputVector.Y = 1f;
        }

        if (Input.IsKeyPressed(Key.S)) {
            inputVector.Y = -1f;
        }
        
        if (Input.IsKeyPressed(Key.Q)) {
            inputVector.X = 1f;
        }

        if (Input.IsKeyPressed(Key.D)){
            inputVector.X = -1f;
        }        

        if (inputVector.Length() > 0f) {
            inputVector = inputVector.Normalized();

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
