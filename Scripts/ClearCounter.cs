using Godot;
using System;

public partial class ClearCounter : Node3D, IKitchenObjectParent {
    [Export] private KitchenObjectResource kitchenObjectResource;
    [Export] private Node3D counterTopPoint;

    private KitchenObject kitchenObject;

    public void Interact(Player player) {
        if (kitchenObject == null) {
            KitchenObject kitchenObject = kitchenObjectResource.prefab.Instantiate<KitchenObject>();
            counterTopPoint.AddChild(kitchenObject);
            kitchenObject.SetKitchenObjectParent(this);
        } else {
            // Give the object to the player
            kitchenObject.SetKitchenObjectParent(player);
        }
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
