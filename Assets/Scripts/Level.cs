using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    //NOTE(vosure): Level only contains information about level, not generating and not spawning objects
    //TODO(vosure): Delete stuff related with spawning and generating later
    public Transform levelHolder;

    public int width;
    public int height;

    //TODO(vosure): tune this later, they should never exist
    public float chanceWalkerToChangeDirection = 0.5f;
    public float chanceWalkerToSpawn = 0.05f;
    public float chanceWalkerToDestoy = 0.05f;
    public int maxWalkers = 10;
    public float levelFillPercent = 0.2f;

    public Biome biome;

    LevelObjectType[,] level;


    private void Start()
    {
        level = new LevelObjectType[width, height];

        GenerateLevel();
    }

    public void GenerateLevel()
    {
        string holderName = "Generated Level";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        levelHolder = new GameObject(holderName).transform;
        levelHolder.parent = transform;

        LevelGenerator levelGenerator = new LevelGenerator(width, height, chanceWalkerToChangeDirection, chanceWalkerToSpawn, chanceWalkerToDestoy, maxWalkers, levelFillPercent);
        level = levelGenerator.GenerateLevel();

        SpawnLevel();
    }

    void SpawnLevel()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                switch (level[x, y])
                {
                    case LevelObjectType.Empty:
                        SpawnEmptyObject(x, 1, y);
                        {
                            if (Random.value <= 0.075f)
                            {
                                SpawnRandomDecoration(x, 2, y);
                            }
                            break;
                        }
                    case LevelObjectType.Floor:
                        {
                            SpawnFloorObject(x, 0, y);
                            //TODO(vosure): Spawn Decorations!
                            break;
                        }

                    case LevelObjectType.Wall:
                        SpawnWallObject(x, 1, y);
                        break;
                }
            }
        }
    }

    void SpawnRandomDecoration(float x, float y, float z)
    {
        Vector2 offset = new Vector2(width, height) / 2.0f;
        Vector2 spawnPos = new Vector2(x, z) - offset;
        SetRandomMaterial(biome.wallPrefab, biome.wallMaterials);
        GameObject obj = Instantiate(biome.decorationObjects[Random.Range(0, biome.decorationObjects.Length)], new Vector3(spawnPos.x, y, spawnPos.y), Quaternion.identity);
        obj.transform.parent = levelHolder;
    }

    void SpawnWallObject(float x, float y, float z)
    {
        Vector2 offset = new Vector2(width, height) / 2.0f;
        Vector2 spawnPos = new Vector2(x, z) - offset;
        SetRandomMaterial(biome.wallPrefab, biome.wallMaterials);
        GameObject obj = Instantiate(biome.wallPrefab, new Vector3(spawnPos.x, y, spawnPos.y), Quaternion.identity);
        obj.transform.parent = levelHolder;
    }

    void SpawnEmptyObject(float x, float y, float z) //NOTE(vosure): Filling the holes
    {
        Vector2 offset = new Vector2(width, height) / 2.0f;
        Vector2 spawnPos = new Vector2(x, z) - offset;
        SetRandomMaterial(biome.emptyPrefab, biome.emptyMaterials);
        GameObject obj = Instantiate(biome.emptyPrefab, new Vector3(spawnPos.x, y, spawnPos.y), Quaternion.identity);
        obj.transform.parent = levelHolder;
    }

    void SpawnFloorObject(float x, float y, float z)
    {
        Vector2 offset = new Vector2(width, height) / 2.0f;
        Vector2 spawnPos = new Vector2(x, z) - offset;
        SetRandomMaterial(biome.floorPrefab, biome.floorMaterials);
        GameObject obj = Instantiate(biome.floorPrefab, new Vector3(spawnPos.x, y, spawnPos.y), Quaternion.Euler(90, 0, 0));
        obj.transform.parent = levelHolder;
    }

    private void SetRandomMaterial(GameObject prefab, Material[] materials)
    {
        prefab.GetComponent<Renderer>().material = materials[Random.Range(0, materials.Length)];
    }

}