using ProjectBase.Core;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Transform _lookAtTransform;

    private bool _isReadyToMove;
    private Vector2 _inputValues;
    private float _speed = 6f;

    public bool IsMoving { get { return _isReadyToMove; } }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        if (!enabled) return; 

        EventManager.OnInputTaken.AddListener(SetMovementAvailable);
        EventManager.OnInputDragged.AddListener(UpdateInputData);
        EventManager.OnInputReleased.AddListener(SetMovementUnavailable);
    }

    public void SetMovementAvailable()
    {
        _isReadyToMove = true;
    }

    public void SetMovementUnavailable()
    {
        _isReadyToMove = false;
    }

    public void UpdateInputData(Vector2 inputValues)
    {
        _inputValues = inputValues;
    }

    private void UnsubscribeEvents()
    {
        EventManager.OnInputTaken.RemoveListener(SetMovementAvailable);
        EventManager.OnInputDragged.RemoveListener(UpdateInputData);
        EventManager.OnInputReleased.RemoveListener(SetMovementUnavailable);
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    public void MovePlayer()
    {
        if (_isReadyToMove && enabled)
        {
            rigidBody.velocity = new Vector3(_inputValues.x * _speed, rigidBody.velocity.y, _inputValues.y * _speed);
            RotatePlayer();
        }
    }

    public void StopPlayer()
    {
        rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, 0);
        rigidBody.angularVelocity = Vector3.zero;
    }

    public void RotatePlayer()
    {
        var moveDirection = new Vector3(_inputValues.x, 0, _inputValues.y);
        if (moveDirection != Vector3.zero)
        {
            _lookAtTransform.LookAt(transform.position + moveDirection);
        }
    }

    public void DisablePlayerControl()
    {
        enabled = false; 
        StopPlayer();   
    }

    public void EnablePlayerControl()
    {
        enabled = true; 
    }

}
