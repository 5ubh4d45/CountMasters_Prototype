using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform holder;
    [SerializeField] private TeamMaker teamMaker;
    [SerializeField] private Runner runner;

    [Space] [SerializeField] private LayerMask runnerLayer;
    [SerializeField] private float detectionSphereRadius;

    [SerializeField] private float enemySpeed;
    [SerializeField] private float slowDownFactor = 0.1f;

    private bool _engaged;
    private bool _isDead;
    private bool _canMove;
    private PlayerMovement _playerMovement;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectRunner();
        
        CheckDeath();
        
    }

    private void FixedUpdate()
    {
        
        MoveTowardsPlayer();
    }

    private void DetectRunner()
    {
        if (_engaged) return;

        Collider[] runnerColliders = Physics.OverlapSphere(transform.position, detectionSphereRadius, runnerLayer);
        
        if (runnerColliders.Length <= 0) return;
        
        // getting the first collider in case of overlapping 
        Collider runnerCollider = runnerColliders[0];

        _engaged = true;
        
        _playerMovement = runnerCollider.transform.parent.parent.GetComponent<PlayerMovement>();
        
        _playerMovement.speedFactor = slowDownFactor;

        _canMove = true;    // enables enemy movement
        
        Debug.Log("Runners detected Engaging!!");

    }

    private void CheckDeath()
    {
        if (_playerMovement == null || _isDead)return;
        
        if (teamMaker.CurrentRunnerAmount - 2 <= 0)
        {
            _isDead = true;
            _canMove = false;
            slowDownFactor = 1f;
            _playerMovement.speedFactor = slowDownFactor;
            
            Debug.Log("All Enemies Dead");
        }
    }

    private void MoveTowardsPlayer()
    {
        if(_isDead) return;     //checks if enemies are dead

        if (_canMove)
        {
            Vector3 playerPos = _playerMovement.transform.position;
            Vector3 oldPos = holder.transform.position;
            Vector3 direction = Vector3.Normalize(playerPos - oldPos);
            
            // Debug.Log("PlayerPosition: " + playerPos + "\n EnemyPosition: " + transform.position);

            holder.transform.position = Vector3.MoveTowards(oldPos, playerPos, enemySpeed * slowDownFactor * Time.deltaTime);
        }

    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionSphereRadius);
        Gizmos.color = Color.red;
    }
    
}
