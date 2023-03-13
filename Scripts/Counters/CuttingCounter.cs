using Godot;
using System;

public partial class CuttingCounter : BaseCounter {
    [Signal] public delegate void CutEventHandler();
    [Signal] public delegate void ProgressChangedEventHandler(float progressNormalized);

    [Export] private CuttingRecipeResource[] cuttingRecipeResourceArray;

    private int cuttingProgress;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectResource())) {
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    cuttingProgress = 0;

                    EmitSignal(SignalName.ProgressChanged, 0f);
                }
            }
        } else {
            if (player.HasKitchenObject()) {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectResource())) {
                        GetKitchenObject().DestroySelf();
                    }
                }
            } else {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player) {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectResource())) {
            EmitSignal(SignalName.Cut);

            cuttingProgress++;

            CuttingRecipeResource cuttingRecipeResource = GetCuttingRecipeResourceWithInput(GetKitchenObject().GetKitchenObjectResource());

            EmitSignal(SignalName.ProgressChanged, (float)cuttingProgress / cuttingRecipeResource.cuttingProgressMax);

            if (cuttingProgress >= cuttingRecipeResource.cuttingProgressMax) {
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(cuttingRecipeResource.output, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectResource inputKitchenObjectResource) {
        CuttingRecipeResource cuttingRecipeResource = GetCuttingRecipeResourceWithInput(inputKitchenObjectResource);

        if (cuttingRecipeResource != null) {
                return true;
        }

        return false;
    }

    private KitchenObjectResource GetOutputForInput(KitchenObjectResource inputKitchenObjectResource) {
        CuttingRecipeResource cuttingRecipeResource = GetCuttingRecipeResourceWithInput(inputKitchenObjectResource);

        if (cuttingRecipeResource != null) {
                return cuttingRecipeResource.output;
        }

        return null;
    }

    private CuttingRecipeResource GetCuttingRecipeResourceWithInput(KitchenObjectResource inputKitchenObjectResource) {
        foreach (CuttingRecipeResource cuttingRecipeResource in cuttingRecipeResourceArray) {
            if  (cuttingRecipeResource.input == inputKitchenObjectResource) {
                return cuttingRecipeResource;
            }
        }

        return null;
    }
}
