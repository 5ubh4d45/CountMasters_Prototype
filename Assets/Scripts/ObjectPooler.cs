using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private List<Pool> pools;
    [SerializeField] private Transform runnerPoolParent;

    private Dictionary<string, Queue<GameObject> > _poolDictionary;

    
    [Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singeltone
    
    public static ObjectPooler Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> poolObj = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, runnerPoolParent);
                obj.SetActive(false);
                obj.name = pool.prefab.name + "_" + i;
                
                poolObj.Enqueue(obj);
            }
            
            _poolDictionary.Add(pool.tag, poolObj);
        }
    }

    public void SpawnFromPool(string objTag, Transform parent, Quaternion rotation)
    {
        if (!_poolDictionary.ContainsKey(objTag))
        {
            Debug.Log("Pool with tag " + objTag + " Doesn't exists!");
            return;
        }
        
        GameObject objToSpawn = _poolDictionary[objTag].Dequeue();

        objToSpawn.transform.parent = parent;
        objToSpawn.transform.position = parent.position;
        objToSpawn.transform.rotation = rotation;

        IPooledObjects pooledObj = objToSpawn.GetComponent<IPooledObjects>();

        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }
        
        objToSpawn.SetActive(true);
        
    }

    public void DestroyRunner(string objTag, GameObject objToDestroy)
    {
        if (!_poolDictionary.ContainsKey(objTag))
        {
            Debug.Log("Pool with tag " + objTag + " Doesn't exists!");
            return;
        }

        objToDestroy.SetActive(false);
        objToDestroy.transform.parent = runnerPoolParent;
        _poolDictionary[objTag].Enqueue(objToDestroy);
    }

    // private void Update()
    // {
    //     Debug.Log("Current Queue Count: " + _poolDictionary[pools[0].tag].Count);
    // }
}

public interface IPooledObjects
{
    void OnObjectSpawn();
}
