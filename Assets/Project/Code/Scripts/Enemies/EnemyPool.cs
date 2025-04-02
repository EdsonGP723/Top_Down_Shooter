using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool instance;

    public List<GameObject> pooledEnemies = new List<GameObject>();
    private int amountToPool = 1;

    [SerializeField] private GameObject enemyPrefab;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(enemyPrefab);
            obj.SetActive(false);
            pooledEnemies.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledEnemies.Count; i++)
        {
            if (!pooledEnemies[i].activeInHierarchy)
            {
                return pooledEnemies[i];
            }
        }
        return null;
    }
}
