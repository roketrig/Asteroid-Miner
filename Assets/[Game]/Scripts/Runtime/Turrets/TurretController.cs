using DG.Tweening;
using EpicToonFX;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;

    [Title("General")]
    public float range = 15f;

    [Title("Bullet Data")]
    public BulletSO defaultBulletData;
    [ReadOnly, SerializeField] private BulletSO temporaryBulletData;
    [ReadOnly, SerializeField] private bool _isTempBulletActive = false;
    [ReadOnly, SerializeField] private List<Ammo> _stackedBullets;
    private BulletSO currentBulletData;

    [Title("Firing Settings")]
    public float fireRate = 0.5f;
    private float fireCountdown = 0f;
    private float damageInterval = 0.5f;
    private float damageTimer = 1f;
    private LineRenderer lineRenderer;
    private ParticleSystem impactEffect;

    [Title("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float turnSpeed = 10f;
    public Transform firePoint;
    public Transform laserParent;
    private bool isTemporaryLaserActive = false;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        InstantiateLaser(defaultBulletData);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if (target == null)
        {
            if (lineRenderer != null && lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
                impactEffect.Stop();
            }
            return;
        }

        LockOnTarget();

        if (currentBulletData.lineRendererPrefab != null && isTemporaryLaserActive)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
    }


    public void ReceiveAmmo(Ammo ammo)
    {
        _stackedBullets.Add(ammo);

        if (!_isTempBulletActive)
        {
            ammo.transform.DOJump(partToRotate.transform.position, 3, 1, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                ammo.gameObject.SetActive(false);
            });
            _isTempBulletActive = true;
            temporaryBulletData = ammo.bulletData;
            ActivateTemporaryLaser(ammo, 3f);
        }
    }

    private void CheckStackedBullets()
    {
        if (_stackedBullets.Count > 0 && _isTempBulletActive)
        {
            _isTempBulletActive = true;
            _stackedBullets[0].transform.DOJump(partToRotate.transform.position, 3, 1, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                _stackedBullets[0].gameObject.SetActive(false);
            });
            temporaryBulletData = _stackedBullets[0].bulletData;
            ActivateTemporaryLaser(_stackedBullets[0], 3f);
        }
        else
        {
            _isTempBulletActive = false;
        }
    }

    void LockOnTarget()
    {
        if (target == null) return;
        Vector3 targetCenter = target.position + Vector3.up * target.GetComponent<Collider>().bounds.extents.y;
        Vector3 dir = targetCenter - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        if (target == null) return;

        RaycastHit hit;
        Vector3 targetCenter = target.position + Vector3.up * target.GetComponent<Collider>().bounds.extents.y;
        Vector3 direction = (targetCenter - firePoint.position).normalized;

        if (Physics.Raycast(firePoint.position, direction, out hit, range))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null && damageTimer <= 0f)
                {
                    enemy.TakeDamage(currentBulletData.bulletDamage);
                    damageTimer = damageInterval;
                }
            }

            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hit.point);

            if (!lineRenderer.enabled)
                lineRenderer.enabled = true;

            impactEffect.transform.position = hit.point;
            impactEffect.transform.rotation = Quaternion.LookRotation(direction);
            if (!impactEffect.isPlaying)
                impactEffect.Play();
        }
        else
        {
            Vector3 endPos = firePoint.position + direction * range;
            lineRenderer.SetPosition(1, endPos);

            if (lineRenderer.enabled)
                lineRenderer.enabled = false;

            impactEffect.Stop();
        }

        if (damageTimer > 0f)
        {
            damageTimer -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(currentBulletData.bulletObject, firePoint.position, firePoint.rotation);
        ETFXProjectileScript bulletScript = bulletGO.GetComponent<ETFXProjectileScript>();
        Rigidbody rb = bulletGO.GetComponent<Rigidbody>();
        Projectile projectileScript = bulletGO.GetComponent<Projectile>();

        if (rb == null)
        {
            rb = bulletGO.AddComponent<Rigidbody>();
            rb.useGravity = false;
        }

        Vector3 direction = (target.position - firePoint.position).normalized;
        float speed = currentBulletData.bulletSpeed;
        rb.velocity = direction * speed;
        Destroy(bulletGO, 5f);
    }



    public void ActivateTemporaryLaser(Ammo newAmmo, float duration)
    {
        if (!isTemporaryLaserActive)
        {
            StartCoroutine(TemporaryLaserCoroutine(newAmmo, duration));
        }
    }

    IEnumerator TemporaryLaserCoroutine(Ammo ammo, float duration)
    {
        isTemporaryLaserActive = true;
        DestroyLaser();
        InstantiateLaser(ammo.bulletData);
        yield return new WaitForSeconds(duration);
        _stackedBullets.Remove(ammo);
        DestroyLaser();
        InstantiateLaser(defaultBulletData);
        isTemporaryLaserActive = false;
        temporaryBulletData = null;
        CheckStackedBullets();
    }

    void InstantiateLaser(BulletSO bulletData)
    {
        currentBulletData = bulletData;

        if (bulletData.lineRendererPrefab != null)
        {
            lineRenderer = Instantiate(bulletData.lineRendererPrefab, laserParent != null ? laserParent : transform);
        }

        if (bulletData.impactEffectPrefab != null)
        {
            impactEffect = Instantiate(bulletData.impactEffectPrefab, laserParent != null ? laserParent : transform);
        }
    }

    void DestroyLaser()
    {
        if (lineRenderer != null)
        {
            Destroy(lineRenderer.gameObject);
        }

        if (impactEffect != null)
        {
            Destroy(impactEffect.gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
