using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    public static event System.Action OnStageCleared;
    public bool isStageCleared {get; private set;}

    void OnEnable()
    {
        isStageCleared = false;
    }
    void Start()
    {
        StartCoroutine(CheckEnemies());
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator CheckEnemies()
    {
        yield return null;

        while (!isStageCleared)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length == 0)
            {
                isStageCleared = true;
                OnStageCleared?.Invoke();
                yield break;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    void OnDisable()
    {
        OnStageCleared = null;
    }
}
