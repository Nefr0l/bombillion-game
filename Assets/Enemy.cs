using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public float range;
    public float speed;
    public float lifespan;
    
    private Vector2 startPosition;
    private Vector2 middlePosition;
    private Vector2 moveDirection;
    
    private static float cameraHeight;
    private static float cameraWidth;
    private static Vector2 centerPoint;
    private float startTime;

    private void Start()
    {
        startTime = Time.time;
        centerPoint = Camera.main.transform.position;
        cameraHeight = Camera.main.orthographicSize - 0.5f;
        cameraWidth = cameraHeight * Camera.main.aspect + 0.5f;
        
        startPosition = ReturnStartPosition();
        middlePosition = ReturnMiddlePosition();
        moveDirection = (middlePosition - startPosition).normalized;
        gameObject.transform.position = startPosition;
    }
    
    void Update()
    {
        float timeElapsed = Time.time - startTime;
        float distanceToMove = speed * timeElapsed;
        Vector3 newPosition = startPosition + moveDirection * distanceToMove;
        transform.position = newPosition;
        
        if (Time.time - startTime > lifespan) 
            Destroy(gameObject);
    }

    public Vector2 ReturnStartPosition()
    {
        float xDistance = cameraHeight * 2.8f;
        float yDistance = cameraWidth * 0.8f;
        int spawnDirection = Random.Range(1, 5);
        float x = 0;
        float y = 0;

        switch (spawnDirection)
        {
            case 1: // Lewo
                x = centerPoint.x - xDistance;
                y = Random.Range(centerPoint.y - yDistance, centerPoint.y + yDistance);
                break;
            case 2: // Prawo
                x = centerPoint.x + xDistance;
                y = Random.Range(centerPoint.y - yDistance, centerPoint.y + yDistance);
                break;
            case 3: // Góra
                x = Random.Range(centerPoint.x - xDistance, centerPoint.x + xDistance);
                y = centerPoint.y - yDistance;
                break;
            case 4: // Dół
                x = Random.Range(centerPoint.x - xDistance, centerPoint.x + xDistance);
                y = centerPoint.y + yDistance;
                break;
        }
        return new Vector2(x,y);
    }

    public Vector2 ReturnMiddlePosition()
    {
        float x = Random.Range(centerPoint.x - range, centerPoint.x + range);
        float y = Random.Range(centerPoint.y - range, centerPoint.y + range);
        return new Vector2(x, y);
    }
}