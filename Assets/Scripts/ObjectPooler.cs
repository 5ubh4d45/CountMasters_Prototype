using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] public List<Pool> pools;
    
    private Dictionary<string, Queue<GameObject>> _poolDictionary;

    
    [System.Serializable]
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
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.name = pool.prefab.name + "_" + i;
                
                poolObj.Enqueue(obj);
            }
            
            _poolDictionary.Add(pool.tag, poolObj);
        }
    }

    public void SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!_poolDictionary.ContainsKey(tag))
        {
            Debug.Log("Pool with tag " + tag + " Doesn't exists!");
            return;
        }
        
        GameObject objToSpawn = _poolDictionary[tag].Dequeue();

        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        IPooledObjects pooledObj = objToSpawn.GetComponent<IPooledObjects>();

        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }
        
        objToSpawn.SetActive(true);
        
        _poolDictionary[tag].Enqueue(objToSpawn);
    }
    
}

public interface IPooledObjects
{
    void OnObjectSpawn();
}
