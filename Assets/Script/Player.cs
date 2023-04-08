using UnityEngine;

public class Player : MonoBehaviour
{
    public float inputSpeed;
    public float rotateSpeed;
    public float brakingSpeed;
    public float maxSpeed;
    public AudioClip hitSound;
    
    private Rigidbody2D rb; 
    private bool isMoving; 
    private Camera camera;  
    private float cameraHeight;    
    private float cameraWidth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        camera = Camera.main;
        cameraHeight = camera.orthographicSize - 0.5f;
        cameraWidth = cameraHeight * camera.aspect + 0.5f;
    }

    void Update()
    {
        UpdateRotation();
        UpdateThrust();
        SpeedControl();
        StickPlayerToTheGameWindow();
    }

    private void UpdateRotation()
    {
        float rotationInput = Input.GetAxisRaw("Horizontal");
        transform.Rotate(0, 0, -rotationInput * rotateSpeed * Time.deltaTime);
    }

    private void UpdateThrust()
    {
        float thrustInput = Input.GetAxisRaw("Vertical");
        
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
    }

    private void SpeedControl()
    {
        if (rb.velocity.magnitude > maxSpeed) 
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }

    private void StickPlayerToTheGameWindow()
    {
        float newX = Mathf.Clamp(transform.position.x, -cameraWidth, cameraWidth); // Ustala zmienną na wartość bliższą ujemnej lub dodatniej szerokości kamery
        float newY = Mathf.Clamp(transform.position.y, -cameraHeight, cameraHeight);
        
        Vector3 newPosition = new Vector3(newX, newY, transform.position.z);
        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("enemy"))
        {
            Manager.hp -= 1;
            GameObject.Find("audio").GetComponent<AudioSource>().PlayOneShot(hitSound);
        }
        else if (col.gameObject.CompareTag("hp"))
        {
            Manager.hp += 1;
        }

        Destroy(col.gameObject);
    }
}