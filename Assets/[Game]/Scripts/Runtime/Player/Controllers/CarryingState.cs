using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryingState : IPlayerState
{
    public void EnterState(PlayerStateManager playerStateManager)
    {

        playerStateManager.AnimationController.ChangeAnimation("Carrying");
    }

    public void ExitState(PlayerStateManager playerStateManager)
    {
    }

    public void UpdateState(PlayerStateManager playerStateManager)
    {
        playerStateManager.PlayerMovementController.MovePlayer();
        if (!playerStateManager.PlayerInventory.IsCarrying)
        {

            playerStateManager.ChangeState(new IdleState());
        }
        else if (playerStateManager.PlayerInventory.IsCarrying)
        {

            if (!playerStateManager.PlayerMovementController.IsMoving)
            {
                playerStateManager.ChangeState(new CarryingIdleState());
            }
        }
    }
}
