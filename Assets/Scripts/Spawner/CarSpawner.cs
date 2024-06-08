using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class CarSpawner : Spawner<Car>
{
    public float spawnTime = 5f;
    private float zPos = 0;
    protected override void Start()
    {
        base.Start();

        if (co_Spawn != null)
            StopCoroutine(co_Spawn);

        co_Spawn = StartCoroutine(Spawn(spawnTime));
    }

    protected IEnumerator Spawn(float time)
    {
        isSpawn = true;
        while (isSpawn)
        {
            int random = Random.Range(0, createData.Count);
            Car obj = PoolManager.Instance.Pop<Car>(createData[random].gameObject, spawnPos);
            zPos = Random.Range(-18f, 3f);
            obj.transform.localPosition = new Vector3(0, 0, zPos);
            spawnData.Add(obj);
            yield return CoroutineHelper.WaitForSeconds(time);
        }

        yield break;
    }

}
