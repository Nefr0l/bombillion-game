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

    public GameObject enemy;
    public AudioSource musicSource;
    public AudioSource audioSource;
    public AudioClip gameOverSound;
    private bool playedDeathSound;
    
    void Start()
    {
        hp = 3;
        playedDeathSound = false;
        Time.timeScale = 1.0f;
        gameOverPanel.SetActive(false);
        
        if (PlayerPrefs.HasKey("bestScore")) 
            bestScore = PlayerPrefs.GetFloat("bestScore");
        gameOverPanel.transform.position = GameObject.Find("Canvas").transform.position;
        InvokeRepeating(nameof(SpawnEnemy),1f,0.5f);
    }

    void Update()
    {
        healthText.text = healthTextPrefix + hp;
        score += Time.deltaTime * 3;
        scoreText.text = scoreTextPrefix + (int)score;
        
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

    public void SpawnEnemy() => Instantiate(enemy);
    public static void ChangeScene(string sceneName) => SceneManager.LoadScene(sceneName);
}