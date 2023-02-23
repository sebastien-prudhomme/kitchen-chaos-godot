using Godot;
using System;

public partial class ContainerCounterAnimationTree : AnimationTree {
    private const string OPEN_CLOSE = "OpenClose";

    [Export] private ContainerCounter containerCounter;

    // private Animator animator;

    // private void Awake() {
    //     animator = GetComponent<Animator>();
    // }

    // private void Start() {
    //     containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    // }

    // private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e) {
    //     animator.SetTrigger(OPEN_CLOSE);
    // }
}
