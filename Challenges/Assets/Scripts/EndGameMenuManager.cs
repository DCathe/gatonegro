using UnityEngine;

public class EndGameMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _uiElements;

    private void Start()
    {
        if (_uiElements == null || _uiElements.Length == 0)
        {
            throw new UnityException("No elements have been assigned");
        }

        Hide();
    }

    public void Hide()
    {
        foreach (var element in _uiElements)
        {
            element.SetActive(false);
        }
    }

    public void Show()
    {
        foreach (var element in _uiElements)
        {
            element.SetActive(true);
        }
    }
}
