using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    
    // [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private string obstacleTag;
    [SerializeField] private GameObject deathParticle;
    
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
            // Debug.Log("Obstacle collided");
            
            DestroyRunner();
        }
    }

    private void DestroyRunner()
    {
        GetComponent<Collider>().enabled = false;
        // GetComponentInChildren<Renderer>().enabled = false;

        Instantiate(deathParticle, transform);
        
        Destroy(gameObject, 0.3f);
    }

    private void CheckMovement()
    {
        float posZ = transform.position.z;

        //checks for movement
        _isMoving = (posZ > oldPositionZ || posZ < oldPositionZ);

        oldPositionZ = posZ;

    }
    
}
