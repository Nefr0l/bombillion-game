using System;
using Unity.VisualScripting;
using UnityEngine;

public class Moving : MonoBehaviour
{
    // Zmienne
    public float inputSpeed = 10f;
    public float rotateSpeed = 100f;
    public float brakingSpeed = 2f;
    public float maxSpeed = 20f;
    
    private Rigidbody2D rb; 
    private bool isMoving; 
    private Camera mainCamera;  
    public static float cameraHeight;    
    public static float cameraWidth;
    private float halfPlayerSize;

    public AudioClip hitClip;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        cameraHeight = mainCamera.orthographicSize - 0.5f;
        cameraWidth = cameraHeight * mainCamera.aspect + 0.5f;
    }

    void Update()
    {
        // Obrót obiektu
        float rotationInput = Input.GetAxisRaw("Horizontal");
        transform.Rotate(0, 0, -rotationInput * rotateSpeed * Time.deltaTime);

        // Poruszanie obiektu
        float thrustInput = Input.GetAxisRaw("Vertical");

        // Przyśpieszenie i hamowanie
        if (thrustInput != 0)
        {
            rb.AddForce(transform.up * (thrustInput * inputSpeed));
            isMoving = true;
        }
        else if (isMoving)
        {
            rb.velocity = rb.velocity.normalized * Mathf.Max(rb.velocity.magnitude - brakingSpeed * Time.deltaTime, 0);
            isMoving = rb.velocity.magnitude > 0;
        }
        
        // Limit prędkości
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        
        // Limit poruszania się gracza do wewnątrz ekranu
        Vector3 newPosition = transform.position;
        if (transform.position.y > cameraHeight)
        {
            newPosition.y = cameraHeight;
        }
        else if (transform.position.y < -cameraHeight)
        {
            newPosition.y = -cameraHeight;
        }
        if (transform.position.x > cameraWidth)
        {
            newPosition.x = cameraWidth;
        }
        else if (transform.position.x < -cameraWidth)
        {
            newPosition.x = -cameraWidth;
        }
        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Manager.hp -= 1;
        GameObject.Find("audio").GetComponent<AudioSource>().PlayOneShot(hitClip);
        Destroy(col.gameObject);
    }
}
