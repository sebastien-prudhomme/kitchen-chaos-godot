using Godot;
using System;

public partial class CuttingCounter : BaseCounter {
    [Export] private CuttingRecipeResource[] cuttingRecipeResourceArray;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectResource())) {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            }
        } else {
            if (!player.HasKitchenObject()) {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player) {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectResource())) {
            KitchenObjectResource outputKitchenObjectResource = GetOutputForInput(GetKitchenObject().GetKitchenObjectResource());

            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(outputKitchenObjectResource, this);
        }
    }

    private bool HasRecipeWithInput(KitchenObjectResource inputKitchenObjectResource) {
        foreach (CuttingRecipeResource cuttingRecipeResource in cuttingRecipeResourceArray) {
            if  (cuttingRecipeResource.input == inputKitchenObjectResource) {
                return true;
            }
        }

        return false;
    }

    private KitchenObjectResource GetOutputForInput(KitchenObjectResource inputKitchenObjectResource) {
        foreach (CuttingRecipeResource cuttingRecipeResource in cuttingRecipeResourceArray) {
            if  (cuttingRecipeResource.input == inputKitchenObjectResource) {
                return cuttingRecipeResource.output;
            }
        }

        return null;
    }
}
