using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    GameObject pauseMenu;
    bool paused = false;

    void Start()
    {
        pauseMenu = transform.GetChild(1).gameObject;
        pauseMenu.SetActive(false);
    } 

    public void OnPause()
    {
        if (!paused) // Pause
        {
            paused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else // Unpause
        {
            paused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void MainMenu()
    {
        SceneController.instance.LoadLevel(0);
    }
}
