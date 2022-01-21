using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeamMaker : MonoBehaviour
{

    [SerializeField] private TextMeshPro runnerCountText;
    
    [Header(" Formation Settings ")]
    [Range(0f, 1f)]
    [SerializeField] private float radiusFactor;
    [Range(0f, 1f)]
    [SerializeField] private float angleFactor;

    [SerializeField] private float baseAngle = 90f;


    [SerializeField] private GameObject runner;
    [SerializeField] private int currentRunnerAmount;
    public int CurrentRunnerAmount => currentRunnerAmount;
    
    private float _teamRadius;
    
    // Start is called before the first frame update
    void Start()
    {
        // AddRunners(runnerAmount);
    }

    // Update is called once per frame
    void Update()
    {
        // ArrangeRunners();

        currentRunnerAmount = transform.childCount;
    }
    
    private void ArrangeRunners()
    {
        // float goldenAngle = 137.5f * angleFactor;

        float baseAngleRad = Mathf.Deg2Rad * baseAngle;
        
        for (int i = 0; i < transform.childCount; i++)
        {
            float x = radiusFactor * Mathf.Sqrt(i + 1) * Mathf.Cos(angleFactor * baseAngleRad * (i + 1));
            float z = radiusFactor * Mathf.Sqrt(i + 1) * Mathf.Sin(angleFactor * baseAngleRad * (i + 1));
        
            Vector3 runnerLocalPosition = new Vector3(x, 0, z);
            transform.GetChild(i).localPosition = Vector3.Lerp(transform.GetChild(i).localPosition, runnerLocalPosition, 0.1f);
        }
        
    }

    public void AddRunners(int amountToSpawn)
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            GameObject runnerSpawnInstance = Instantiate(runner, transform);

            runnerSpawnInstance.name = "Runner_" + runnerSpawnInstance.transform.GetSiblingIndex();
            
            ArrangeRunners();
        }
    }

    public float GetTeamRadius()
    {
        _teamRadius = radiusFactor * Mathf.Sqrt(transform.childCount);
        return _teamRadius;
    }
}
