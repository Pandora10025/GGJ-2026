using UnityEngine;

[CreateAssetMenu(menuName = "Mask/Shape Catalog 2D", fileName = "MaskShapeCatalog2D")]
public class MaskShapeCatalog2D : ScriptableObject
{
    public GameObject[] shapePrefabs; // index-based
}
