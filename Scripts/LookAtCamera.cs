using Godot;
using System;

public partial class LookAtCamera : Node3D {
    private enum Mode {
        CameraForward,
        CameraForwardInverted,
        LookAt,
        LookAtInverted
    }

    [Export] Mode mode;

    public override void _EnterTree() {
        // Late process
        ProcessPriority = 1;
    }

    public override void _Process(double delta) {
        Camera3D camera = GetViewport().GetCamera3D();

        switch (mode) {
            case Mode.CameraForward:
                LookAt(GlobalPosition + camera.GlobalTransform.Basis.Z);
                break;
            case Mode.CameraForwardInverted:
                LookAt(GlobalPosition - camera.GlobalTransform.Basis.Z);
                break;
            case Mode.LookAt:
                LookAt(camera.GlobalPosition);
                break;
            case Mode.LookAtInverted:
                Vector3 dirFromCamera = GlobalPosition - camera.GlobalPosition;
                LookAt(GlobalPosition + dirFromCamera);
                break;
        }
    }
}
