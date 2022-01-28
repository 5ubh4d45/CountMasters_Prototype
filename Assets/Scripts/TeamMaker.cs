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
    [SerializeField] private bool isEnemy;

    [SerializeField] private float baseAngle = 90f;
    [SerializeField] private bool checkFormationRealTime;
    
    [SerializeField] private GameObject runner;
    [SerializeField] private string objectToSpawn;
    [SerializeField] private int currentRunnerAmount = 1;
    
    private bool _isDead;

    public bool IsDead => _isDead;
    public int CurrentRunnerAmount => currentRunnerAmount;
    
    private float _teamRadius;
    private ObjectPooler _objectPooler;

    // Start is called before the first frame update
    void Start()
    {
        AddRunners(currentRunnerAmount - 1);
        _objectPooler = ObjectPooler.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (checkFormationRealTime)
        {
            RunnerArranger();
        }

        currentRunnerAmount = transform.childCount;
        CheckDeathSts();
    }
    
    private void RunnerArranger()
    {
        // float goldenAngle = 137.5f * angleFactor;

        float baseAngleRad = Mathf.Deg2Rad * baseAngle;
        
        for (int i = 0; i < transform.childCount; i++)
        {
            float x = radiusFactor * Mathf.Sqrt(i + 1) * Mathf.Cos(angleFactor * baseAngleRad * (i + 1));
            float z = radiusFactor * Mathf.Sqrt(i + 1) * Mathf.Sin(angleFactor * baseAngleRad * (i + 1));
        
            Vector3 runnerLocalPosition = new Vector3(x, transform.GetChild(i).localPosition.y, z);
            transform.GetChild(i).localPosition = Vector3.Lerp(transform.GetChild(i).localPosition, runnerLocalPosition, 0.1f);
            
        }
        
    }

    public void AddRunners(int amountToSpawn)
    {
        if (isEnemy)
        {
            for (int i = 0; i < amountToSpawn; i++)
            {
                GameObject runnerSpawnInstance = Instantiate(runner, transform);
                
                runnerSpawnInstance.name = "EnemyRunner_" + runnerSpawnInstance.transform.GetSiblingIndex();
                StartCoroutine(ArrangeRunners(1.5f));
            }
            
            return;
        }

        for (int i = 0; i < amountToSpawn; i++)
        {
            _objectPooler.SpawnFromPool(objectToSpawn, this.transform, Quaternion.identity);

            StartCoroutine(ArrangeRunners(1.5f));
        }
        
    }

    private IEnumerator ArrangeRunners(float timeToRun)
    {
        float timePassed = 0;
        while (timePassed < timeToRun)
        {
            RunnerArranger();
            timePassed += Time.deltaTime;

            yield return null;
        }
    }
    

    public float GetTeamRadius()
    {
        _teamRadius = radiusFactor * Mathf.Sqrt(transform.childCount);
        return _teamRadius;
    }
    
    private void CheckDeathSts()
    {
        if (currentRunnerAmount <= 0)
        {
            _isDead = true;
        }
    }
}
