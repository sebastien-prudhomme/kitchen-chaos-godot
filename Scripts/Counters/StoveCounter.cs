using Godot;
using System;

public partial class StoveCounter : BaseCounter {
    [Export] private FryingRecipeResource[] fryingRecipeResourceArray;
    [Export] private BurningRecipeResource[] burningRecipeResourceArray;

    private enum State {
        Idle,
        Frying,
        Fried,
        Burned
    }

    private State state;
    private float fryingTimer;
    private FryingRecipeResource fryingRecipeResource;
    private float burningTimer;
    private BurningRecipeResource burningRecipeResource;

    public override void _Ready() {
        state = State.Idle;
    }

	public override void _Process(double delta) {
        if (HasKitchenObject()) {
            switch (state) {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += (float)delta;

                    if (fryingTimer > fryingRecipeResource.fryingTimerMax) {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeResource.output, this);

                        state = State.Fried;
                        burningTimer = 0f;
                        burningRecipeResource = GetBurningRecipeResourceWithInput(GetKitchenObject().GetKitchenObjectResource());
                    }

                    break;
                case State.Fried:
                    burningTimer += (float)delta;

                    if (burningTimer > burningRecipeResource.burningTimerMax) {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeResource.output, this);

                        state = State.Burned;
                    }

                    break;
                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectResource())) {
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    state = State.Frying;
                    fryingTimer = 0f;
                    fryingRecipeResource = GetFryingRecipeResourceWithInput(GetKitchenObject().GetKitchenObjectResource());
                }
            }
        } else {
            if (!player.HasKitchenObject()) {
                GetKitchenObject().SetKitchenObjectParent(player);

                state = State.Idle;
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

    private BurningRecipeResource GetBurningRecipeResourceWithInput(KitchenObjectResource inputKitchenObjectResource) {
        foreach (BurningRecipeResource burningRecipeResource in burningRecipeResourceArray) {
            if  (burningRecipeResource.input == inputKitchenObjectResource) {
                return burningRecipeResource;
            }
        }

        return null;
    }
}
