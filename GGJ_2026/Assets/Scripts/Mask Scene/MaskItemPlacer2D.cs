using UnityEngine;

public class MaskItemPlacer2D : MonoBehaviour
{
    [SerializeField] private MaskBuildState buildState;
    [SerializeField] private ShapeManager shapeManager;

    public void PlaceItemById(string itemId)
    {
        if (buildState.shapeIndex < 0)
        {
            Debug.LogWarning("[MaskItemPlacer2D] Select a shape first.");
            return;
        }

        var shape = shapeManager.CurrentShapeInstance;
        if (shape == null || shape.AttachmentRoot == null) return;

        var itemData = MaskItemRegistry.Get(itemId);
        if (itemData == null || itemData.prefab2D == null)
        {
            Debug.LogWarning($"[MaskItemPlacer2D] Missing item data/prefab for: {itemId}");
            return;
        }

        var go = Instantiate(itemData.prefab2D, shape.AttachmentRoot);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;

        var inst = go.GetComponent<PlacedItemInstance>();
        if (inst == null) inst = go.AddComponent<PlacedItemInstance>();
        inst.itemId = itemId;

        buildState.RegisterPlaced(inst);
    }
}
