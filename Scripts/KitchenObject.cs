using Godot;
using System;

public partial class KitchenObject : Node {
    [Export(PropertyHint.File, "*.tres,")] private string kitchenObjectResourcePath; // Use path to prevent cyclic dependency with the prefab

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectResource GetKitchenObjectResource() {
        return GD.Load<KitchenObjectResource>(kitchenObjectResourcePath);
    }

    public IKitchenObjectParent GetKitchenObjectParent() {
        return kitchenObjectParent;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {
        if (kitchenObjectParent.HasKitchenObject()) {
            GD.PrintErr("IKitchenObjectParent already has a KitchenObject!");
        }

        if (this.kitchenObjectParent != null) {
            this.kitchenObjectParent.ClearKitchenObject();

            this.kitchenObjectParent.GetKitchenObjectFollowTransform().RemoveChild(this);
        }

        this.kitchenObjectParent = kitchenObjectParent;

        kitchenObjectParent.SetKitchenObject(this);

        this.kitchenObjectParent.GetKitchenObjectFollowTransform().AddChild(this);
    }

    public void DestroySelf() {
        if (kitchenObjectParent != null) {
            kitchenObjectParent.ClearKitchenObject();
        }

        QueueFree();
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectResource kitchenObjectResource, IKitchenObjectParent kitchenObjectParent) {
        KitchenObject kitchenObject = kitchenObjectResource.prefab.Instantiate<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }
}
