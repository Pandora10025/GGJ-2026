using UnityEngine;

[CreateAssetMenu(menuName = "Mask/Mask Item", fileName = "MaskItem_")]
public class MaskItemData : ScriptableObject
{
    [Header("Identity")]
    public string itemId;            // unique, e.g. "feather_red_01"
    public MaskCategory category;

    [Header("UI")]
    public Sprite icon;

    [Header("Prefab to spawn")]
    public GameObject prefab2D;      // should include SpriteRenderer/UI, etc.

    [Header("Repo modifiers (optional for now)")]

    [Range(-2, 2)] public int refinement;
    [Range(-2, 2)] public int era;
    [Range(-2, 2)] public int ornamentation;
    [Range(-2, 2)] public int presence;
}
