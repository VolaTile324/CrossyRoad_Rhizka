using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Terrain
{
    [SerializeField] List<GameObject> treePrefabList;
    [SerializeField, Range(0, 1)] float treeProbability;

    public override void Generate(int size)
    {
        base.Generate(size);
        var limit = Mathf.FloorToInt((float)size / 2);
        var treeCount = Mathf.FloorToInt((float)size * treeProbability);
        List<int> emptyPosition = new List<int>();

        // buat daftar posisi kosong
        for(int i = -limit; i <= limit; i++)
        {
            emptyPosition.Add(i);
        }

        for(int i = 0; i < treeCount; i++)
        {
            // cari posisi kosong secara random
            var randomIndex = Random.Range(0, emptyPosition.Count);
            var xPos = emptyPosition[randomIndex];

            // hapus posisi dari daftar
            emptyPosition.RemoveAt(randomIndex);

            // spawn tree tapi random prefab dan rotasi
            SpawnRandomTree(xPos);
        }

        // boundary always spawn tree
        SpawnRandomTree(-limit - 1);
        SpawnRandomTree(limit + 1);

        Debug.Log(string.Join(", ", emptyPosition));
    }

    private void SpawnRandomTree(int xPos)
    {
        var randomIndex = Random.Range(0, treePrefabList.Count);
        var prefab = treePrefabList[randomIndex];

        // instantiate tree, with random rotation
        var tree = Instantiate(prefab,
            new Vector3(xPos, 0, this.transform.position.z),
            Quaternion.Euler(0, Random.Range(0, 360), 0),
            this.transform);
    }

    public void SetTreePercentage(float newPercentage)
    {
        treeProbability = newPercentage;
    }
}
