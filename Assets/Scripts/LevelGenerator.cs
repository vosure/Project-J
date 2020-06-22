using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator
{
    private int width;
    private int height;

    private float chanceTochangeDirection;
    private float chanceToSpawn;
    private float chanceToDestroy;

    private int maxWalkers;

    private float fillPercent;

    private LevelObjectType[,] level;

    private List<Walker> walkers;

    public LevelGenerator(int width, int height, float chanceTochangeDirection, float chanceToSpawn, float chanceToDestroy, int maxWalkers, float fillPercent)
    {
        this.width = width;
        this.height = height;

        this.chanceTochangeDirection = chanceTochangeDirection;
        this.chanceToSpawn = chanceToSpawn;
        this.chanceToDestroy = chanceToDestroy;

        this.maxWalkers = maxWalkers;
        this.fillPercent = fillPercent;
    }

    public LevelObjectType[,] GenerateLevel()
    {
        Setup();
        GenerateFloor();
        GenerateWalls();
        RemoveSingleWalls();

        return level;
    }

    void Setup()
    {
        level = new LevelObjectType[width, height];
        
        for (int x = 0; x < width - 1; x++)
        {
            for (int y = 0; y < height - 1; y++)
            {
        
                level[x, y] = LevelObjectType.Empty;
            }
        }

        walkers = new List<Walker>();
        
        Walker newWalker = new Walker();
        newWalker.moveDirection = GetRandomDirection();
        
        Vector2 spawnPos = new Vector2(Mathf.RoundToInt(width / 2.0f),
                                        Mathf.RoundToInt(height / 2.0f));
        newWalker.currentPosition = spawnPos;
        
        walkers.Add(newWalker);
    }

    void GenerateFloor()
    {
        int iterations = 0;
        do
        {
            
            foreach (Walker walker in walkers)
            {
                level[(int)walker.currentPosition.x, (int)walker.currentPosition.y] = LevelObjectType.Floor;
            }
            
            for (int i = 0; i < walkers.Count; i++)
            {
                if (Random.value < chanceToDestroy && walkers.Count > 1)
                {
                    walkers.RemoveAt(i);
                    break; 
                }
            }
            
            for (int i = 0; i < walkers.Count; i++)
            {
                if (Random.value < chanceTochangeDirection)
                {
                    Walker thisWalker = walkers[i];
                    thisWalker.moveDirection = GetRandomDirection();
                    walkers[i] = thisWalker;
                }
            }
            
            for (int i = 0; i < walkers.Count; i++)
            {
                
                if (Random.value < chanceToSpawn && walkers.Count < maxWalkers)
                {
                    
                    Walker newWalker = new Walker();
                    newWalker.moveDirection = GetRandomDirection();
                    newWalker.currentPosition = walkers[i].currentPosition;
                    walkers.Add(newWalker);
                }
            }
            for (int i = 0; i < walkers.Count; i++)
            {
                Walker thisWalker = walkers[i];
                thisWalker.currentPosition += thisWalker.moveDirection;
                walkers[i] = thisWalker;
            }
            for (int i = 0; i < walkers.Count; i++)
            {
                Walker thisWalker = walkers[i];
                thisWalker.currentPosition.x = Mathf.Clamp(thisWalker.currentPosition.x, 1, width - 2);
                thisWalker.currentPosition.y = Mathf.Clamp(thisWalker.currentPosition.y, 1, height - 2);
                walkers[i] = thisWalker;
            }
            if ((float)NumberOfFloors() / (float)level.Length > fillPercent)
            {
                break;
            }
            iterations++;
        } while (iterations < 100000); //NOTE(vosure): Do I actually need that?! Probably check fillPercent is enought?!
    }

    void GenerateWalls()
    {
        for (int x = 0; x < width - 1; x++)
        {
            for (int y = 0; y < height - 1; y++)
            {
                if (level[x, y] == LevelObjectType.Floor)
                {
                    if (level[x, y + 1] == LevelObjectType.Empty)
                    {
                        level[x, y + 1] = LevelObjectType.Wall;
                    }
                    if (level[x, y - 1] == LevelObjectType.Empty)
                    {
                        level[x, y - 1] = LevelObjectType.Wall;
                    }
                    if (level[x + 1, y] == LevelObjectType.Empty)
                    {
                        level[x + 1, y] = LevelObjectType.Wall;
                    }
                    if (level[x - 1, y] == LevelObjectType.Empty)
                    {
                        level[x - 1, y] = LevelObjectType.Wall;
                    }
                }
            }
        }
    }
    void RemoveSingleWalls()
    {
        for (int x = 0; x < width - 1; x++)
        {
            for (int y = 0; y < height - 1; y++)
            {
                if (level[x, y] == LevelObjectType.Wall)
                {
                    bool allFloors = true;
                    for (int checkX = -1; checkX <= 1; checkX++)
                    {
                        for (int checkY = -1; checkY <= 1; checkY++)
                        {
                            if (x + checkX < 0 || x + checkX > width - 1 ||
                                y + checkY < 0 || y + checkY > height - 1)
                            {
                                continue;
                            }
                            if ((checkX != 0 && checkY != 0) || (checkX == 0 && checkY == 0))
                            {
                                continue;
                            }
                            if (level[x + checkX, y + checkY] != LevelObjectType.Floor)
                            {
                                allFloors = false;
                            }
                        }
                    }
                    if (allFloors)
                    {
                        level[x, y] = LevelObjectType.Floor;
                    }
                }
            }
        }
    }

    Vector2 GetRandomDirection()
    {
        int random = Random.Range(0, 4);
        switch (random)
        {
            case 0:
                return Vector2.down;
            case 1:
                return Vector2.left;
            case 2:
                return Vector2.up;
            default:
                return Vector2.right;
        }
    }

    //NOTE(vosure): Too Slow, we call it each eteration while generating floor. Find out a better way to get number of floor tiles in level
    //TODO(vosure): Optimization!
    int NumberOfFloors()
    {
        int count = 0;
        foreach (LevelObjectType levelObject in level)
        {
            if (levelObject == LevelObjectType.Floor)
            {
                count++;
            }
        }
        return count;
    }

}

public struct Walker
{
    public Vector2 moveDirection;
    public Vector2 currentPosition;
}
