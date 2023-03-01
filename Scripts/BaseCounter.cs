using Godot;
using System;

public partial class BaseCounter : Node3D, IKitchenObjectParent {
    [Export] private Node3D counterTopPoint;

    private KitchenObject kitchenObject;

    public virtual void Interact(Player player) {
        GD.PrintErr("BaseCounter.Interact()");
    }

    public virtual void InteractAlternate(Player player) {
        GD.PrintErr("BaseCounter.InteractAlternate()");
    }

    public Node3D GetKitchenObjectFollowTransform() {
        return counterTopPoint;
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
