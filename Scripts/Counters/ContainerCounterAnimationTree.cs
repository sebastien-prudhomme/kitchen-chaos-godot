using Godot;
using System;

public partial class ContainerCounterAnimationTree : AnimationTree {
    private const string CONTAINER_OPEN_CLOSE = "ContainerOpenClose";

    [Export] private ContainerCounter containerCounter;

    private AnimationNodeStateMachinePlayback stateMachine;

    public override void _EnterTree() {
        stateMachine = (AnimationNodeStateMachinePlayback)Get("parameters/playback");
    }

    public override void _Ready() {
        containerCounter.PlayerGrabbedObject += OnPlayerGrabbedObject;
    }

    private void OnPlayerGrabbedObject() {
        stateMachine.Travel(CONTAINER_OPEN_CLOSE);
    }
}
