using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject menuPanel;
    public GameObject creditsPanel;
    
    void Start()
    {
        creditsPanel.SetActive(false);
        creditsPanel.transform.position = GameObject.Find("Canvas").transform.position;
        scoreText.text = "best score - " + (int)PlayerPrefs.GetFloat("bestScore");
    }
    
    public static void ChangeScene(string sceneName) => SceneManager.LoadScene(sceneName);
    
    public void Credits(bool open)
    {
        if (open)
        {
            menuPanel.SetActive(false);
            creditsPanel.SetActive(true);
        }
        else
        {
            menuPanel.SetActive(true);
            creditsPanel.SetActive(false);
        }
    }
}
