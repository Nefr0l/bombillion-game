using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    // Zmienne - Gra
    public GameObject enemy;
    public static int hp;
    
    // Zmienne - UI
    public string healthTextPrefix = "HP - ";
    public TextMeshProUGUI healthText;
    public GameObject gameOverPanel;

    private float score;
    private float bestScore;
    public string scoreTextPrefix = "score - ";
    public TextMeshProUGUI scoreText;
    
    // Zmienne - Dźwięk
    public AudioSource musicSource;
    public AudioSource audioSource;
    public AudioClip gameOverSound;
    private bool playedDeathSound;
    
    void Start()
    {
        if (PlayerPrefs.HasKey("bestScore"))
        {
            bestScore = PlayerPrefs.GetFloat("bestScore");
        }
        playedDeathSound = false;
        Time.timeScale = 1.0f;
        gameOverPanel.SetActive(false);
        gameOverPanel.transform.position = GameObject.Find("Canvas").transform.position;
        hp = 3;
        InvokeRepeating("SpawnEnemy",1f,0.5f);
    }

    void Update()
    {
        healthText.text = healthTextPrefix + hp;
        score += Time.deltaTime * 3;
        scoreText.text = scoreTextPrefix + (int)score;
        
        if (hp <= 0)
        {
            Time.timeScale = 0;
            musicSource.Stop();
            if (!playedDeathSound) { 
                audioSource.PlayOneShot(gameOverSound);
                playedDeathSound = true;
            }

            if (score >= bestScore)
            {
                PlayerPrefs.SetFloat("bestScore", score);
            }
            
            gameOverPanel.SetActive(true);
        }
    }

    public void SpawnEnemy()
    {
        Instantiate(enemy);
    }

    public static void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
