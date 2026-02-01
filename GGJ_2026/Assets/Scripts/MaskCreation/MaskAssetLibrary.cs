using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mask Maker/Mask Asset Library", fileName = "MaskAssetLibrary")]
public class MaskAssetLibrary : ScriptableObject
{
    [Header("Shape")]
    public List<MaskSpriteEntry> shapes = new();

    [Header("Pattern")]
    public List<MaskSpriteEntry> patterns = new();

    [Header("Trim")]
    public List<MaskSpriteEntry> trims = new();

    [Header("Other Categories")]
    public List<MaskSpriteEntry> gilded = new();
    public List<MaskSpriteEntry> lace = new();
    public List<MaskSpriteEntry> feathers = new();
    public List<MaskSpriteEntry> furs = new();
    public List<MaskSpriteEntry> accessories = new();
    public List<MaskSpriteEntry> flowers = new();

    Dictionary<string, Sprite> _shapeMap;
    Dictionary<string, Sprite> _patternMap;
    Dictionary<string, Sprite> _trimMap;

    Dictionary<string, Sprite> _gildedMap;
    Dictionary<string, Sprite> _laceMap;
    Dictionary<string, Sprite> _feathersMap;
    Dictionary<string, Sprite> _fursMap;
    Dictionary<string, Sprite> _accessoriesMap;
    Dictionary<string, Sprite> _flowersMap;

    void OnEnable()
    {
        // Build maps when the asset loads (Editor + Runtime)
        _shapeMap = BuildMap(shapes, "shapes");
        _patternMap = BuildMap(patterns, "patterns");
        _trimMap = BuildMap(trims, "trims");

        _gildedMap = BuildMap(gilded, "gilded");
        _laceMap = BuildMap(lace, "lace");
        _feathersMap = BuildMap(feathers, "feathers");
        _fursMap = BuildMap(furs, "furs");
        _accessoriesMap = BuildMap(accessories, "accessories");
        _flowersMap = BuildMap(flowers, "flowers");
    }

    Dictionary<string, Sprite> BuildMap(List<MaskSpriteEntry> list, string label)
    {
        var map = new Dictionary<string, Sprite>(StringComparer.Ordinal);

        foreach (var e in list)
        {
            if (e == null) continue;

            if (string.IsNullOrWhiteSpace(e.id))
            {
                Debug.LogWarning($"[{name}] Empty id found in {label}.", this);
                continue;
            }

            if (e.sprite == null)
            {
                Debug.LogWarning($"[{name}] Missing sprite for id '{e.id}' in {label}.", this);
                continue;
            }

            if (map.ContainsKey(e.id))
            {
                Debug.LogError($"[{name}] Duplicate id '{e.id}' in {label}. IDs must be unique.", this);
                continue;
            }

            map.Add(e.id, e.sprite);
        }

        return map;
    }

    // --- Getters (return null if not found) ---
    public Sprite GetShape(string id) => GetFrom(_shapeMap, id);
    public Sprite GetPattern(string id) => GetFrom(_patternMap, id);
    public Sprite GetTrim(string id) => GetFrom(_trimMap, id);

    public Sprite GetGilded(string id) => GetFrom(_gildedMap, id);
    public Sprite GetLace(string id) => GetFrom(_laceMap, id);
    public Sprite GetFeathers(string id) => GetFrom(_feathersMap, id);
    public Sprite GetFurs(string id) => GetFrom(_fursMap, id);
    public Sprite GetAccessories(string id) => GetFrom(_accessoriesMap, id);
    public Sprite GetFlowers(string id) => GetFrom(_flowersMap, id);

    Sprite GetFrom(Dictionary<string, Sprite> map, string id)
    {
        if (map == null || string.IsNullOrWhiteSpace(id)) return null;
        return map.TryGetValue(id, out var sprite) ? sprite : null;
    }
}

[Serializable]
public class MaskSpriteEntry
{
    public string id;
    public Sprite sprite;
}
