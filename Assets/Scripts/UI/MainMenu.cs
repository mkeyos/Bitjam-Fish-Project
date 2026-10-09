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

    public void DisableSettings()
    {
        gameObject.SetActive(false);
    }

    public void ActivateSettings()
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
