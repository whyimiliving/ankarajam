using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private PlayerControls _playerActions;
    [SerializeField] private InputData _inputData;
    private Vector2 moveInput;

    // Delegate alanları
    private Action<InputAction.CallbackContext> onMovementPerformed;
    private Action<InputAction.CallbackContext> onMovementCanceled;
    
    private void Awake()
    {
        _playerActions = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerActions.Enable();
        SubscribeEvents();
    }

    private void OnDisable()
    {
        _playerActions.Disable();
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
      
        onMovementPerformed = HandleMovementPerformed;
        _playerActions.Player.Movement.performed += onMovementPerformed;

        onMovementCanceled = HandleMovementCanceled;
        _playerActions.Player.Movement.canceled += onMovementCanceled;
        
    }

    private void UnsubscribeEvents()
    {
   
        _playerActions.Player.Movement.performed -= onMovementPerformed;
        _playerActions.Player.Movement.canceled -= onMovementCanceled;
        
    }

    // Movement
    private void HandleMovementPerformed(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
        _inputData.InputVectorX = moveInput.x;
        _inputData.InputVectorY = moveInput.y;
    }

    private void HandleMovementCanceled(InputAction.CallbackContext ctx)
    {
        _inputData.InputVectorX = 0;
        _inputData.InputVectorY = 0;
    }
    
}