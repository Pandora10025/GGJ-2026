using UnityEngine;
using System;

public class MaskBuildState : MonoBehaviour
{
    public bool HasShape { get; private set; }
    public event Action<bool> OnHasShapeChanged;

    public void SetHasShape(bool value)
    {
        if (HasShape == value) return;
        HasShape = value;
        OnHasShapeChanged?.Invoke(HasShape);
    }
}
