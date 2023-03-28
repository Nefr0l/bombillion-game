using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject enemy;
    
    void Start()
    {
        InvokeRepeating("SpawnEnemy",1f,1f);
    }

    void Update()
    {
        
    }

    public void SpawnEnemy()
    {
        Instantiate(enemy);
    }
}
