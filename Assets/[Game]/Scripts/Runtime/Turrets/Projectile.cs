using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage;
    private Vector3 direction;
    private float originalSpeed;
    private int originalDamage;
    public float lifetime = 5f; 

    public void Initialize(BulletSO bulletData, float defaultSpeed, int defaultDamage)
    {
        originalSpeed = defaultSpeed;
        originalDamage = defaultDamage;

        speed = bulletData.bulletSpeed;
        damage = Mathf.RoundToInt(bulletData.bulletDamage);
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction.normalized;
    }

    private void Start()
    {
        StartCoroutine(DestroyAfterLifetime());
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyAfterLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
