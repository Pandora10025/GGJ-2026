using UnityEngine;

public class LoadSavedMaskOnStart : MonoBehaviour
{
    public MaskRenderer renderer;

    void Start()
    {
        if (PlayerMaskStore.Instance == null)
        {
            Debug.LogError("[LoadSavedMaskOnStart] PlayerMaskStore.Instance is null");
            return;
        }

        if (!PlayerMaskStore.Instance.HasSavedMask)
        {
            Debug.LogWarning("[LoadSavedMaskOnStart] No saved mask found");
            return;
        }

        var savedMask = PlayerMaskStore.Instance.SavedMask;
        Debug.Log($"[LoadSavedMaskOnStart] Loaded mask with baseColor {savedMask.baseColor}");

        renderer.Apply(savedMask);
    }
}
