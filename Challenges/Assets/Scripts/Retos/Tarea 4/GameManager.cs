using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int _gameSceneIndex;

    [SerializeField]
    private PlayerInput _playerInput;

    [SerializeField]
    private StarterAssetsInputs _starterAssetsInputs;

    private void Start()
    {
        if (_playerInput == null)
        {
            throw new UnityException("No player input selected");
        }
        if (_starterAssetsInputs == null)
        {
            throw new UnityException("No starter asset input selected");
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(_gameSceneIndex);
    }

    public void GameIsOver()
    {
        _playerInput.DeactivateInput();
        _starterAssetsInputs.cursorLocked = false;
        _starterAssetsInputs.cursorInputForLook = false;
    }
}
