using UnityEditor;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public void FinishGame()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
