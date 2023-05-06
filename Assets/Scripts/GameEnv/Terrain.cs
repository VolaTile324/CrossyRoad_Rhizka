using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    protected int horizontalSize;
    [SerializeField] int boundarySize = 1;

    private void Start()
    {
        // Generate(5);
    }

    public virtual void Generate(int size)
    {
        /* for (int x = 0; x < size; x++)
        {
            for (int z = 0; z < size; z++)
            {
                var tile = Instantiate(tilePrefab, transform);
                tile.transform.localPosition = new Vector3(x, 0, z);
            }
        } */ 
        horizontalSize = size;

        if(size == 0)
        {
            return;
        }

        if((float) size % 2 == 0)
        {
            size -= 1;
        }

        int limit = Mathf.FloorToInt((float)size / 2);

        for(int x = -limit; x <= limit; x++)
        {
            SpawnTile(x);
        }

        // var leftBoundaryTile = SpawnTile(-limit - 1);
        // var rightBoundaryTile = SpawnTile(limit + 1);
        // DarkenObject(leftBoundaryTile);
        // DarkenObject(rightBoundaryTile);

        // lets spawn boundary tile both left and right, depending on boundarySize
        for(int i = 1; i <= boundarySize; i++)
        {
            var leftBoundaryTile = SpawnTile(-limit - i);
            var rightBoundaryTile = SpawnTile(limit + i);
            DarkenObject(leftBoundaryTile);
            DarkenObject(rightBoundaryTile);
        }
    }

    private GameObject SpawnTile(int xPos)
    {
        var tile = Instantiate(tilePrefab, transform);
        tile.transform.localPosition = new Vector3(xPos, 0, 0);
        return tile;
    }

    private void DarkenObject(GameObject tile)
    {
        var renderer = tile.GetComponentsInChildren<MeshRenderer>(includeInactive: true);
        foreach(var r in renderer)
        {
            r.material.color = r.material.color * Color.grey;
        }
    }
}
