using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private PlayerInput _playerInput;

    public static InputManager Instance;

    private Vector2 _movementVector;
    public Vector2 Movement => _movementVector;

    private bool _isSpace;
    public bool IsSpace => _isSpace;
    
    private Vector3 _touchVector;

    private InputAction.CallbackContext _ctx;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Player.Enable();

        // _playerInput.Player.Move.performed += ctx => _movementVector = ctx.ReadValue<Vector2>();
        // _playerInput.Player.Move.canceled += ctx => _movementVector = Vector2.zero;
        // _playerInput.Player.MoveTouchX.performed += GetTouch;
    }

    private void OnDisable()
    {
        _playerInput.Player.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        _movementVector = _playerInput.Player.Move.ReadValue<Vector2>();
        _isSpace = _playerInput.Player.Space.WasReleasedThisFrame();
        
        Vector3 screenPos = _playerInput.Player.MoveTouchX.ReadValue<Vector2>();
        screenPos.z = _camera.nearClipPlane;
        
        _touchVector = _camera.ScreenToWorldPoint(screenPos);
        // Debug.Log(_movementVector);
    }

    public void GetTouch(InputAction.CallbackContext ctx)
    {
        Vector3 screenPos = _playerInput.Player.MoveTouchX.ReadValue<Vector2>();
        screenPos.z = _camera.nearClipPlane;
        
        _touchVector = _camera.ScreenToWorldPoint(screenPos);
    }
    

}
