using UnityEngine;
using UnityEngine.EventSystems;

public class CategoryButton : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    public MaskCategory category;
    public CategoryUIController controller;

    public void OnSelect(BaseEventData eventData)
    {
        controller.SelectCategory(category);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        controller.SelectCategory(category);
    }
}
