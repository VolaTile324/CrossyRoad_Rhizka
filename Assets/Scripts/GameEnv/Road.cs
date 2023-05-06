using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : Terrain
{
    [SerializeField] GameObject tunnelPrefab;
    [SerializeField] List<Car> carPrefab;
    [SerializeField] float minCarSpawnInterval;
    [SerializeField] float maxCarSpawnInterval;
    float carSpawnTimer;
    Vector3 carSpawnPos;
    Quaternion carRotation;

    private void Start()
    {
        if(Random.value > 0.5f)
        {
            carSpawnPos = new Vector3(horizontalSize / 2 + 3, 0, this.transform.position.z);
            carRotation = Quaternion.Euler(0, -90, 0);
        }
        else
        {
            carSpawnPos = new Vector3(-(horizontalSize / 2 + 3), 0, this.transform.position.z);
            carRotation = Quaternion.Euler(0, 90, 0);
        }
    }

    private void Update()
    {
        if (carSpawnTimer <= 0)
        {
            carSpawnTimer = Random.Range(minCarSpawnInterval, maxCarSpawnInterval);
            // randomly choose car to spawn in list
            var car = Instantiate(
                carPrefab[Random.Range(0, carPrefab.Count)], 
                carSpawnPos,
                carRotation
                );
            car.SetDistanceLimit(horizontalSize + 6);

            return;
        }

        carSpawnTimer -= Time.deltaTime;
    }

    public override void Generate(int size)
    {
        base.Generate(size);
        var limit = Mathf.FloorToInt((float)size / 2);
        // boundary always spawn tree
        SpawnLeftTunnel(-limit - 1);
        SpawnRightTunnel(limit + 1);
    }

    private void SpawnLeftTunnel(int xPos)
    {
        var tunnel = Instantiate(tunnelPrefab, transform);
        tunnel.transform.localPosition = new Vector3(xPos, 0, 0);
    }

    private void SpawnRightTunnel(int xPos)
    {
        var tunnel = Instantiate(tunnelPrefab, transform);
        tunnel.transform.localPosition = new Vector3(xPos, 0, 0);
        tunnel.transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
}
