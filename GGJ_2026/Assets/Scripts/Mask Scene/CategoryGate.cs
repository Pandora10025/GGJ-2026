using UnityEngine;
using UnityEngine.UI;

public class CategoryGate : MonoBehaviour
{
    [SerializeField] private MaskBuildState buildState;

    [Header("Assign all category buttons EXCEPT Shape")]
    [SerializeField] private Button[] gatedButtons;

    [Header("Optional: grey out visuals too")]
    [SerializeField] private CanvasGroup[] gatedCanvasGroups;

    private void Start()
    {
        ApplyGate(buildState != null && buildState.HasShape);
        if (buildState != null) buildState.OnHasShapeChanged += ApplyGate;
    }

    private void OnDestroy()
    {
        if (buildState != null) buildState.OnHasShapeChanged -= ApplyGate;
    }

    private void ApplyGate(bool hasShape)
    {
        foreach (var b in gatedButtons)
        {
            if (b != null) b.interactable = hasShape;
        }

        foreach (var cg in gatedCanvasGroups)
        {
            if (cg != null) cg.alpha = hasShape ? 1f : 0.4f;
        }
    }
}
