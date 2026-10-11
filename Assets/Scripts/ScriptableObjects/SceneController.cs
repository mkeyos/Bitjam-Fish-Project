using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    public static event System.Action OnStageCleared;
    public bool isStageCleared {get; private set;}
    private Coroutine checkEnemiesCoroutine;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void Start()
    {
        StartEnemyCheck();
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartEnemyCheck();
    }

    private void StartEnemyCheck()
    {
        if (checkEnemiesCoroutine != null)
        {
            StopCoroutine(checkEnemiesCoroutine);
        }
        isStageCleared = false;
        checkEnemiesCoroutine = StartCoroutine(CheckEnemies());
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
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
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (checkEnemiesCoroutine != null)
        {
            StopCoroutine(checkEnemiesCoroutine);
        }
    }
}
