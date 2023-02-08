using Godot;
using Godot.Collections;
using System;

public partial class Player : Node3D {
	[Export] private float moveSpeed = 7f;
    [Export] private GameInput gameInput;
    [Export] private ShapeCast3D shapeCast;

    private bool isWalking;
    private Vector3 lastInteractDir;

	public override void _PhysicsProcess(double delta) {
        HandleMovement(delta);
        HandleInteractions();
	}

	public void HandleInteractions() {
		Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.X, 0f, inputVector.Y);

        if (moveDir != Vector3.Zero) {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;

        PhysicsRayQueryParameters3D query = new PhysicsRayQueryParameters3D();
        query.From = GlobalPosition;
        query.To = GlobalPosition + (lastInteractDir * interactDistance);
        query.CollisionMask = 0x00000001; // Counters

        Dictionary intersection = GetWorld3D().DirectSpaceState.IntersectRay(query);

        if (intersection.Count > 0) {
            Node collider = (Node)intersection["collider"];
            Node colliderParent = collider.GetParent();

            if (colliderParent.HasMethod("Interact")) {
                colliderParent.Call("Interact");
            }
        }
    }

	public void HandleMovement(double delta) {
		Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        if (inputVector.Length() > 0f) {
            Vector3 moveDir = new Vector3(inputVector.X, 0f, inputVector.Y);

            float moveDistance = moveSpeed * (float)delta;

            // Problème à faire fonctionner en tant qu'enfant
            shapeCast.GlobalPosition = GlobalPosition + Vector3.Up;
            shapeCast.TargetPosition = moveDir * moveDistance;
            shapeCast.ForceShapecastUpdate();

            bool canMove = !shapeCast.IsColliding();

            if (!canMove) {
                Vector3 moveDirX = new Vector3(moveDir.X, 0, 0).Normalized();
                shapeCast.TargetPosition = moveDirX * moveDistance;
                shapeCast.ForceShapecastUpdate();

                canMove = !shapeCast.IsColliding();

                if (canMove) {
                    moveDir = moveDirX;
                } else {
                    Vector3 moveDirZ = new Vector3(0, 0, moveDir.Z).Normalized();
                    shapeCast.TargetPosition = moveDirZ * moveDistance;
                    shapeCast.ForceShapecastUpdate();

                    canMove = !shapeCast.IsColliding();

                    if (canMove) {
                        moveDir = moveDirZ;
                    }
                }
            }

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
