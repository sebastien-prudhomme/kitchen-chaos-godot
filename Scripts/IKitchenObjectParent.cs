using Godot;
using System;

public interface IKitchenObjectParent {
    public Node3D GetKitchenObjectFollowTransform();

    public KitchenObject GetKitchenObject();

    public void SetKitchenObject(KitchenObject kitchenObject);

    public void ClearKitchenObject();

    public bool HasKitchenObject();
}
