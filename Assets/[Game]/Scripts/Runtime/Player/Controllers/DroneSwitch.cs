using System.Collections;
using UnityEngine;
using Cinemachine;
using TMPro;

public class DroneSwitch : MonoBehaviour
{
    public GameObject DronePrefab;
    public Transform SpawnPoint;
    public PlayerMovementController Playercontroller;
    public PlayerStateManager PlayerStateManager;
    public float DroneActiveDuration = 20f;
    public float CooldownDuration = 30f;

    public TextMeshPro cooldownText;
    public Transform playerTransform;

    public CinemachineVirtualCamera followCamera;

    private GameObject activeDrone;
    private bool canSwitchToDrone = true;
    private BoxCollider triggerCollider;

    private int originalCameraPriority;

    private void Start()
    {
        triggerCollider = GetComponent<BoxCollider>();
        UpdateCooldownText(-1);
    }

    private void Update()
    {
        if (cooldownText != null && playerTransform != null)
        {
            cooldownText.transform.LookAt(playerTransform);
            cooldownText.transform.Rotate(0, 180, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canSwitchToDrone)
        {
            ActivateDrone();
        }
    }

    private void ActivateDrone()
    {
        if (activeDrone == null)
        {
            if (PlayerStateManager != null)
            {
                PlayerStateManager.gameObject.SetActive(true);
            }

            activeDrone = Instantiate(DronePrefab, SpawnPoint.position, SpawnPoint.rotation);
            DroneController droneController = activeDrone.GetComponent<DroneController>();

            if (droneController != null)
            {
                droneController.EnableDroneControl();

                if (followCamera != null)
                {
                    followCamera.LookAt = activeDrone.transform;

                    Vector3 offset = new Vector3(0, 36, -7); 
                    followCamera.transform.position = activeDrone.transform.position + offset;
                    followCamera.transform.LookAt(activeDrone.transform);

                    originalCameraPriority = followCamera.Priority;
                    followCamera.Priority = 100;
                }
            }
            else
            {
                Debug.LogError("DroneController component not found on the instantiated drone.");
            }

            Playercontroller.DisablePlayerControl();
            StartCoroutine(SwitchBackToPlayerAfterDelay(DroneActiveDuration));
        }
    }


    private IEnumerator SwitchBackToPlayerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (activeDrone != null)
        {
            DroneController droneController = activeDrone.GetComponent<DroneController>();
            if (droneController != null)
            {
                droneController.DisableDroneControl();
            }

            if (followCamera != null)
            {
                followCamera.Priority = 0;
            }

            Destroy(activeDrone);
            activeDrone = null;
        }

        if (PlayerStateManager != null)
        {
            PlayerStateManager.gameObject.SetActive(true);
        }

        Playercontroller.EnablePlayerControl();
        StartCoroutine(StartCooldown(CooldownDuration));
    }

    private IEnumerator StartCooldown(float cooldownTime)
    {
        canSwitchToDrone = false;
        if (triggerCollider != null)
        {
            triggerCollider.enabled = false;
        }

        float remainingTime = cooldownTime;

        while (remainingTime > 0)
        {
            UpdateCooldownText(remainingTime);
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
        }

        UpdateCooldownText(0);
        cooldownText.text = "Drone Ready To Use";

        canSwitchToDrone = true;
        if (triggerCollider != null)
        {
            triggerCollider.enabled = true;
        }
    }

    private void UpdateCooldownText(float time)
    {
        if (cooldownText != null)
        {
            if (time < 0)
            {
                cooldownText.text = "Drone Ready To Use";
            }
            else
            {
                cooldownText.text = Mathf.Ceil(time).ToString();
            }
        }
    }
}
