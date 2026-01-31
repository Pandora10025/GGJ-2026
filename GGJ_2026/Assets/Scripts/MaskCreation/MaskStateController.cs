using UnityEngine;

public class MaskStateController : MonoBehaviour
{
    public MaskState currentMask = new MaskState();
    public MaskRenderer renderer;

    void Start()
    {
        Apply();
    }

    public void Apply()
    {
        renderer.Apply(currentMask);
    }

    // These are the ONLY ways the mask should change
    public void SetBaseColor(Color color)
    {
        currentMask.baseColor = color;
        Apply();
    }

    public void SetShape(Sprite shape)
    {
        currentMask.shapeSprite = shape;
        Apply();
    }

    // You’ll add more setters later (lace, feathers, etc.)
}
