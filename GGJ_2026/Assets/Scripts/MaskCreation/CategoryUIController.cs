using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CategoryUIController : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text headerText;

    [Header("State")]
    public MaskCategory selectedCategory;

    [Header("Events")]
    public UnityEvent<MaskCategory> onCategoryChanged;

    void Start()
    {
        RefreshUI();
        onCategoryChanged?.Invoke(selectedCategory);
    }

    // Keep this for button OnClick() if you want it
    public void SelectCategory(int categoryIndex)
    {
        SelectCategory((MaskCategory)categoryIndex);
    }

    public void SelectCategory(MaskCategory category)
    {
        if (selectedCategory == category) return;

        selectedCategory = category;
        RefreshUI();
        onCategoryChanged?.Invoke(selectedCategory);
    }

    void RefreshUI()
    {
        if (headerText != null)
            headerText.text = PrettyName(selectedCategory);
    }

    // Optional: nicer display names than enum.ToString()
    string PrettyName(MaskCategory cat)
    {
        return cat switch
        {
            MaskCategory.DanglingAccessories => "Accessories",
            _ => cat.ToString()
        };
    }
}
