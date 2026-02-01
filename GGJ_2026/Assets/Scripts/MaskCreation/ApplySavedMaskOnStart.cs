using UnityEngine;

public class ApplySavedMaskOnStart : MonoBehaviour
{
    public MaskRenderer renderer;

    void Start()
    {
        var store = PlayerMaskStore.Instance;
        if (store == null)
        {
            Debug.LogError("[ApplySavedMaskOnStart] PlayerMaskStore.Instance is null. Did you load this scene from MaskMaker during Play Mode?");
            return;
        }

        if (!store.HasSavedMask)
        {
            Debug.LogWarning("[ApplySavedMaskOnStart] No saved mask found.");
            return;
        }

        renderer.Apply(store.SavedMask);
        Debug.Log("[ApplySavedMaskOnStart] Applied saved mask.");
    }
}
