using DG.Tweening;
using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

public class Hook : MonoBehaviour
{
    [Title("General")]
    public GameObject hookPrefab;
    public float hookSpeed;
    public float hookRange;
    [SerializeField] private Transform mainobject;
    private bool isHookEmpty = true;
    [SerializeField] AsteroidMachine asteroidMachine;

    [Title("Animation")]
    public ParticleSystem rocketParticle;
    public Transform particleSpawnPoint;
    public float takeOffHeight = 5f;
    public float takeOffDuration = 1f;
    public float landingDuration = 1f;
    public Vector3 particleRotation = new Vector3(0f, 180f, 0f);

    void Start()
    {
        StartCoroutine(AutoMine());
    }

    IEnumerator AutoMine()
    {
        while (true)
        {
            if (isHookEmpty)
            {
                yield return StartCoroutine(LaunchHookCoroutine());
            }
            yield return null;
        }
    }

    IEnumerator LaunchHookCoroutine()
    {
        GameObject closestMaden = FindClosestMaden();

        if (closestMaden != null)
        {
            isHookEmpty = false;

            ParticleSystem instantiatedParticle = Instantiate(rocketParticle, particleSpawnPoint.position, Quaternion.Euler(particleRotation));
            instantiatedParticle.transform.SetParent(transform);
            instantiatedParticle.gameObject.SetActive(true);
            instantiatedParticle.Play();
            instantiatedParticle.transform.localScale = Vector3.one;

            yield return transform.DOMove(transform.position + Vector3.up * takeOffHeight, takeOffDuration).WaitForCompletion();

            Asteroid asteroidComponent = closestMaden.GetComponent<Asteroid>();
            if (asteroidComponent != null)
            {
                float distance = Vector3.Distance(transform.position, closestMaden.transform.position);

                if (distance > hookRange)
                {
                    isHookEmpty = true;
                    Destroy(instantiatedParticle.gameObject);
                    yield break;
                }

                float asteroidPullTime = asteroidComponent.AsteroidData.pullTime;
                float newHookSpeed = hookSpeed / asteroidPullTime;
                float duration = distance / newHookSpeed;

                // Hook > maden
                Tween moveToMaden = transform.DOMove(closestMaden.transform.position, 2f)
                    .SetEase(Ease.InOutSine); 

                yield return moveToMaden.WaitForCompletion();

                closestMaden.transform.SetParent(transform);
                // Maden > hook > makine
                Tween moveToMain = transform.DOMove(mainobject.position, asteroidPullTime +1f)
                    .SetEase(Ease.InOutSine); 

                yield return moveToMain.WaitForCompletion();

                isHookEmpty = true;
                asteroidMachine.AddAstreoid(closestMaden.GetComponent<Asteroid>());
                closestMaden.SetActive(false);
                Destroy(instantiatedParticle.gameObject);

                yield return transform.DOMove(mainobject.position, landingDuration).WaitForCompletion();
            }
            else
            {
                isHookEmpty = true;
                Destroy(instantiatedParticle.gameObject);
            }
        }
        else
        {
            isHookEmpty = true;
        }
    }

    GameObject FindClosestMaden()
    {
        GameObject[] madenObjects = GameObject.FindGameObjectsWithTag("Maden");
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject obj in madenObjects)
        {
            float distance = Vector3.Distance(obj.transform.position, currentPosition);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj;
            }
        }

        return closestObject;
    }
}
