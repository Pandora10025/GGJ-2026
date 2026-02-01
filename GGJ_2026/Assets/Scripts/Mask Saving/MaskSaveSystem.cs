using UnityEngine;
using System.IO;

public static class MaskSaveSystem
{
    private const string FileName = "mask_save.json";
    private static MaskData cached;

    private static string SavePath =>
        Path.Combine(Application.persistentDataPath, FileName);

    public static void Save(MaskData data)
    {
        cached = data;
        var json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
        Debug.Log($"[MaskSaveSystem] Saved: {SavePath}");
    }

    public static MaskData LoadOrNull()
    {
        Debug.Log("[MaskSaveSystem] LoadPath = " + SavePath + " exists=" + File.Exists(SavePath));

        if (cached != null) return cached;
        if (!File.Exists(SavePath)) return null;

        var json = File.ReadAllText(SavePath);
        cached = JsonUtility.FromJson<MaskData>(json);
        return cached;
    }

    public static bool HasSave() => cached != null || File.Exists(SavePath);
}
