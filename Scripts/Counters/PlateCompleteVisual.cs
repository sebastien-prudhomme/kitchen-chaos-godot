using Godot;
using System;
using System.Collections.Generic;

public partial class PlateCompleteVisual : Node3D {
    [Export] private PlateKitchenObject plateKitchenObject;

    [Export] private KitchenObjectResource breadKitchenObjectResource;
    [Export] private KitchenObjectResource cabbageSlicesKitchenObjectResource;
    [Export] private KitchenObjectResource cheeseSlicesKitchenObjectResource;
    [Export] private KitchenObjectResource meatPattyBurnedKitchenObjectResource;
    [Export] private KitchenObjectResource meatPattyCookedKitchenObjectResource;
    [Export] private KitchenObjectResource tomatoSlicesKitchenObjectResource;

    [Export] private Node3D breadNode3D;
    [Export] private Node3D cabbageSlicesNode3D;
    [Export] private Node3D cheeseSlicesNode3D;
    [Export] private Node3D meatPattyBurnedNode3D;
    [Export] private Node3D meatPattyCookedNode3D;
    [Export] private Node3D tomatoSlicesNode3D;

    public override void _Ready() {
        plateKitchenObject.IngredientAdded += OnIngredientAdded;
    }

    private void OnIngredientAdded(KitchenObjectResource kitchenObjectResource) {
        if (kitchenObjectResource == breadKitchenObjectResource) {
            breadNode3D.Show();
        } else if (kitchenObjectResource == cabbageSlicesKitchenObjectResource) {
            cabbageSlicesNode3D.Show();
        } else if (kitchenObjectResource == cheeseSlicesKitchenObjectResource) {
            cheeseSlicesNode3D.Show();
        } else if (kitchenObjectResource == meatPattyBurnedKitchenObjectResource) {
            meatPattyBurnedNode3D.Show();
        } else if (kitchenObjectResource == meatPattyCookedKitchenObjectResource) {
            meatPattyCookedNode3D.Show();
        } else if (kitchenObjectResource == tomatoSlicesKitchenObjectResource) {
            tomatoSlicesNode3D.Show();
        }
    }
}
