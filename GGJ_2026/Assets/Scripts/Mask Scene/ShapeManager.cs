using UnityEngine;

public class ShapeManager : MonoBehaviour
{
    [SerializeField] private Transform maskPreviewRoot;
    [SerializeField] private GameObject[] shapePrefabs; 

    private GameObject currentShape;
    public int CurrentShapeIndex { get; private set; } = -1;

    public void SelectShape(int index)
    {
        if (index < 0 || index >= shapePrefabs.Length) return;

        if (currentShape != null) Destroy(currentShape);

        CurrentShapeIndex = index;
        currentShape = Instantiate(shapePrefabs[index], maskPreviewRoot);
        currentShape.transform.localPosition = Vector3.zero;
        currentShape.transform.localRotation = Quaternion.identity;
        currentShape.transform.localScale = Vector3.one;
    }

    private void Start()
    {
        if (CurrentShapeIndex == -1) SelectShape(0);
    }
}
