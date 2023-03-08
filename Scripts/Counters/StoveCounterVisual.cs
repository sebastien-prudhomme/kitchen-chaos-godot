using Godot;
using System;

public partial class StoveCounterVisual : Node3D {
    [Export] private StoveCounter stoveCounter;
    // [Export] private Node3D[] visualNodeArray;
    [Export] private Node3D visualNode;

    public override void _Ready() {
        stoveCounter.StateChanged += OnStateChanged;
    }

    private void OnStateChanged(StoveCounter.State state) {
        bool showVisual = (state == StoveCounter.State.Frying) || (state == StoveCounter.State.Fried);

        // foreach (Node3D visualNode in visualNodeArray) {
        //     visualNode.Visible = showVisual;
        // }
        visualNode.Visible = showVisual;
    }
}
