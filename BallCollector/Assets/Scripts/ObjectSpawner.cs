using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public string[] fallingObjects;
    public List<FallingObjectsData> fallingData = new List<FallingObjectsData>();
    public float minSpawnTime;
    public float maxSpawnTime;
    public Coroutine spawnCo;
    // Start is called before the first frame update
    void Start()
    {
        spawnCo = StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while(GameManager.instance.canPlay)
        {
            SpawnItem();
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        }
    }
    private void SpawnItem()
    {
        string item = GetRandom();
        GameObject obj = PoolManager.instance.GetObject(item);
        obj.SetActive(true);
    }
    public void IncreaseDifficulty()
    {
        minSpawnTime = minSpawnTime <= 0.5f ? 0.5f : minSpawnTime - 0.5f;
        maxSpawnTime = maxSpawnTime <= 1f ? 1f : maxSpawnTime - 0.5f;
    }
    private string GetRandom()
    {
        float totalWeight = 0;

        foreach (FallingObjectsData p in fallingData)
        {
            totalWeight += p.weight;
        }

        float value = Random.value * totalWeight;

        float sumWeight = 0;

        foreach (FallingObjectsData p in fallingData)
        {
            sumWeight += p.weight;

            if (sumWeight >= value)
            {
                return p.name;
            }
        }

        return "";
    }
}
[System.Serializable]
public class FallingObjectsData
{
    public string name;
    public float weight;
}
