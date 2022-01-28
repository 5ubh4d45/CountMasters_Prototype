using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour, IPooledObjects
{
    
    // [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private string obstacleTag;
    [SerializeField] private GameObject deathParticle;
    [SerializeField] private bool isEnemy;
    
    private bool _isMoving;
    private float _oldPositionZ;
    public bool IsMoving => _isMoving;
    private ObjectPooler _objectPooler;

    // Start is called before the first frame update
    void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }
    
    public void OnObjectSpawn()
    {
        _oldPositionZ = transform.position.z;
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
            
            StartCoroutine(DestroyRunner());
        }
    }

    private IEnumerator DestroyRunner()
    {
        if (isEnemy)
        {
            GetComponent<Collider>().enabled = false;
            // GetComponentInChildren<Renderer>().enabled = false;
            
            Instantiate(deathParticle, transform);
            
            Destroy(gameObject, 0.3f);
            yield return null;
        }

        Instantiate(deathParticle, transform);
            
        Destroy(gameObject, 0.3f);
        yield return new WaitForSeconds(0.3f);
        _objectPooler.DestroyRunner("PlayerRunner", gameObject);
        
    }

    private void CheckMovement()
    {
        float posZ = transform.position.z;

        //checks for movement
        _isMoving = (posZ > _oldPositionZ || posZ < _oldPositionZ);

        _oldPositionZ = posZ;

    }
    
}
