using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBulletPool : MonoBehaviour
{
    public static EnemyBulletPool instance;

    public List<GameObject> pooledEnemyBullets = new List<GameObject>();
    private int amountToPool = 50;

    [SerializeField] private GameObject enemyBulletsPrefab;
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
            GameObject obj = Instantiate(enemyBulletsPrefab);
            obj.SetActive(false);
            pooledEnemyBullets.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledEnemyBullets.Count; i++)
        {
            if (!pooledEnemyBullets[i].activeInHierarchy)
            {
                return pooledEnemyBullets[i];
            }
        }
        return null;
    }
}
