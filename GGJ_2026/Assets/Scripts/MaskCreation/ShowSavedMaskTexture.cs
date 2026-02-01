using UnityEngine;
using UnityEngine.UI;

public class ShowSavedMaskTexture : MonoBehaviour
{
    public RawImage target;

    void Start()
    {
        var store = PlayerMaskStore.Instance;
        if (store == null || !store.HasSavedMaskTexture)
        {
            Debug.LogWarning("[ShowSavedMaskTexture] No saved mask texture.");
            return;
        }

        target.texture = store.SavedMaskTexture;
    }
}
