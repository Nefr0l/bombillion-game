using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public static void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
