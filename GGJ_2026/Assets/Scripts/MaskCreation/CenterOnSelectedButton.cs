using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CenterOnSelectedButton : MonoBehaviour
{
    public RectTransform buttonContainer;
    public RectTransform viewport;

    public float lerpSpeed = 12f;

    RectTransform targetButton;

    void Update()
    {
        var selected = EventSystem.current.currentSelectedGameObject;
        if (selected == null) return;

        var rt = selected.GetComponent<RectTransform>();
        if (rt == null || rt.parent != buttonContainer) return;

        if (targetButton != rt)
            targetButton = rt;

        CenterOnTarget();
    }

    void CenterOnTarget()
    {
        Vector3 worldPos = targetButton.TransformPoint(targetButton.rect.center);
        Vector3 localPos = viewport.InverseTransformPoint(worldPos);

        float offsetX = localPos.x;
        Vector2 containerPos = buttonContainer.anchoredPosition;

        float targetX = containerPos.x - offsetX;

        buttonContainer.anchoredPosition = Vector2.Lerp(
            containerPos,
            new Vector2(targetX, containerPos.y),
            Time.deltaTime * lerpSpeed
        );
    }
}
