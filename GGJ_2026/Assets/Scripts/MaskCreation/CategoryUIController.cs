using UnityEngine;
using TMPro;
public class CategoryUIController : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text headerText;

    [Header("State")]
    public MaskCategory selectedCategory;

    public void SelectCategory(int categoryIndex)
    {
        selectedCategory = (MaskCategory)categoryIndex;
        RefreshUI();
    }

    public void SelectCategory(MaskCategory category)
    {
        selectedCategory = category;
        RefreshUI();
    }

    void Start()
    {
        RefreshUI();
    }

    void RefreshUI()
    {
        if (headerText != null)
            headerText.text = selectedCategory.ToString();
    }
}
