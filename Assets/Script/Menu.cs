using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject menuPanel;

    public Slider musicSlider;
    public Slider fxSlider;
    private float musicVolume;
    private float fxVolume;
    public AudioMixer mixer;

    void Start()
    {
        scoreText.text = "best score - " + (int)PlayerPrefs.GetFloat("bestScore");
        if (PlayerPrefs.HasKey("fxVolume")) 
            ReadVolumeData(); 
    }

    private void ReadVolumeData()
    {
        musicVolume = PlayerPrefs.GetFloat("musicVolume");
        fxVolume = PlayerPrefs.GetFloat("fxVolume");
        musicSlider.value = musicVolume;
        fxSlider.value = fxVolume;
    }

    public void SaveVolumeData()
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("fxVolume", fxVolume);
    }
    
    public void SliderValueChanged(string volumeType) // music, fx
    {
        switch (volumeType)
        {
            case "music":
                musicVolume = musicSlider.value;
                mixer.SetFloat("musicVolume", musicVolume);
                print("Głośność muzyki: " + musicVolume + "db");
                break;
            case "fx":
                fxVolume = fxSlider.value;
                mixer.SetFloat("fxVolume", fxVolume);
                print("Głośność dźwięków: " + fxVolume + "db");
                break;
        }
        SaveVolumeData();
    }

    public static void ChangeScene(string sceneName) 
        => SceneManager.LoadScene(sceneName);

    public void OpenPanel(GameObject panel)
    {
        panel.transform.position = GameObject.Find("Canvas").transform.position;
        panel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        menuPanel.SetActive(true);
    }
}
