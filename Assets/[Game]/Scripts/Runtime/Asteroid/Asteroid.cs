using System.Collections;
using UnityEngine;
using DG.Tweening; // DOTween'i kullanmak için gerekli

public class Asteroid : MonoBehaviour
{
    public AsteroidSO AsteroidData;

    private void Start()
    {
        RotateAsteroid();
    }

    private void RotateAsteroid()
    {
        Vector3 randomRotation = new Vector3(
            Random.Range(0f, 360f), 
            Random.Range(0f, 360f), 
            Random.Range(0f, 360f)  
        );

        transform.DORotate(randomRotation, Random.Range(10f, 15f), RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .OnComplete(RotateAsteroid); 
    }
}
