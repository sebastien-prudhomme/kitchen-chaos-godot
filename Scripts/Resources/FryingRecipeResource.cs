using Godot;
using System;

public partial class FryingRecipeResource : Resource {
	[Export] public KitchenObjectResource input;
	[Export] public KitchenObjectResource output;
    [Export] public float fryingTimerMax;
}
