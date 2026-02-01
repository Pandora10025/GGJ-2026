using UnityEngine;

public class MaskShapeInstance : MonoBehaviour
{
    [SerializeField] private Transform attachmentRoot;
    public Transform AttachmentRoot => attachmentRoot;

    private void Reset()
    {
        // Auto-fill if you named it correctly
        var t = transform.Find("AttachmentRoot");
        if (t != null) attachmentRoot = t;
    }
}
