using System.Collections.Generic;
using UnityEngine;

public static class MaskItemRegistry
{
    private static Dictionary<string, MaskItemData> byId;

    public static void Warm()
    {
        if (byId != null) return;

        byId = new Dictionary<string, MaskItemData>();
        var all = Resources.LoadAll<MaskItemData>("MaskItems"); // Resources/MaskItems/*
        foreach (var item in all)
        {
            if (item == null || string.IsNullOrWhiteSpace(item.itemId)) continue;
            byId[item.itemId] = item;
        }
    }

    public static MaskItemData Get(string itemId)
    {
        Warm();
        byId.TryGetValue(itemId, out var data);
        return data;
    }
}
