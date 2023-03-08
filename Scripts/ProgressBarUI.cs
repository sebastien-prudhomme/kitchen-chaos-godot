using Godot;
using System;

public partial class ProgressBarUI : Node3D {
    [Export] private Node hasProgressNode;
    [Export] private ProgressBar progressBar;

    private const string PROGRESS_CHANGED = "ProgressChanged";

    public override void _Ready() {
        progressBar.Value = 0f;
        Hide();

        if (!hasProgressNode.HasSignal(PROGRESS_CHANGED)) {
            GD.PrintErr("Node " + hasProgressNode + " doesn't have a signal " + PROGRESS_CHANGED + "!");
        }

        hasProgressNode.Connect(PROGRESS_CHANGED, new Callable(this, MethodName.OnProgressChanged));
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
