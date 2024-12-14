using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public float range = 10f;
    private float nextFireTime = 0f;
    [SerializeField] private float damage = 10f;

    private Transform target;

    void Update()
    {
        UpdateTarget();

        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.position) <= range)
            {
                if (Time.time >= nextFireTime)
                {
                    FireProjectile(target.gameObject);
                    nextFireTime = Time.time + 1f / fireRate;
                }
            }
        }
    }
    public float GetDamage()
    {
        return damage; 
    }

    void UpdateTarget()
    {
        GameObject drone = GameObject.FindWithTag("Drone");
        if (drone != null)
        {
            target = drone.transform; 
            Debug.Log("Target set to: Drone");
        }
        else
        {
            GameObject closestTurret = FindClosestTurret();
            if (closestTurret != null)
            {
                target = closestTurret.transform;
            }
        }
    }

    GameObject FindClosestTurret()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        GameObject closestTurret = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject turret in turrets)
        {
            float distanceToTurret = Vector3.Distance(transform.position, turret.transform.position);
            if (distanceToTurret < closestDistance)
            {
                closestDistance = distanceToTurret;
                closestTurret = turret;
            }
        }

        return closestTurret;
    }

    void FireProjectile(GameObject target)
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Enemy_Bullet projectileScript = projectile.GetComponent<Enemy_Bullet>();
        projectileScript.SetTarget(target);
    }
}
