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
    

    [SerializeField] private bool isTouchEnabled;
    private float _mouseX;
    private float _mouseY;
    
    private bool _gettingInputs;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
            _gettingInputs = true;
            
            _mouseX = Input.GetAxis("Horizontal") * 15;
            _mouseY = Input.GetAxis("Vertical") * 15;
            return;
        }
        
        //gets the touch inputs of the X & Y axis
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            _gettingInputs = true;
            
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
        
    private void Movement()
    {   
        //moves the cube
        Vector3 oldPos = cube.position;
        
        // waits for the first input
        if (_gettingInputs)
        {
            Vector3 newPos = oldPos + new Vector3((xAxisSpeed * _mouseX) * Time.deltaTime, 0, zAxisSpeed * Time.deltaTime);
            cube.position = newPos;
        }
        
    }

}
