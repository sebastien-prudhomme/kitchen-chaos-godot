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
        }

        this.kitchenObjectParent = kitchenObjectParent;

        kitchenObjectParent.SetKitchenObject(this);

        Reparent(kitchenObjectParent.GetKitchenObjectFollowTransform(), false);
    }
}
