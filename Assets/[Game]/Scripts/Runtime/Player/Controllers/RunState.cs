using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IPlayerState
{
    public void EnterState(PlayerStateManager playerStateManager)
    {
        playerStateManager.AnimationController.ChangeAnimation("Run");
    }

    public void ExitState(PlayerStateManager playerStateManager)
    {
    }

    public void UpdateState(PlayerStateManager playerStateManager)
    {
        if (playerStateManager.PlayerInventory.IsCarrying)
        {
            playerStateManager.ChangeState(new CarryingState());
        }
        else if (!playerStateManager.PlayerMovementController.IsMoving)
        {
            playerStateManager.ChangeState(new IdleState());
        }
        else
        {
            playerStateManager.PlayerMovementController.MovePlayer();
        }
    }
}
