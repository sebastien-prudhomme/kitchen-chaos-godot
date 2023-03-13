using Godot;
using System;
using System.Collections.Generic;

public partial class PlateKitchenObject : KitchenObject {
    [Signal] public delegate void IngredientAddedEventHandler(KitchenObjectResource kitchenObjectResource);

    [Export] Godot.Collections.Array<KitchenObjectResource> validKitchenObjectResourceArray;

    private Godot.Collections.Array<KitchenObjectResource> kitchenObjectResourceArray;

    public override void _EnterTree() {
        kitchenObjectResourceArray = new Godot.Collections.Array<KitchenObjectResource>();
    }

    public bool TryAddIngredient(KitchenObjectResource kitchenObjectResource) {
        if (!validKitchenObjectResourceArray.Contains(kitchenObjectResource)) {
            return false;
        }

        if (kitchenObjectResourceArray.Contains(kitchenObjectResource)) {
            return false;
        }

        kitchenObjectResourceArray.Add(kitchenObjectResource);

        EmitSignal(SignalName.IngredientAdded, kitchenObjectResource);

        return true;
    }
}
