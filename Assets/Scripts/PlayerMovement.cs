using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform cube;
    
    [SerializeField] private float zAxisSpeed = 3f;
    [SerializeField] private float xAxisSpeed = 0.1f;

    [SerializeField] private Vector2 xAxisMovementClamp;

    [SerializeField] private bool isTouchEnabled;
    private float _mouseX;
    private float _mouseY;
    
    [HideInInspector]
    public float speedFactor = 1f;
    
    private bool _gettingInputs;
    private bool _canMove;
    

    private Vector2 _actualClamping;

    private Detection _detection;
    
    // Start is called before the first frame update
    void Start()
    {
        _detection = GetComponent<Detection>();
        _canMove = false;
    }

    // Update is called once per frame
    private void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        // RotateCube();
        
        Movement();
    }

    private void ProcessInputs()
    {
        //checks for the touch if disabled then it uses keyboard
        if (!isTouchEnabled)
        {
            // _gettingInputs = true;
            _gettingInputs = Input.GetKeyDown("space");
            if (_gettingInputs)
            {
                _canMove = true;
            }
            
            _mouseX = Input.GetAxis("Horizontal");
            _mouseY = Input.GetAxis("Vertical");
            return;
        }
        
        //gets the touch inputs of the X & Y axis
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            _gettingInputs = true;
            _canMove = _gettingInputs;
            
            // _mouseX = Mathf.Clamp(Input.GetTouch(0).deltaPosition.x, -1f, 1f);
            // _mouseY = Mathf.Clamp(Input.GetTouch(0).deltaPosition.y, -1f, 1f);
            
            _mouseX = Input.GetTouch(0).deltaPosition.x;
            _mouseY = Input.GetTouch(0).deltaPosition.y;
        }
        else
        {
            _mouseX = 0;
            _mouseY = 0;

            // cube.position = new Vector3(Mathf.Lerp(cube.position.x, 0, (float) 1.5), cube.position.y, cube.position.z);
        }
        
    }

    private void CheckClampSize()
    {
        float radius = _detection.DetectionSpehereRadius;
        _actualClamping = new Vector2(xAxisMovementClamp.x + radius, xAxisMovementClamp.y - radius);
        
    }
        
    private void Movement()
    {   
        //moves the cube
        Vector3 oldPos = cube.position;

        CheckClampSize();
        
        // temporary start text disabling
        if (_canMove)
        {
            UIManager.Instance.DisableStartText();
        }
        
        // waits for the first input
        if (_canMove)
        {
            Vector3 newPos = oldPos + new Vector3((xAxisSpeed * _mouseX * speedFactor) * Time.deltaTime, 0, zAxisSpeed * speedFactor * Time.deltaTime);

            newPos.x = Mathf.Clamp(newPos.x, xAxisMovementClamp.x, xAxisMovementClamp.y);
            
            cube.position = newPos;
        }
    }
    

}
