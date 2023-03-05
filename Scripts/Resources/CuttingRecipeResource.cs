using Godot;
using System;

public partial class CuttingRecipeResource : Resource {
	[Export] public KitchenObjectResource input;
	[Export] public KitchenObjectResource output;
    [Export] public int cuttingProgressMax;
}
