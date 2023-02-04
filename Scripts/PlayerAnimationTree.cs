using Godot;
using System;

public partial class PlayerAnimationTree : AnimationTree {
	[Export]
	private Player player;

	[Export]
    private bool isWalking;

	public override void _Process(double delta) {
		isWalking = player.IsWalking();
	}
}
