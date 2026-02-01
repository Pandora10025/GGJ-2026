using UnityEngine;

public class CategoryPanelPopulator : MonoBehaviour
{
    [SerializeField] private MaskCategory category;
    [SerializeField] private Transform contentRoot;          // grid root
    [SerializeField] private DraggableItemTile tilePrefab;   // UI prefab

    public void Refresh()
    {
        if (contentRoot == null) { Debug.LogError("[CategoryPanelPopulator] contentRoot not assigned", this); return; }
        if (tilePrefab == null) { Debug.LogError("[CategoryPanelPopulator] tilePrefab not assigned", this); return; }

        for (int i = contentRoot.childCount - 1; i >= 0; i--)
            Destroy(contentRoot.GetChild(i).gameObject);

        var items = MaskItemDatabase.GetByCategory(category);
        Debug.Log($"[CategoryPanelPopulator] {category} items found: {items.Count}", this);

        foreach (var item in items)
        {
            var tile = Object.Instantiate(tilePrefab, contentRoot);
            tile.Bind(item);
        }
    }

    private void OnEnable()
    {
        Refresh();
    }
}
