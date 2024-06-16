using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    void Start()
    {
        // Deactivate the pause menu UI initially
        pauseMenuUI.SetActive(false);

        //added due to delay issue
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        if (pauseMenuUI.activeSelf)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        //Time.timeScale = 0f; // Stop time
        pauseMenuUI.SetActive(true);
    }

    public void ResumeGame()
    {
        //Time.timeScale = 1f; // Resume time
        pauseMenuUI.SetActive(false);
    }
}
