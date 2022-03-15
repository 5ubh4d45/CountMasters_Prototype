using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform cube;
    
    [SerializeField] private float zAxisSpeed = 3f;
    [SerializeField] private float xAxisSpeed = 1f;

    [SerializeField] private Vector2 xAxisMovementClamp;
    
    private Vector2 _movement;
    private Vector3 _newPos;
    private Vector3 _oldPos;
    
    // [HideInInspector]
    public float speedFactor = 1f;
    
    private bool _gettingInputs;
    private bool _canMove;

    private Camera _camera;
    private Vector2 _screenSpacePosition;
    private Vector2 _worldSpacePosition;

    private Vector2 _actualClamping;

    private Detection _detection;

    private InputManager _input;
    
    // Start is called before the first frame update
    private void Start()
    {
        _detection = GetComponent<Detection>();
        _canMove = false;
        _camera = Camera.main;
        _input = InputManager.Instance;
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
        // new InputSystem
        _movement = _input.Movement;
        
        // _gettingInputs = true;
        _gettingInputs = _input.IsSpace || _movement.x != 0;
        if (_gettingInputs)
        {
            _canMove = true;
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
        _oldPos = cube.position;

        CheckClampSize();
        
        // temporary start text disabling
        if (_canMove)
        {
            UIManager.Instance.DisableStartText();
        }
        
        // waits for the first input
        if (_canMove)
        {
            _newPos = _oldPos + new Vector3((xAxisSpeed * _movement.x * speedFactor) * Time.deltaTime, 0, zAxisSpeed * speedFactor * Time.deltaTime);
            

            _newPos.x = Mathf.Clamp(_newPos.x, xAxisMovementClamp.x, xAxisMovementClamp.y);
            
            cube.position = _newPos;
        }
    }

    public IEnumerator RunTowardsMiddle(float duration)
    {
        float tempPosX;
        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            tempPosX = Mathf.Lerp(a: _newPos.x, b: 0, t: timeElapsed/duration);
            transform.position = new Vector3(tempPosX, _newPos.y, _newPos.z);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
    }

}
