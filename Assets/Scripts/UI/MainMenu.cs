using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    public int currentLevel = 1;

    public void StartButton()
    {
       SceneManager.LoadScene(currentLevel);
    }

    public void DisablePanel()
    {
        gameObject.SetActive(false);
    }

    public void ActivatePanel()
    {
        gameObject.SetActive(true);
    }

        public void QuitButton()
        {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }

}
