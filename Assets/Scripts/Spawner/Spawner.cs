using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner<T> : MonoBehaviour where T : UnityEngine.Component
{
    protected List<T> createData = new List<T>();
    protected List<T> spawnData = new List<T>();

    protected Coroutine co_Spawn;

    public event Action<T> OnSpawn;

    public Transform spawnPos;

    protected bool isSpawn = false;
    protected virtual void Awake()
    {
    }
    protected virtual void Start()
    {
        Init();

    }
    private void Init()
    {
        T[] cars = Resources.LoadAll<T>($"Prefabs/{typeof(T).Name}");

        for(int i = 0; i < cars.Length; i++)
        {
            createData.Add(cars[i]);
        }
        foreach (T car in spawnData)
        {
            PoolManager.Instance.CreatePool<T>(car.gameObject);
        }
    }

    public void Spawn(T sp)
    {
        OnSpawn?.Invoke(sp);
    }
    public void StopSpawn() => isSpawn = false;




    protected void ClearAllData()
    {
        for(int i = 0; i < spawnData.Count; i++)
        {
            PoolManager.Instance.Push<T>(spawnData[i]);
        }
        spawnData.Clear();
    }
}
