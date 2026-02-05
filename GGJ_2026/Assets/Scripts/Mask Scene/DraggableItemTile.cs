using Fungus;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Windows;
using System.Text.RegularExpressions;

public class DraggableItemTile : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image iconImage;

    private MaskItemData boundItem;
    private Canvas rootCanvas;
    private RectTransform rectTransform;

    private GameObject dragGhost;
    private RectTransform dragGhostRT;

    public void Bind(MaskItemData item)
    {
        boundItem = item;
        if (iconImage != null) iconImage.sprite = item.icon;
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rootCanvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (boundItem == null) return;

        // Create a ghost icon that follows the mouse
        dragGhost = new GameObject("DragGhost");
        dragGhost.transform.SetParent(rootCanvas.transform, false);
        //if feather then 5f otherwise 4f
        if (Regex.IsMatch(boundItem.itemId, "feather"))
        {
            Debug.Log("5f");
            dragGhost.transform.localScale = 5f * Vector3.one;
        }
        else {
            dragGhost.transform.localScale = 3.5f * Vector3.one;
        }

        dragGhostRT = dragGhost.AddComponent<RectTransform>();
        dragGhostRT.sizeDelta = rectTransform.sizeDelta;

        var img = dragGhost.AddComponent<Image>();
        img.raycastTarget = false;
        img.sprite = iconImage != null ? iconImage.sprite : null;

        UpdateGhost(eventData);
        MaskDragContext.CurrentItemId = boundItem.itemId;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragGhostRT == null) return;
        UpdateGhost(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragGhost != null) Destroy(dragGhost);
        dragGhost = null;
        dragGhostRT = null;

        // Note: Drop handling happens in MaskDropZone via IDropHandler.
        // Clear current drag item AFTER drop has a chance to read it.
        MaskDragContext.ClearNextFrame();
    }

    private void UpdateGhost(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)rootCanvas.transform,
            eventData.position,
            rootCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : rootCanvas.worldCamera,
            out var localPos
        );
        dragGhostRT.localPosition = localPos;
    }
}
