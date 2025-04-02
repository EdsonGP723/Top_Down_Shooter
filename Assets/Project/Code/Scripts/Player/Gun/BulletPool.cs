using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public static BulletPool instance;

    private List<GameObject> pooledBullets = new List<GameObject>();
    private int amountToPool = 20;

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
            pooledBullets.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledBullets.Count; i++)
        {
            if (!pooledBullets[i].activeInHierarchy)
            {
                return pooledBullets[i];
            }
        }
        return null;
    }
}
