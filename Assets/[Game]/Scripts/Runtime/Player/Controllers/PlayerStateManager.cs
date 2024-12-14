using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    [SerializeField]private PlayerMovementController _playerMovementController;

    public PlayerMovementController PlayerMovementController { get { return _playerMovementController; } }

    [SerializeField] private PlayerAnimationController _animationController;
    public PlayerAnimationController AnimationController { get { return _animationController; } }

    [SerializeField] private PlayerInventory _playerInventory;
    public PlayerInventory PlayerInventory { get { return _playerInventory; } }
    private IPlayerState _currentState;
    private void Awake()
    {
        _currentState = new IdleState();
        _currentState.EnterState(this);
    }
    private void Update()
    {
        _currentState.UpdateState(this);

    }
    public void ChangeState(IPlayerState newState)
    {
        _currentState.ExitState(this);
        _currentState = newState;
        _currentState.EnterState(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DroneControlZone")) 
        {
            ChangeState(new DroneControlState());
        }
    }

}
