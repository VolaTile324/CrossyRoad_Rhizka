using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayManager : MonoBehaviour
{   
    // the list to use in randomizer algorithm
    [SerializeField] List<Terrain> terrainList;
    [SerializeField] List<Coin> coinList;

    // the player
    [SerializeField] CharMovement duck;

    // the terrain
    [SerializeField] int initialCount = 5;
    [SerializeField] int horizontalSize;
    [SerializeField] int backViewDistance = -5;
    [SerializeField] int frontViewDistance = 5;

    // active terrain dictionary, to keep track of the terrain
    Dictionary<int, Terrain> activeTerrainDiction = new Dictionary<int, Terrain>(20);

    // the player's score, and how far the player has traveled
    [SerializeField] private int travelDistance; // how far the player has traveled
    [SerializeField] private int gameCoin; // how many coins the player has collected
    public UnityEvent<int, int> OnUpdateTerrainLimit; // update terrain limit
    public UnityEvent<int> OnScoreUpdate; // update score

    private void Start()
    {          
        // initial grass
        for (int zPos = backViewDistance; zPos < initialCount; zPos++)
        {
            var terrain = Instantiate(terrainList[0]);
            terrain.transform.position = new Vector3(0, 0, zPos);

            if (terrain is Grass grass)
            {
                grass.SetTreePercentage(zPos < -1 ? 1: 0);
            }

            terrain.Generate(horizontalSize);

            activeTerrainDiction[zPos] = terrain;
        }

        // randomize ahead
        for (int zPos = initialCount; zPos < frontViewDistance; zPos++)
        {
            SpawnRandomTerrain(zPos);
        }

        OnUpdateTerrainLimit.Invoke(horizontalSize, travelDistance + backViewDistance);
    }

    // tbh, i have no idea how this works, it's driving me insane AAAAAAAAAAAA
    private Terrain SpawnRandomTerrain(int zPos)
    {
        // 'for' looping to check if the terrain is the same as the previous 3 terrains
        Terrain comparatorTerrain = null;
        int randomIndex;
        for (int z = -1; z >= -3; z--)
        {
            var checkPos = zPos + z;
            if (comparatorTerrain == null)
            {
                comparatorTerrain = activeTerrainDiction[checkPos];
                continue;
            }
            else if (comparatorTerrain.GetType() != activeTerrainDiction[checkPos].GetType())
            {
                randomIndex = Random.Range(0, terrainList.Count);
                return SpawnTerrain(terrainList[randomIndex], zPos);
            }
            else
            {
                continue;
            }
        }

        // if the terrain is the same as the previous 3 terrains, remove it from the list
        var CandidateTerrain = new List<Terrain>(terrainList);
        for(int i = 0; i < CandidateTerrain.Count; i++)
        {
            if(comparatorTerrain.GetType() == CandidateTerrain[i].GetType())
            {
                CandidateTerrain.Remove(CandidateTerrain[i]);
                break;
            }
        }

        // spawn the terrain
        randomIndex = Random.Range(0, CandidateTerrain.Count);
        return SpawnTerrain(CandidateTerrain[randomIndex], zPos);
    }

    // the function that actually spawns the terrain
    public Terrain SpawnTerrain(Terrain terrain, int zPos)
    {
        var newTerrain = Instantiate(terrain);
        newTerrain.transform.position = new Vector3(0, 0, zPos);
        newTerrain.Generate(horizontalSize);
        activeTerrainDiction[zPos] = newTerrain;
        // now that the terrain is spawned, we need to spawn the coin
        SpawnCoin(horizontalSize, zPos);
        return newTerrain;
    }

    // the function that actually spawns the random type of coin from the list
    public Coin SpawnCoin(int hzSize, int zPos, float probability = 0.2f)
    {
        if (probability == 0)
        {
            return null;
        }

        // we need to make sure the coin cannot spawn on tree too!
        List<Vector3> treeCheckList = new List<Vector3>();
        for (int i = -horizontalSize/2; i <= horizontalSize/2; i++)
        {
            // how this code works: it checks if the tree position is the same as the coin position
            // if it is, then it will not spawn the coin on the tree
            var safeSpawnPos = new Vector3(i, 0, zPos);
            if (Tree.AllPositions.Contains(safeSpawnPos) == false)
            {
                treeCheckList.Add(safeSpawnPos);
            }
        }
        if (probability >= Random.value)
        {
            var coinIndex = Random.Range(0, coinList.Count);
            var treeCheckIndex = Random.Range(0, treeCheckList.Count);
            Instantiate(
                coinList[coinIndex],
                treeCheckList[treeCheckIndex],
                Quaternion.identity
                );
        }
        return null;
    }

    // update the travel distance, also update the terrain and score
    public void UpdateTravelDist(Vector3 targetPosition)
    {
        if (targetPosition.z > travelDistance)
        {
            travelDistance = Mathf.CeilToInt(targetPosition.z);
            UpdateGameTerrain();
            OnScoreUpdate.Invoke(GetScore());
        }
    }

    // add the collected coin
    public void AddCoin(int value = 1)
    {
        this.gameCoin += value;
        OnScoreUpdate.Invoke(GetScore());
    }

    // get the score
    private int GetScore()
    {
        return travelDistance + gameCoin;
    }

    // the tabibito is moving, so we need to update the terrain
    public void UpdateGameTerrain()
    {
        var DestroyPos = travelDistance - 1 + backViewDistance;
        Destroy(activeTerrainDiction[DestroyPos].gameObject);
        activeTerrainDiction.Remove(DestroyPos);

        var SpawnPos = travelDistance - 1 + frontViewDistance;
        SpawnRandomTerrain(SpawnPos);

        OnUpdateTerrainLimit.Invoke(horizontalSize, travelDistance + backViewDistance);
    }
}
