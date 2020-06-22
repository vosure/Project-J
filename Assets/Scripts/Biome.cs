using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOTE(vosure): Think about biomes
//TODO(vosure): Find assets / textures for different biomes
public enum BiomeType 
{
    Forest,
    Desert,
    Snow
}

public class Biome : MonoBehaviour
{
    public BiomeType type;

    public GameObject wallPrefab;
    public Material[] wallMaterials;

    public GameObject emptyPrefab;
    public Material[] emptyMaterials;

    public GameObject floorPrefab;
    public Material[] floorMaterials;

    public GameObject[] decorationObjects; //NOTE(vosure), flowers, trees, stones, chests etc.

    //TODO(vosure): Add more parameters
}
