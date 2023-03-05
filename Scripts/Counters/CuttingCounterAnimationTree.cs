using Godot;
using System;

public partial class CuttingCounterAnimationTree : AnimationTree {
    private const string CUTTING_COUNTER_CUT = "CuttingCounterCut";

    [Export] private CuttingCounter cuttingCounter;

    private AnimationNodeStateMachinePlayback stateMachine;

    public override void _EnterTree() {
        stateMachine = (AnimationNodeStateMachinePlayback)Get("parameters/playback");
    }

    public override void _Ready() {
        cuttingCounter.Cut += OnCut;
    }

    private void OnCut() {
        stateMachine.Travel(CUTTING_COUNTER_CUT);
    }
}
