using Godot;
using System;

public partial class ProgressBarUI : Node3D {
    [Export] private CuttingCounter cuttingCounter;
    [Export] private ProgressBar progressBar;

    public override void _Ready() {
        progressBar.Value = 0f;
        Hide();

        cuttingCounter.ProgressChanged += OnProgressChanged;
    }

    private void OnProgressChanged(float progressNormalized) {
        progressBar.Value = progressNormalized * progressBar.MaxValue;

        if (progressNormalized == 0f || progressNormalized == 1f) {
            Hide();
        } else {
            Show();
        }
    }
}
