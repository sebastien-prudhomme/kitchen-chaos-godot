using Godot;
using System;

public partial class StoveCounter : BaseCounter {
    [Signal] public delegate void ProgressChangedEventHandler(float progressNormalized);
    [Signal] public delegate void StateChangedEventHandler(State state);

    [Export] private FryingRecipeResource[] fryingRecipeResourceArray;
    [Export] private BurningRecipeResource[] burningRecipeResourceArray;

    public enum State {
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

                    EmitSignal(SignalName.ProgressChanged, fryingTimer / fryingRecipeResource.fryingTimerMax);

                    if (fryingTimer > fryingRecipeResource.fryingTimerMax) {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeResource.output, this);

                        state = State.Fried;
                        burningTimer = 0f;
                        burningRecipeResource = GetBurningRecipeResourceWithInput(GetKitchenObject().GetKitchenObjectResource());

                        EmitSignal(SignalName.ProgressChanged, 0f);
                        EmitSignal(SignalName.StateChanged, (int)state);
                    }

                    break;
                case State.Fried:
                    burningTimer += (float)delta;

                    EmitSignal(SignalName.ProgressChanged, burningTimer / burningRecipeResource.burningTimerMax);

                    if (burningTimer > burningRecipeResource.burningTimerMax) {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeResource.output, this);

                        state = State.Burned;

                        EmitSignal(SignalName.ProgressChanged, 0f);
                        EmitSignal(SignalName.StateChanged, (int)state);
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

                    EmitSignal(SignalName.ProgressChanged, 0f);
                    EmitSignal(SignalName.StateChanged, (int)state);
                }
            }
        } else {
            if (player.HasKitchenObject()) {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectResource())) {
                        GetKitchenObject().DestroySelf();

                        state = State.Idle;

                        EmitSignal(SignalName.ProgressChanged, 0f);
                        EmitSignal(SignalName.StateChanged, (int)state);
                    }
                }
            } else {
                GetKitchenObject().SetKitchenObjectParent(player);

                state = State.Idle;

                EmitSignal(SignalName.ProgressChanged, 0f);
                EmitSignal(SignalName.StateChanged, (int)state);
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
