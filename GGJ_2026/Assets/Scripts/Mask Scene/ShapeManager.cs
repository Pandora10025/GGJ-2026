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

    private void Awake()
    {
        if (buildState == null) buildState = GetComponent<MaskBuildState>();
    }

    // Call from UI buttons
    public void SelectShape(int index)
    {
        if (index < 0 || index >= shapePrefabs.Length) return;

        // If changing shape after already having one, clear applied stuff first
        if (currentShape != null)
        {
            ClearAllApplied();
        }

        // Swap shape
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

        buildState.SetHasShape(true);
    }

    private void ClearAllApplied()
    {
        // For now, applied things are assumed to be children under AttachmentRoot
        var root = currentShape.AttachmentRoot;
        if (root == null) return;

        for (int i = root.childCount - 1; i >= 0; i--)
        {
            Destroy(root.GetChild(i).gameObject);
        }
    }
}
