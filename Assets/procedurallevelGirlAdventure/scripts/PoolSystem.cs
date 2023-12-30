using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSystem : MonoBehaviour {

    public static PoolSystem Instance
    {
        get; private set;
    }
    private Queue<GameObject> availablePlayerBullets = new Queue<GameObject>();
    private Queue<GameObject> availableEnemyBullets = new Queue<GameObject>();

    [SerializeField]
    GameObject playerBulletPrefab;

    [SerializeField]
    GameObject EnemyBulletPrefab;
    private void Awake()
    {
        Instance = this;
        GrowPool(true);
        GrowPool(false);
    }

    private  void GrowPool(bool isPbullet)
    {
        if (isPbullet)
        {
            for (int i = 0; i < 10; i++)
            {
                var InstanceToAdd = Instantiate(playerBulletPrefab);
                InstanceToAdd.transform.SetParent(transform);
                AddtoPool(InstanceToAdd, true);

            }
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                var InstanceToAdd = Instantiate(EnemyBulletPrefab);
                InstanceToAdd.transform.SetParent(transform);
                AddtoPool(InstanceToAdd, false);

            }
        }
    }
    public GameObject getFromPool(bool isPlayerBullet)
    {
        if(availablePlayerBullets.Count ==0)
        {
            GrowPool(true);
        }
        if (availableEnemyBullets.Count == 0)
        {
            GrowPool(false);
        }
        if (isPlayerBullet)
        {
            var instance = availablePlayerBullets.Dequeue();
            instance.SetActive(true);
            return instance;
        }
        else
        {
            var instance = availableEnemyBullets.Dequeue();
            instance.SetActive(true);
            return instance;
        }
    }
    public void AddtoPool(GameObject instance, bool isPlayerB)
    {
        if (isPlayerB)
        {
            instance.SetActive(false);
            availablePlayerBullets.Enqueue(instance);
        }
        else
        {
            instance.SetActive(false);
            availableEnemyBullets.Enqueue(instance);
        }
    }

     
}
