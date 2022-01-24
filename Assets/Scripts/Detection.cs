using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    [SerializeField] private TeamMaker teamMaker;
    [SerializeField] private Runner runner;

    [Space] [SerializeField] private LayerMask gateLayer;
    [SerializeField] private float detectionSphereRadius;

    public float DetectionSpehereRadius => detectionSphereRadius;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectGates();
        DeathCheck();
    }

    private void DetectGates()
    {
        detectionSphereRadius = teamMaker.GetTeamRadius();
        
        Collider[] detectedGates = Physics.OverlapSphere(transform.position, detectionSphereRadius, gateLayer);
        
        if (detectedGates.Length <= 0) return;

        // getting the first collider gate in case of overlapping 
        Collider detectedGate = detectedGates[0];

        Gates gate = detectedGate.GetComponentInParent<Gates>();

        int runnersToAdd = gate.GetRunnerAmount(detectedGate, teamMaker.CurrentRunnerAmount);
        
        teamMaker.AddRunners(runnersToAdd);
        
        ScoreManager.Instance.AddScore(runnersToAdd * 100);
        
        
        //temp gate check
        // detectedGate.gameObject.SetActive(false);
        Debug.Log("Gates detected spawning!!");

    }

    private void DeathCheck()
    {
        //temp checks for death and signals temp UI to pause
        if (teamMaker.IsDead)
        {
            GetComponent<PlayerMovement>().speedFactor = 0f;
            UIManager.Instance.PlayerDead();
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionSphereRadius);
        Gizmos.color = Color.green;
    }
}
