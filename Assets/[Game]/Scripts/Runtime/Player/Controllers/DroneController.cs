using UnityEngine;

public class DroneController : MonoBehaviour
{
    public float speed = 5f;

    [SerializeField] FloatingHealthBar healthBar; 
    [SerializeField] float maxHealth = 1000f;  
    private float health;

    private Vector2 touchInput;
    private Vector3 movement;
    private bool isControlEnabled = false;

    private void Awake()
    {
        health = maxHealth;  
        healthBar = GetComponentInChildren<FloatingHealthBar>(); 
    }

    private void Start()
    {
        healthBar.UpdateHealthBar(health, maxHealth); 
    }

    private void Update()
    {
        if (isControlEnabled)
        {
            HandleInput();
            HandleMovement();
        }
    }

    private void HandleInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        touchInput = new Vector2(horizontalInput, verticalInput);
    }

    private void HandleMovement()
    {
        movement = new Vector3(touchInput.x, 0, touchInput.y);

        if (movement.magnitude > 1)
            movement.Normalize();

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    public void EnableDroneControl()
    {
        gameObject.SetActive(true);
    }

    public void DisableDroneControl()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth); 
        if (health <= 0)
        {
            DestroyDrone();
        }
    }

    private void DestroyDrone() 
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAmmo"))
        {
            Enemy_Bullet bullet = other.GetComponent<Enemy_Bullet>();
            if (bullet != null)
            {
                float damage = 10f;
                TakeDamage(damage);
            }
            Destroy(other.gameObject);
        }
    }
}
