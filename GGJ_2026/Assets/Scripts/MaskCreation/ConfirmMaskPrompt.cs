using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConfirmMaskPrompt : MonoBehaviour
{
    [Header("Refs")]
    public MaskStateController maskStateController;

    [Header("UI")]
    public GameObject overlayRoot;
    public Button yesButton;
    public Button noButton;

    [Header("Optional")]
    public GameObject defaultSelectedWhenClosed;

    bool _isOpen;

    void Awake()
    {
        if (overlayRoot == null) overlayRoot = gameObject;

        if (yesButton != null) yesButton.onClick.AddListener(ConfirmYes);
        if (noButton != null) noButton.onClick.AddListener(ConfirmNo);

        Close();
    }

    public void Open()
    {
        if (_isOpen) return;
        _isOpen = true;

        overlayRoot.SetActive(true);

        if (yesButton != null)
            EventSystem.current.SetSelectedGameObject(yesButton.gameObject);
    }

    public void Close()
    {
        _isOpen = false;
        overlayRoot.SetActive(false);

        if (defaultSelectedWhenClosed != null)
            EventSystem.current.SetSelectedGameObject(defaultSelectedWhenClosed);
    }

    public void ConfirmYes()
    {
        if (maskStateController == null)
        {
            Debug.LogError("[ConfirmMaskPrompt] maskStateController not assigned.");
            Close();
            return;
        }

        var store = PlayerMaskStore.Instance;
        if (store == null)
        {
            Debug.LogError("[ConfirmMaskPrompt] No PlayerMaskStore instance found in the scene.");
            Close();
            return;
        }

        store.SaveMask(maskStateController.currentMask);
        Debug.Log("[ConfirmMaskPrompt] Mask saved (state only)!");
        Close();
    }

    public void ConfirmNo()
    {
        Close();
    }

    public bool IsOpen() => _isOpen;
}
