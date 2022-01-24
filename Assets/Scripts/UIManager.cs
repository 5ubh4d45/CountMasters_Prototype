using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [Header("Score System")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private TextMeshProUGUI startText;

    [SerializeField] private GameObject finishScreen;

    public TextMeshProUGUI StartText => startText; //temp accessing in player movement

    private int _score;

    public static UIManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        finishScreen.SetActive(false);
        scoreText.text = "Score: 000";
    }

    // Update is called once per frame
    void Update()
    {
        //sets the score

        scoreText.text = "Score: " + ScoreManager.Instance.Score;
    }

    public void DisableStartText()
    {
        startText.gameObject.SetActive(false);
    }

    public void PlayerDead()
    {
        //freeze time
        // Time.timeScale = 0f;
        
        finishScreen.SetActive(true);
    }

    public void Restart()
    {
        // Time.timeScale = 1f;
        
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
        
    }

    public void NextLevel()
    {
        // Scene currentScene = SceneManager.GetActiveScene();
        // Scene nextScene = SceneManager.GetSceneByBuildIndex(currentScene.buildIndex + 1);
        //
        // Debug.Log("CurrentScene: " + currentScene + "\n NextScene: " + nextScene.name + nextScene.buildIndex);
        //
        //
        // if (nextScene.name == "FinalLevel")
        // {
        //     Debug.Log("Final Level Reached");
        //     
        //     SceneManager.LoadScene(0);
        //     
        //     return;
        // }
        // Debug.Log("Next LEvel Approaching...");
        // SceneManager.LoadScene(nextScene.name);

        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "FinalLevel")
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        }
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
