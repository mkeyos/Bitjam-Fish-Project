using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
