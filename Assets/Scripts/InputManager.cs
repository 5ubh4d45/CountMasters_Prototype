using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private PlayerInput _playerInput;

    public static InputManager Instance;

    private Vector2 _movementVector2;

    private InputAction.CallbackContext _ctx;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        _playerInput = new PlayerInput();
        _playerInput.Player.Enable();

        _playerInput.Player.Move.performed += ctx => _movementVector2 = ctx.ReadValue<Vector2>();
        _playerInput.Player.Move.canceled += ctx => _movementVector2 = Vector2.zero;

    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    public Vector2 Movement()
    {
        return _movementVector2;
    }

}
