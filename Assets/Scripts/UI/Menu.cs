using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
