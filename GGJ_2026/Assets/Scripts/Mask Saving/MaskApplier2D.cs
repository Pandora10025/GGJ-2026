using UnityEngine;

public class MaskApplier2D : MonoBehaviour
{
    [SerializeField] private Transform faceAnchor;
    [SerializeField] private Vector3 localOffset;

    [Header("Optional sorting override for base shape")]
    [SerializeField] private string baseSortingLayer = "Player";
    [SerializeField] private int baseOrder = 10;

    private GameObject currentMask;

    private void Start()
    {
        ApplySavedMask();
    }

    public void ApplySavedMask()
    {
        var data = MaskSaveSystem.LoadOrNull();
        if (data == null) return;

        Debug.Log($"[MaskApplier2D] Loaded shapeIndex={data.shapeIndex}, items={data.placedItems?.Count ?? 0}");

        var shapeCatalog = Resources.Load<MaskShapeCatalog2D>("MaskShapeCatalog2D");
        if (shapeCatalog == null)
        {
            Debug.LogError("[MaskApplier2D] Missing Resources/MaskShapeCatalog2D.asset");
            return;
        }

        if (data.shapeIndex < 0 || data.shapeIndex >= shapeCatalog.shapePrefabs.Length) return;

        if (currentMask != null) Destroy(currentMask);

        // 1) Spawn base shape
        currentMask = Instantiate(shapeCatalog.shapePrefabs[data.shapeIndex], faceAnchor);
        currentMask.transform.localPosition = localOffset;
        currentMask.transform.localRotation = Quaternion.identity;
        currentMask.transform.localScale = Vector3.one;

        var shapeInst = currentMask.GetComponent<MaskShapeInstance>();
        if (shapeInst == null || shapeInst.AttachmentRoot == null)
        {
            Debug.LogError("[MaskApplier2D] Shape prefab missing MaskShapeInstance/AttachmentRoot.");
            return;
        }

        // Ensure base renders properly
        var baseSR = currentMask.GetComponentInChildren<SpriteRenderer>();
        if (baseSR != null)
        {
            baseSR.sortingLayerName = baseSortingLayer;
            baseSR.sortingOrder = baseOrder;
        }

        // 2) Spawn placed items
        MaskItemRegistry.Warm();

        int i = 0;
        foreach (var placed in data.placedItems)
        {
            var itemData = MaskItemRegistry.Get(placed.itemId);
            if (itemData == null || itemData.prefab2D == null) continue;

            var go = Instantiate(itemData.prefab2D, shapeInst.AttachmentRoot);
            go.transform.localPosition = placed.localPosition.ToUnity();
            go.transform.localEulerAngles = new Vector3(0, 0, placed.localRotationZ);
            go.transform.localScale = placed.localScale.ToUnity();

            var sr = go.GetComponentInChildren<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = placed.tint.ToUnity();

                // Force ALL items to render above the base, in a consistent stack order
                sr.sortingLayerName = baseSortingLayer;
                sr.sortingOrder = baseOrder + 1 + i;
            }

            i++;
        }
    }
}
