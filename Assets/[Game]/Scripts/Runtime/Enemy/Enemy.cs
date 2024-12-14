using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private Transform previousTarget;
    private Rigidbody rb;

    [SerializeField] FloatingHealthBar healthBar;
    [SerializeField] EnemySo enemyData;
    [SerializeField] float speed = 5f;
    [SerializeField] float stopDistanceFromDrone = 5f;
    [SerializeField] private EnemyKillCounterSO killCounterData;
    private float health;
    private float maxHealth;
    private float damageAmount;
    public GameObject lootBag;

    public GameObject _particleSystem;
    private float moveFrequency;
    private float moveAmplitude;
    private Vector3 randomMovementDirection;
    private DroneController playerDrone;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        GameObject droneObject = GameObject.FindWithTag("Drone");
        if (droneObject != null)
        {
            playerDrone = droneObject.GetComponent<DroneController>();
        }
    }

    private void Start()
    {
        if (enemyData != null)
        {
            maxHealth = enemyData.enemyHealth;
            health = maxHealth;
            damageAmount = enemyData.enemyDamage;
        }

        healthBar.UpdateHealthBar(health, maxHealth);

        moveFrequency = Random.Range(0.5f, 3f);
        moveAmplitude = Random.Range(0.5f, 2f);
        randomMovementDirection = Random.insideUnitSphere;
        UpdateTarget();
    }

    private void Update()
    {
        UpdateTarget();

        if (target != null)
        {
            MoveTowardsTarget();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Missile"))
        {
            Projectile projectile = other.GetComponent<Projectile>();
            if (projectile != null)
            {
                TakeDamage(projectile.damage);
            }
            Destroy(other.gameObject);
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        previousTarget = target;
    }

    private void MoveTowardsTarget()
    {
        if (target == null) return;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (target.CompareTag("Drone") && distanceToTarget <= stopDistanceFromDrone)
        {
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        Vector3 randomOffset = new Vector3(
            Mathf.Sin(Time.time * moveFrequency) * moveAmplitude * randomMovementDirection.x,
            0,
            Mathf.Sin(Time.time * moveFrequency) * moveAmplitude * randomMovementDirection.z
        );

        Vector3 finalDirection = direction + randomOffset;

        rb.MovePosition(transform.position + finalDirection * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        if (enemyData != null)
        {
            Instantiate(lootBag, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
        GameObject particleSystemInstance = Instantiate(_particleSystem, transform.position, Quaternion.identity);
        ParticleSystem particleSystem = particleSystemInstance.GetComponent<ParticleSystem>();
        particleSystem.Play();

        Destroy(particleSystemInstance, particleSystem.main.duration);
        killCounterData.IncrementKillCount();
    }

    private void UpdateTarget()
    {
        if (playerDrone != null && playerDrone.gameObject.activeSelf)
        {
            target = playerDrone.transform;
            previousTarget = playerDrone.transform; 
        }
        else
        {
            if (target == null || target.CompareTag("Drone"))
            {
                if (previousTarget != null)
                {
                    target = previousTarget;
                }
                else
                {
                    GameObject[] turrets = GameObject.FindGameObjectsWithTag("Target");
                    if (turrets.Length > 0)
                    {
                        target = turrets[0].transform;
                        previousTarget = target; 
                    }
                }
            }
        }
    }
}
