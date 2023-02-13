using Godot;
using System;

public partial class SelectedCounterVisual : Node3D {
    [Export] private ClearCounter clearCounter;
    [Export] private Node3D visualNode;

    public override void _Ready() {
        Player.Instance.SelectedCounterChanged += OnSelectedCounterChanged;
    }

    private void OnSelectedCounterChanged(ClearCounter selectedCounter) {
        if (selectedCounter == clearCounter) {
            ShowVisual();
        } else {
            HideVisual();
        }
    }

    private void HideVisual() {
        visualNode.Hide();
    }

    private void ShowVisual() {
        visualNode.Show();
    }
}
