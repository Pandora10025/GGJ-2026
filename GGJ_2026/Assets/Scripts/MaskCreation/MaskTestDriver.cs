using UnityEngine;

public class MaskTestDriver : MonoBehaviour
{
    public MaskRenderer renderer;
    public MaskState testState;

    void Start()
    {
        renderer.Apply(testState);
    }
}
