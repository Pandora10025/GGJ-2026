using UnityEngine;
using UnityEngine.UI;

public class MaskRenderer : MonoBehaviour
{
    [Header("Library")]
    public MaskAssetLibrary library;

    [Header("Layers")]
    public Image baseShape;
    public Image surfaceDetail;   
    public Image trim;            

    public void Apply(MaskState state)
    {
        Debug.Log($"[MaskRenderer] Apply() got baseColor={state.baseColor} shapeId={state.shapeId}");
        if (state == null) return;

        // --- Base Shape ---
        if (baseShape != null)
        {
            Sprite shapeSprite = library != null ? library.GetShape(state.shapeId) : null;
            baseShape.sprite = shapeSprite;
            baseShape.color = state.baseColor;

            baseShape.enabled = true;
        }

        // --- Pattern / Surface Detail ---
        if (surfaceDetail != null)
        {
            Sprite patternSprite = library != null ? library.GetPattern(state.patternId) : null;
            surfaceDetail.sprite = patternSprite;
            surfaceDetail.color = state.patternColor;
            surfaceDetail.enabled = patternSprite != null;
        }

        // --- Trim ---
        if (trim != null)
        {
            Sprite trimSprite = library != null ? library.GetTrim(state.trimId) : null;
            trim.sprite = trimSprite;
            trim.color = state.trimColor;
            trim.enabled = trimSprite != null;
        }
    }
}
