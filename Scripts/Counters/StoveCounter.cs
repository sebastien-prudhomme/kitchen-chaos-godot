using Godot;
using System;

public partial class StoveCounter : BaseCounter {
    [Export] private FryingRecipeResource[] fryingRecipeResourceArray;

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

    private bool HasRecipeWithInput(KitchenObjectResource inputKitchenObjectResource) {
        FryingRecipeResource fryingRecipeResource = GetFryingRecipeResourceWithInput(inputKitchenObjectResource);

        if (fryingRecipeResource != null) {
                return true;
        }

        return false;
    }

    private KitchenObjectResource GetOutputForInput(KitchenObjectResource inputKitchenObjectResource) {
        FryingRecipeResource fryingRecipeResource = GetFryingRecipeResourceWithInput(inputKitchenObjectResource);

        if (fryingRecipeResource != null) {
                return fryingRecipeResource.output;
        }

        return null;
    }

    private FryingRecipeResource GetFryingRecipeResourceWithInput(KitchenObjectResource inputKitchenObjectResource) {
        foreach (FryingRecipeResource fryingRecipeResource in fryingRecipeResourceArray) {
            if  (fryingRecipeResource.input == inputKitchenObjectResource) {
                return fryingRecipeResource;
            }
        }

        return null;
    }
}
