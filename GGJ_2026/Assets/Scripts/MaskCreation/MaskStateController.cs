using UnityEngine;

public class MaskStateController : MonoBehaviour
{
    public MaskState currentMask = new MaskState();

    [Header("Rendering")]
    public MaskRenderer renderer;

    void Start()
    {
        currentMask.shapeId = "shape_round_placeholder";
        currentMask.baseColor = Color.red;
        Apply();
    }

    void Apply()
    {
        Debug.Log($"[MaskStateController] Apply() baseColor={currentMask.baseColor} shapeId={currentMask.shapeId}");

        if (renderer == null)
        {
            Debug.LogError("[MaskStateController] renderer is NOT assigned!");
            return;
        }

        renderer.Apply(currentMask);
    }


    // ===== Shape =====
    public void SetShape(string shapeId)
    {
        currentMask.shapeId = shapeId;
        Apply();
    }

    // ===== Base Color =====
    public void SetBaseColor(Color color)
    {
        currentMask.baseColor = color;
        Apply();
    }

    // ===== Pattern =====
    public void SetPattern(string patternId)
    {
        currentMask.patternId = patternId;
        Apply();
    }

    public void SetPatternColor(Color color)
    {
        currentMask.patternColor = color;
        Apply();
    }

    // ===== Trim =====
    public void SetTrim(string trimId)
    {
        currentMask.trimId = trimId;
        Apply();
    }

    public void SetTrimColor(Color color)
    {
        currentMask.trimColor = color;
        Apply();
    }

    // Future categories follow the same pattern:
    // SetGilded(string id)
    // SetLace(string id)
    // SetFeathers(string id)
    // etc.
}
