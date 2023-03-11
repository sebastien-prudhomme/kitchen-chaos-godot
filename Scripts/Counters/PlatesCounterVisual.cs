using Godot;
using System;
using System.Collections.Generic;

public partial class PlatesCounterVisual : BaseCounter {
    [Export] private PlatesCounter platesCounter;
    [Export] private Node3D counterTopPoint;
    [Export] private PackedScene plateVisualPrefab;

    private List<Node3D> plateVisualList;

    public override void _EnterTree() {
        plateVisualList = new List<Node3D>();
    }

    public override void _Ready() {
        platesCounter.PlayerGrabbedObject += OnPlayerGrabbedObject;
        platesCounter.PlateSpawned += OnPlateSpawned;
    }

    private void OnPlayerGrabbedObject() {
        Node3D plateVisual = plateVisualList[plateVisualList.Count - 1];

        plateVisualList.Remove(plateVisual);
        plateVisual.QueueFree();
    }

    private void OnPlateSpawned() {
        Node3D plateVisual = plateVisualPrefab.Instantiate<Node3D>();
        counterTopPoint.AddChild(plateVisual);

        float plateOffsetY = 0.1f;
        plateVisual.Position = new Vector3(0, plateOffsetY * plateVisualList.Count, 0);

        plateVisualList.Add(plateVisual);
    }
}
