using Godot;
using System;

public partial class PlatesCounter : BaseCounter {
    [Signal] public delegate void PlayerGrabbedObjectEventHandler();
    [Signal] public delegate void PlateSpawnedEventHandler();

    [Export] private KitchenObjectResource kitchenObjectResource;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int platesSpawnedAmount;
    private int platesSpawnedAmountMax = 4;

    public override void _Process(double delta) {
        spawnPlateTimer += (float)delta;

        if (spawnPlateTimer > spawnPlateTimerMax) {
            spawnPlateTimer = 0f;

            if (platesSpawnedAmount < platesSpawnedAmountMax) {
                platesSpawnedAmount++;

                EmitSignal(SignalName.PlateSpawned);
            }
        }
    }

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            if (platesSpawnedAmount > 0) {
                platesSpawnedAmount--;

                KitchenObject.SpawnKitchenObject(kitchenObjectResource, player);

                EmitSignal(SignalName.PlayerGrabbedObject);
            }
        }
    }
}
