using UnityEngine;

public static class MaskDragContext
{
    public static string CurrentItemId;

    public static void ClearNextFrame()
    {
        // Simple jam-safe approach:
        // delay clearing so drop handlers can read it this frame
        var go = new GameObject("MaskDragContext_Clear");
        Object.DontDestroyOnLoad(go);
        go.hideFlags = HideFlags.HideAndDontSave;
        go.AddComponent<MaskDragContextClearer>();
    }

    public static void Clear()
    {
        CurrentItemId = null;
    }

    private class MaskDragContextClearer : MonoBehaviour
    {
        private void LateUpdate()
        {
            MaskDragContext.Clear();
            Destroy(gameObject);
        }
    }
}
