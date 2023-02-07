using Godot;
using System;

public partial class Player : Node3D {
	[Export] private float moveSpeed = 7f;
    [Export] private GameInput gameInput;
    [Export] private ShapeCast3D shapeCast;

    private bool isWalking;

	public override void _PhysicsProcess(double delta) {
		Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        if (inputVector.Length() > 0f) {
            Vector3 moveDir = new Vector3(inputVector.X, 0f, inputVector.Y);

            float moveDistance = moveSpeed * (float)delta;

            // Problème à faire fonctionner en tant qu'enfant
            shapeCast.GlobalPosition = GlobalPosition + Vector3.Up;
            shapeCast.TargetPosition = moveDir * moveDistance;
            shapeCast.ForceShapecastUpdate();

            bool canMove = !shapeCast.IsColliding();

            if (canMove) {
                Position += moveDir * moveDistance;
            }

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
