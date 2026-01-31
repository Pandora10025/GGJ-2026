using UnityEngine;
using UnityEngine.UI;

public class MaskRenderer : MonoBehaviour
{
    public Image shapeImage;
    public Image patternImage;
    public Image trimImage;

    public void Apply(MaskState state)
    {
        shapeImage.sprite = state.shapeSprite;
        shapeImage.color = state.baseColor;

        patternImage.sprite = state.patternSprite;
        patternImage.color = state.patternColor;
        patternImage.enabled = state.patternSprite != null;

        trimImage.sprite = state.trimSprite;
        trimImage.color = state.trimColor;
        trimImage.enabled = state.trimSprite != null;
    }
}
