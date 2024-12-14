using ProjectBase.Core;
using System.Collections;
using UnityEngine;

public class DroneControlState : IPlayerState
{
    private float controlDuration = 20f;
    private Coroutine droneCoroutine;

    public void EnterState(PlayerStateManager playerStateManager)
    {
        playerStateManager.AnimationController.ChangeAnimation("Idle"); 
        playerStateManager.PlayerMovementController.DisablePlayerControl(); 
        droneCoroutine = playerStateManager.StartCoroutine(ControlDrone(playerStateManager));
    }

    public void UpdateState(PlayerStateManager playerStateManager)
    {
    }

    public void ExitState(PlayerStateManager playerStateManager)
    {
        playerStateManager.PlayerMovementController.EnablePlayerControl(); 
        if (droneCoroutine != null)
        {
            playerStateManager.StopCoroutine(droneCoroutine);
            droneCoroutine = null;
        }
    }

    private IEnumerator ControlDrone(PlayerStateManager playerStateManager)
    {
        float timer = controlDuration;
        DroneController droneController = playerStateManager.GetComponent<DroneController>();

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            if (droneController != null)
            {
                Vector3 inputDirection = InputManager.Instance.InputDirection;
                Vector3 movement = new Vector3(inputDirection.x, 0, inputDirection.z);

                if (movement.magnitude > 1)
                    movement.Normalize();

                droneController.transform.Translate(movement * droneController.speed * Time.deltaTime, Space.World);
            }

            yield return null;
        }
        playerStateManager.ChangeState(new IdleState());
    }
}
