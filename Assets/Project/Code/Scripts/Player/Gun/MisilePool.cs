using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MisilePool : MonoBehaviour
{
    public static MisilePool instance;

    public List<GameObject> pooledMisiles = new List<GameObject>();
    public int amountToPool = 2;

    [SerializeField] private GameObject bulletPrefab;
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
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            pooledMisiles.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledMisiles.Count; i++)
        {
            if (!pooledMisiles[i].activeInHierarchy)
            {
                return pooledMisiles[i];
            }
        }
        return null;
    }
}
