using UnityEngine;

public class ShapeManager : MonoBehaviour
{
    [Header("Scene refs")]
    [SerializeField] private Transform maskPreviewRoot;
    [SerializeField] private MaskBuildState buildState;

    [Header("Shape prefabs (size 5)")]
    [SerializeField] private GameObject[] shapePrefabs;

    private GameObject currentShapeGO;
    private MaskShapeInstance currentShape;

    public int CurrentShapeIndex { get; private set; } = -1;

    // Expose this for other systems (like MaskItemPlacer2D)
    public MaskShapeInstance CurrentShapeInstance => currentShape;

    private void Awake()
    {
        if (buildState == null) buildState = GetComponent<MaskBuildState>();
    }

    // Call from UI buttons
    public void SelectShape(int index)
    {
        if (index < 0 || index >= shapePrefabs.Length) return;

        // Changing shape wipes everything applied
        if (buildState != null)
            buildState.ClearAllPlaced();

        // Swap shape (destroying old shape also destroys any children under it)
        if (currentShapeGO != null) Destroy(currentShapeGO);

        CurrentShapeIndex = index;

        currentShapeGO = Instantiate(shapePrefabs[index], maskPreviewRoot);
        currentShapeGO.transform.localPosition = Vector3.zero;
        currentShapeGO.transform.localRotation = Quaternion.identity;
        currentShapeGO.transform.localScale = Vector3.one;

        currentShape = currentShapeGO.GetComponent<MaskShapeInstance>();
        if (currentShape == null)
        {
            Debug.LogError("Selected shape prefab is missing MaskShapeInstance on its root.");
        }

        // This is the authoritative “shape selected” flag (via shapeIndex)
        if (buildState != null)
            buildState.SetShapeIndex(index);
    }
}
