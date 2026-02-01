using UnityEngine;

public enum MaskCategory
{
    Shape,
    Feathers,
    Lace,
    Furs,
    Flowers,
    Accessories
}

public class CategoryUIController : MonoBehaviour
{
    [System.Serializable]
    public class CategoryPanel
    {
        public MaskCategory category;
        public GameObject panel; // Right-side panel for this category
    }

    [Header("Assign each category to its right-side panel")]
    [SerializeField] private CategoryPanel[] categoryPanels;

    [Header("Which category to show on start")]
    [SerializeField] private MaskCategory defaultCategory = MaskCategory.Shape;

    private void Start()
    {
        ShowCategory(defaultCategory);
    }

    /// <summary>
    /// Unity Button OnClick-friendly wrapper (Unity Inspector likes int/bool/float/string).
    /// Set the button parameter to:
    /// 0=Shape, 1=Feathers, 2=Lace, 3=Furs, 4=Flowers, 5=Accessories
    /// </summary>
    public void ShowCategory(int categoryIndex)
    {
        if (categoryIndex < 0 || categoryIndex >= System.Enum.GetValues(typeof(MaskCategory)).Length)
        {
            Debug.LogWarning($"Invalid category index: {categoryIndex}");
            return;
        }

        ShowCategory((MaskCategory)categoryIndex);
    }

    /// <summary>
    /// Core category switching logic.
    /// </summary>
    public void ShowCategory(MaskCategory category)
    {
        // Debug.Log("Switching category to: " + category); // Uncomment if helpful

        for (int i = 0; i < categoryPanels.Length; i++)
        {
            var entry = categoryPanels[i];
            if (entry.panel == null) continue;

            bool shouldShow = entry.category == category;
            entry.panel.SetActive(shouldShow);
        }
    }
}
