using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class WorldManager : MonoBehaviour
{
    public bool paused = false;

    public static WorldManager instance;

    public GameObject pauseMenu;

    public TextMeshProUGUI pickupReminderText;

    public List<Image> iconsForPickUp = new List<Image>();

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // This makes the WorldManager persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy the new instance if one already exists
        }
    }


   

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        paused = !paused;
        pauseMenu.SetActive(paused);
        Time.timeScale = paused ? 0 : 1;

        Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void ResumeGame()
    {
        paused = false;
        pauseMenu.SetActive(paused);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
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
}
