using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    // [SerializeField] private Collider collider;
    // [SerializeField] private Renderer renderer;
    // [SerializeField] private LayerMask obstacleLayer;

    // [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private string obstacleTag;
    
    private bool _isMoving;
    private float oldPositionZ;
    public bool IsMoving => _isMoving;

    // Start is called before the first frame update
    void Start()
    {
        oldPositionZ = transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //height Check
        if (transform.position.y < -3)
        {
            DestroyRunner();
        }
        CheckMovement();
    }
    

    private void OnTriggerEnter(Collider other)
    {
        //detection for collision
        if (other.CompareTag(obstacleTag))
        {
            Debug.Log("Obstacle collided");
            // Destroy(this.gameObject);
            DestroyRunner();
        }
    }

    private void DestroyRunner()
    {
        this.GetComponent<Collider>().enabled = false;
        this.GetComponent<Renderer>().enabled = false;
        
        Destroy(gameObject);
    }

    private void CheckMovement()
    {
        float posZ = transform.position.z;

        _isMoving = posZ > oldPositionZ;

        oldPositionZ = posZ;

    }
    
}
