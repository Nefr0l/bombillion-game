using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public GameObject enemy;
    public string prefixText = "HP - ";
    public TextMeshProUGUI healthText;
    public GameObject gameOverPanel;

    public static int hp;
    
    void Start()
    {
        Time.timeScale = 1.0f;
        gameOverPanel.SetActive(false);
        gameOverPanel.transform.position = GameObject.Find("Canvas").transform.position;
        hp = 3;
        InvokeRepeating("SpawnEnemy",1f,0.5f);
    }

    void Update()
    {
        healthText.text = prefixText + hp;

        if (hp <= 0)
        {
            Time.timeScale = 0;
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
