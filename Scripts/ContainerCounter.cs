using Godot;
using System;

public partial class ContainerCounter : BaseCounter {
    [Signal] public delegate void PlayerGrabbedObjectEventHandler();

    [Export] private KitchenObjectResource kitchenObjectResource;

    public override void Interact(Player player) {
        KitchenObject kitchenObject = kitchenObjectResource.prefab.Instantiate<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(player);

        EmitSignal(SignalName.PlayerGrabbedObject);
    }
}
