using ProjectBase.Core;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloatingImageAnimator : MonoBehaviour
{
    public Transform mainCam;
    public Transform target;
    public Transform worldSpaceCanvas;
    public TextMeshProUGUI amount_Text;
    public Vector3 offset;
    public float duration = 1f;
    public float displayDuration = 2f;

    private void Awake()
    {
        mainCam = Camera.main.transform;
        worldSpaceCanvas = GameObject.FindObjectOfType<Canvas>().transform;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCam.position);

        transform.position = target.position + offset;
    }

    public void StartImageAnimation(Transform targetPosition)
    {
        target = targetPosition;
        transform.SetParent(worldSpaceCanvas);
        StartCoroutine(AnimateToTarget(targetPosition));
    }

    private System.Collections.IEnumerator AnimateToTarget(Transform targetPosition)
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = targetPosition.position + offset;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;
        GameManager.Instance.PlayerData.IncreaseCurrencyData(ExchangeType.Coin, Take_Coin.coinValue);
        Destroy(gameObject);
    }
}
