using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static int hp;
    public string healthTextPrefix;
    public TextMeshProUGUI healthText;
    public GameObject gameOverPanel;

    private float score;
    private float bestScore;
    public string scoreTextPrefix;
    public TextMeshProUGUI scoreText;

    public AudioSource musicSource;
    public AudioSource audioSource;
    public AudioClip gameOverSound;
    private bool playedDeathSound;

    public GameObject enemy;
    public GameObject healthPoint;

    void Start()
    {
        Initialize();
        InvokeRepeating(nameof(SpawnEnemy), 3f, 0.5f);
        InvokeRepeating(nameof(SpawnHealthPoint), 17f, 34f);
    }

    void Update()
    {
        UpdateHealthAndScoreText();
        CheckGameOverCondition();
    }

    void Initialize()
    {
        hp = 3;
        playedDeathSound = false;
        Time.timeScale = 1.0f;
        gameOverPanel.SetActive(false);

        if (PlayerPrefs.HasKey("bestScore"))
            bestScore = PlayerPrefs.GetFloat("bestScore");

        gameOverPanel.transform.position = GameObject.Find("Canvas").transform.position;
    }

    void UpdateHealthAndScoreText()
    {
        healthText.text = healthTextPrefix + hp;
        score += Time.deltaTime * 3;
        scoreText.text = scoreTextPrefix + (int)score;
    }

    void CheckGameOverCondition()
    {
        if (hp <= 0)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            musicSource.Stop();
            if (!playedDeathSound)
            {
                audioSource.PlayOneShot(gameOverSound);
                playedDeathSound = true;
            }
            if (score >= bestScore)
                PlayerPrefs.SetFloat("bestScore", score);
        }
    }

    public void SpawnEnemy()
    {
        Instantiate(enemy);
    }

    public void SpawnHealthPoint()
    {
        Instantiate(healthPoint, Enemy.ReturnMiddlePosition(8.75f), Quaternion.identity);
    }

    public static void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}