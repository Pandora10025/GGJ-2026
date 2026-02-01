using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Vec3
{
    public float x, y, z;
    public Vec3(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }
    public static Vec3 From(Vector3 v) => new Vec3(v.x, v.y, v.z);
    public Vector3 ToUnity() => new Vector3(x, y, z);
}

[Serializable]
public struct Col
{
    public float r, g, b, a;
    public Col(float r, float g, float b, float a) { this.r = r; this.g = g; this.b = b; this.a = a; }
    public static Col From(Color c) => new Col(c.r, c.g, c.b, c.a);
    public Color ToUnity() => new Color(r, g, b, a);
}

[Serializable]
public class PlacedItemData
{
    public string itemId;

    public Vec3 localPosition;
    public float localRotationZ;  // 2D rotation
    public Vec3 localScale;

    public Col tint;              // per-item color customization (optional)
    public string sortingLayer;
    public int orderInLayer;
}

[Serializable]
public class MaskData
{
    public int shapeIndex;
    public List<PlacedItemData> placedItems = new List<PlacedItemData>();
}
