using UnityEngine;

public class MaskHotkeySaver : MonoBehaviour
{
    [SerializeField] private MaskBuildState buildState;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (buildState.shapeIndex < 0)
            {
                Debug.LogWarning("[MaskHotkeySaver] No shape selected. Not saving.");
                return;
            }

            var data = buildState.ToMaskData();
            MaskSaveSystem.Save(data);
        }
    }
}
