using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class GameOverMenu : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }

    public void Show()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Hide()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        // Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;
    }
}
