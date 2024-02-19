using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRoomSpawner : MonoBehaviour
{
    [System.Serializable]

    public struct RandomSpawner
    {
        public string name;
        public SpawnerData spawnerData;
    }

    public GridController grid;

    public RandomSpawner[] spawnerData;

    private void Start()
    {
        grid = GetComponent<GridController>();
    }

    public void InitialiseObjectSpawning()
    {
        foreach(RandomSpawner rs in spawnerData)
        {
            SpawnObjects(rs);
        }
    }

    void SpawnObjects(RandomSpawner data)
    {
        int randomInteraction = Random.Range(data.spawnerData.minSpawn, data.spawnerData.maxSpawn + 1);

        for (int i = 0; i < randomInteraction; i ++)
        {
            int randomPos = Random.Range(0, grid.availablePoints.Count - 1);
            GameObject gameObject = Instantiate(data.spawnerData.itemToSpawn, grid.availablePoints[randomPos], Quaternion.identity, transform) as GameObject;
            grid.availablePoints.RemoveAt(randomPos);
        }
    }
}