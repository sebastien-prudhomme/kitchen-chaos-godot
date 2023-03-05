using Godot;
using System;

public partial class ContainerCounter : BaseCounter {
    [Signal] public delegate void PlayerGrabbedObjectEventHandler();

    [Export] private KitchenObjectResource kitchenObjectResource;

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            KitchenObject.SpawnKitchenObject(kitchenObjectResource, player);

            EmitSignal(SignalName.PlayerGrabbedObject);
        }
    }
}
