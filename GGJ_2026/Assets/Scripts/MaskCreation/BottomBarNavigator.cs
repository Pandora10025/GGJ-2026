using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BottomBarNavigator : MonoBehaviour
{
    [Header("Assign the first button to be selected on scene start")]
    public Selectable firstButton;

    void Start()
    {
        if (firstButton != null)
            EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
    }

    void Update()
    {
        if (EventSystem.current == null) return;

        if (Input.GetKeyDown(KeyCode.A))
            Move(-1);
        else if (Input.GetKeyDown(KeyCode.D))
            Move(1);
    }

    void Move(int direction)
    {
        var current = EventSystem.current.currentSelectedGameObject;
        if (current == null) return;

        var selectable = current.GetComponent<Selectable>();
        if (selectable == null) return;

        Selectable next = direction < 0
            ? selectable.FindSelectableOnLeft()
            : selectable.FindSelectableOnRight();

        if (next != null)
            next.Select();
    }
}
