using Godot;
using Godot.Collections;
using System;

public partial class Player : Node3D, IKitchenObjectParent {
    public static Player Instance { get; private set; }

    [Signal] public delegate void SelectedCounterChangedEventHandler(ClearCounter selectedCounter);

	[Export] private float moveSpeed = 7f;
    [Export] private GameInput gameInput;
    [Export] private ShapeCast3D shapeCast;
    [Export] private Node3D kitchenObjectHoldPoint;

    private bool isWalking;
    private Vector3 lastInteractDir;
    private ClearCounter selectedCounter;
    private KitchenObject kitchenObject;

    public override void _EnterTree() {
        if (Instance != null) {
            GD.PrintErr("There is more than one Player instance");
        }

        Instance = this;
    }

    public override void _Ready() {
        gameInput.InteractActionPressed += OnInteractActionPressed;
    }

    private void OnInteractActionPressed() {
        if (selectedCounter != null) {
            selectedCounter.Interact(this);
        }
    }

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
                ClearCounter clearCounter = (ClearCounter)colliderParent;

                if (selectedCounter != clearCounter) {
                    SetSelectedCounter(clearCounter);
                }
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
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

    private void SetSelectedCounter(ClearCounter selectedCounter) {
        this.selectedCounter = selectedCounter;

        EmitSignal(SignalName.SelectedCounterChanged, selectedCounter);
    }

    public Node3D GetKitchenObjectFollowTransform() {
        return kitchenObjectHoldPoint;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}
