using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    // [SerializeField] private Collider collider;
    // [SerializeField] private Renderer renderer;
    [SerializeField] private LayerMask obstacleLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    private void OnTriggerEnter(Collider other)
    {
        //detection for collision
        if (other.CompareTag("Finish"))
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
    
}
