using Godot;
using System;

public partial class BurningRecipeResource : Resource {
	[Export] public KitchenObjectResource input;
	[Export] public KitchenObjectResource output;
    [Export] public float burningTimerMax;
}
