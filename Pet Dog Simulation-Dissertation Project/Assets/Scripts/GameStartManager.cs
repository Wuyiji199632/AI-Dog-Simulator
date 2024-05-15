using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameStartManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }   

    public void QuitGame()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
        // This code will execute when the game is built and running outside the Unity Editor.
        Application.Quit();
#endif
    }

    public void QuitApp()
    {
        QuitGame();
        Application.Quit();

    }
}
