using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryingIdleState : IPlayerState
{
    public void EnterState(PlayerStateManager playerStateManager)
    {
        playerStateManager.AnimationController.ChangeAnimation("Carrying_idle");
        playerStateManager.PlayerMovementController.StopPlayer();
    }

    public void ExitState(PlayerStateManager playerStateManager)
    {
    }

    public void UpdateState(PlayerStateManager playerStateManager)
    {
        if (playerStateManager.PlayerInventory.IsCarrying)
        {
            if (playerStateManager.PlayerMovementController.IsMoving)
            {
                playerStateManager.ChangeState(new CarryingState());
            }
        }
        if (!playerStateManager.PlayerInventory.IsCarrying)
        {
            if (playerStateManager.PlayerMovementController.IsMoving)
            {
                playerStateManager.ChangeState(new RunState());
            }
            else
            {
                playerStateManager.ChangeState(new IdleState());
            }
        }

    }
}
