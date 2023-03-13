using Godot;
using System;

public partial class ClearCounter : BaseCounter {
    [Export] private KitchenObjectResource kitchenObjectResource;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        } else {
            if (player.HasKitchenObject()) {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectResource())) {
                        GetKitchenObject().DestroySelf();
                    }
                } else {
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectResource())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            } else {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
