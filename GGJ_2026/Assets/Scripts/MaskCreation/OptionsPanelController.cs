using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OptionsPanelController : MonoBehaviour
{
    [Header("Refs")]
    public CategoryUIController categoryController;
    public MaskStateController maskController;

    [Header("UI")]
    public Transform contentParent;
    public Button optionButtonPrefab;

    void OnEnable()
    {
        if (categoryController != null)
            categoryController.onCategoryChanged.AddListener(Rebuild);
    }

    void OnDisable()
    {
        if (categoryController != null)
            categoryController.onCategoryChanged.RemoveListener(Rebuild);
    }

    void Start()
    {
        // Build once on start
        if (categoryController != null)
            Rebuild(categoryController.selectedCategory);
    }

    void Rebuild(MaskCategory category)
    {
        // clear old
        for (int i = contentParent.childCount - 1; i >= 0; i--)
            Destroy(contentParent.GetChild(i).gameObject);

        // build new (placeholder options)
        switch (category)
        {
            case MaskCategory.Color:
                AddColorOption("Red", Color.red);
                AddColorOption("Green", Color.green);
                AddColorOption("Blue", Color.blue);
                AddColorOption("White", Color.white);
                break;

            case MaskCategory.Shape:
                AddIdOption("Round (placeholder)", () => maskController.SetShape("shape_round_placeholder"));
                AddIdOption("Angular (placeholder)", () => maskController.SetShape("shape_angular_placeholder"));
                break;

            default:
                AddLabelOnly($"No options wired yet for {category}.");
                break;
        }
    }

    void AddColorOption(string label, Color color)
    {
        var btn = Instantiate(optionButtonPrefab, contentParent);
        btn.GetComponentInChildren<TMP_Text>().text = label;
        btn.onClick.AddListener(() => maskController.SetBaseColor(color));
    }

    void AddIdOption(string label, UnityEngine.Events.UnityAction action)
    {
        var btn = Instantiate(optionButtonPrefab, contentParent);
        btn.GetComponentInChildren<TMP_Text>().text = label;
        btn.onClick.AddListener(action);
    }

    void AddLabelOnly(string label)
    {
        var btn = Instantiate(optionButtonPrefab, contentParent);
        btn.GetComponentInChildren<TMP_Text>().text = label;
        btn.interactable = false;
    }
}
