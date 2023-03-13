using Godot;
using System;
using System.Collections.Generic;

public partial class PlatesCounterVisual : BaseCounter {
    [Export] private PlatesCounter platesCounter;
    [Export] private Node3D counterTopPoint;
    [Export] private PackedScene plateVisualPrefab;

    private Godot.Collections.Array<Node3D> plateVisualArray;

    public override void _EnterTree() {
        plateVisualArray = new Godot.Collections.Array<Node3D>();
    }

    public override void _Ready() {
        platesCounter.PlayerGrabbedObject += OnPlayerGrabbedObject;
        platesCounter.PlateSpawned += OnPlateSpawned;
    }

    private void OnPlayerGrabbedObject() {
        Node3D plateVisual = plateVisualArray[plateVisualArray.Count - 1];

        plateVisualArray.Remove(plateVisual);
        plateVisual.QueueFree();
    }

    private void OnPlateSpawned() {
        Node3D plateVisual = plateVisualPrefab.Instantiate<Node3D>();
        counterTopPoint.AddChild(plateVisual);

        float plateOffsetY = 0.1f;
        plateVisual.Position = new Vector3(0, plateOffsetY * plateVisualArray.Count, 0);

        plateVisualArray.Add(plateVisual);
    }
}
