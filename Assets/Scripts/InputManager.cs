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
    }

    private void OnEnable()
    {
        _playerInput = new PlayerInput();
        _playerInput.Player.Enable();

        _playerInput.Player.Move.performed += ctx => _movementVector = ctx.ReadValue<Vector2>();
        _playerInput.Player.Move.canceled += ctx => _movementVector = Vector2.zero;
        // _playerInput.Player.MoveTouchX.performed += GetTouch;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = _playerInput.Player.MoveTouchX.ReadValue<Vector2>();
        screenPos.z = _camera.nearClipPlane;
        
        _touchVector = _camera.ScreenToWorldPoint(screenPos);
       // Debug.Log(_touchVector);
    }

    public void GetTouch(InputAction.CallbackContext ctx)
    {
        Vector3 screenPos = _playerInput.Player.MoveTouchX.ReadValue<Vector2>();
        screenPos.z = _camera.nearClipPlane;
        
        _touchVector = _camera.ScreenToWorldPoint(screenPos);
    }
    public Vector2 Movement()
    {
        return _movementVector;
    }

}
