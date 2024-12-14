using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Data", menuName = "Create Bullet Data")]
public class BulletSO : ScriptableObject
{
    public string bulletName;
    public int bulletDamage;
    public float bulletSpeed;
    public float bulletDuration;
    public GameObject bulletObject;
    public LineRenderer lineRendererPrefab;
    public ParticleSystem impactEffectPrefab;
}
