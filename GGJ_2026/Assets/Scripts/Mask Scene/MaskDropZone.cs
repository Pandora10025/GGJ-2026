using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MaskDropZone : MonoBehaviour, IDropHandler
{
    [SerializeField] private ShapeManager shapeManager;
    [SerializeField] private MaskBuildState buildState;
    [SerializeField] private Camera worldCamera; // camera that renders the mask preview

    public void OnDrop(PointerEventData eventData)
    {
        var itemId = MaskDragContext.CurrentItemId;
        if (string.IsNullOrEmpty(itemId)) return;

        if (buildState == null || !buildState.HasShape)
        {
            Debug.Log("[MaskDropZone] Select a shape first.");
            return;
        }

        var shape = shapeManager.CurrentShapeInstance;
        if (shape == null || shape.AttachmentRoot == null) return;

        var itemData = MaskItemDatabase.GetById(itemId);
        if (itemData == null || itemData.prefab2D == null) return;

        // Convert drop screen position to world position
        if (worldCamera == null) worldCamera = Camera.main;
        var screen = eventData.position;
        var world = worldCamera.ScreenToWorldPoint(new Vector3(screen.x, screen.y, -worldCamera.transform.position.z));
        world.z = shape.AttachmentRoot.position.z;

        // Spawn under attachment root at local position
        var go = Object.Instantiate(itemData.prefab2D, shape.AttachmentRoot);
        go.transform.position = new Vector3(world.x, world.y, -1);//needs manual depth thing
        go.transform.localScale = Vector3.one;
        Debug.Log(go.name);

        // Ensure it has PlacedItemInstance and gets registered
        var inst = go.GetComponent<PlacedItemInstance>();
        if (inst == null) inst = go.AddComponent<PlacedItemInstance>();
        inst.itemId = itemId;

        buildState.RegisterPlaced(inst);
    }
}
