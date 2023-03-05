using Godot;
using System;

public partial class SelectedCounterVisual : Node3D {
    [Export] private BaseCounter baseCounter;
    // [Export] private Node3D[] visualNodeArray;
    [Export] private Node3D visualNode;

    public override void _Ready() {
        Player.Instance.SelectedCounterChanged += OnSelectedCounterChanged;
    }

    private void OnSelectedCounterChanged(BaseCounter selectedCounter) {
        if (selectedCounter == baseCounter) {
            ShowVisual();
        } else {
            HideVisual();
        }
    }

    private void HideVisual() {
        // foreach (Node3D visualNode in visualNodeArray) {
        //     visualNode.Hide();
        // }
        visualNode.Hide();
    }

    private void ShowVisual() {
        // foreach (Node3D visualNode in visualNodeArray) {
        //     visualNode.Show();
        // }
        visualNode.Show();
    }
}
