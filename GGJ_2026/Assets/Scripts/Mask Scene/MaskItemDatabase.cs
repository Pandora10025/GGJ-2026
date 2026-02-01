using System.Collections.Generic;
using UnityEngine;

public static class MaskItemDatabase
{
    private static Dictionary<MaskCategory, List<MaskItemData>> byCategory;
    private static Dictionary<string, MaskItemData> byId;

    public static void Warm()
    {
        if (byCategory != null && byId != null) return;

        byCategory = new Dictionary<MaskCategory, List<MaskItemData>>();
        byId = new Dictionary<string, MaskItemData>();

        var all = Resources.LoadAll<MaskItemData>("MaskItems");
        Debug.Log($"[MaskItemDatabase] Loaded {all.Length} MaskItemData assets from Resources/MaskItems");

        foreach (var item in all)
        {
            if (item == null)
            {
                Debug.LogWarning("[MaskItemDatabase] Found null item asset reference");
                continue;
            }

            if (string.IsNullOrWhiteSpace(item.itemId))
            {
                Debug.LogWarning($"[MaskItemDatabase] Skipping item with EMPTY itemId: {item.name}");
                continue;
            }

            byId[item.itemId] = item;

            if (!byCategory.TryGetValue(item.category, out var list))
            {
                list = new List<MaskItemData>();
                byCategory[item.category] = list;
            }
            list.Add(item);
        }
    }

    public static IReadOnlyList<MaskItemData> GetByCategory(MaskCategory category)
    {
        Warm();
        return byCategory.TryGetValue(category, out var list) ? list : (IReadOnlyList<MaskItemData>)System.Array.Empty<MaskItemData>();
    }

    public static MaskItemData GetById(string itemId)
    {
        Warm();
        byId.TryGetValue(itemId, out var item);
        return item;
    }
}
