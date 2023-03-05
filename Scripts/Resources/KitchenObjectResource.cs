using Godot;
using System;

public partial class KitchenObjectResource : Resource {
	[Export] public PackedScene prefab;
    [Export] public Texture texture;
    [Export] public string objectName;
}
