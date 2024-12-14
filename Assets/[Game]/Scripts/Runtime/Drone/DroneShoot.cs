using System.Collections.Generic;
using UnityEngine;

public class DroneShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public float range = 10f;
    private float nextFireTime = 0f;
    [SerializeField] private float damage = 10f;

    private Transform target;
    private List<GameObject> projectilePool;
    public int poolSize = 50; 

    void Start()
    {
        InitializeProjectilePool();
    }

    void Update()
    {
        if (target == null || Vector3.Distance(transform.position, target.position) > range)
        {
            FindTarget();
        }

        if (target != null && Time.time >= nextFireTime)
        {
            FireProjectile(target.gameObject);
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void InitializeProjectilePool()
    {
        projectilePool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.SetActive(false);
            projectilePool.Add(projectile);
        }
    }

    void FireProjectile(GameObject target)
    {
        if (target == null) return;

        GameObject projectile = GetPooledProjectile();
        if (projectile != null)
        {
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = firePoint.rotation * Quaternion.Euler(0f, 90f, 45f);

            projectile.SetActive(true);

            Vector3 direction = (target.transform.position - firePoint.position).normalized;
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.SetDirection(direction);
            }
        }
    }

    GameObject GetPooledProjectile()
    {
        foreach (var projectile in projectilePool)
        {
            if (projectile != null && !projectile.activeInHierarchy)
            {
                return projectile;
            }
        }
        GameObject newProjectile = Instantiate(projectilePrefab);
        newProjectile.SetActive(false);
        projectilePool.Add(newProjectile);
        return newProjectile;
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
        }

        target = (nearestEnemy != null && shortestDistance <= range) ? nearestEnemy.transform : null;
    }
}
