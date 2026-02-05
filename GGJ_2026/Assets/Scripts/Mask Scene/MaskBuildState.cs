using System;
using System.Collections.Generic;
using UnityEngine;

public class MaskBuildState : MonoBehaviour
{
    // -1 means no shape selected yet
    public int shapeIndex = -1;

    // Gating API (CategoryGate expects this)
    public bool HasShape => shapeIndex >= 0;
    public event Action<bool> OnHasShapeChanged;

    private readonly List<PlacedItemInstance> placed = new List<PlacedItemInstance>();

    /// <summary>
    /// Set the current selected shape index.
    /// Triggers HasShape change event if we cross the -1 boundary.
    /// </summary>
    public void SetShapeIndex(int newIndex)
    {
        bool hadShapeBefore = HasShape;

        shapeIndex = newIndex;

        bool hasShapeNow = HasShape;
        if (hadShapeBefore != hasShapeNow)
        {
            OnHasShapeChanged?.Invoke(hasShapeNow);
        }
    }

    /// <summary>
    /// Clears the selected shape (locks categories again).
    /// </summary>
    public void ClearShape()
    {
        SetShapeIndex(-1);
    }

    public void RegisterPlaced(PlacedItemInstance inst)
    {
        if (inst == null) return;
        if (!placed.Contains(inst)) placed.Add(inst);
    }

    public void ClearAllPlaced()
    {
        for (int i = placed.Count - 1; i >= 0; i--)
        {
            if (placed[i] != null) Destroy(placed[i].gameObject);
        }              
        placed.Clear();
    }

    public MaskData ToMaskData()
    {
        var data = new MaskData();
        data.shapeIndex = shapeIndex;

        foreach (var inst in placed)
        {
            if (inst == null) continue;

            var t = inst.transform;

            var entry = new PlacedItemData
            {
                itemId = inst.itemId,
                localPosition = Vec3.From(t.localPosition),
                localRotationZ = t.localEulerAngles.z,
                localScale = Vec3.From(t.localScale),
                tint = Col.From(inst.SpriteRenderer != null ? inst.SpriteRenderer.color : Color.white),
                sortingLayer = inst.SpriteRenderer != null ? inst.SpriteRenderer.sortingLayerName : "Default",
                orderInLayer = inst.SpriteRenderer != null ? inst.SpriteRenderer.sortingOrder : 0
            };

            data.placedItems.Add(entry);
        }

        return data;
    }
}
