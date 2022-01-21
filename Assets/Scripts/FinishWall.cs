using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishWall : MonoBehaviour
{
    [SerializeField] private Vector3 reStartLocation;
    
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
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Cube Touched and finished");
            other.gameObject.transform.position = reStartLocation;
        }
    }
}
