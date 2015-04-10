using UnityEngine;
using System.Collections;
using Mapping;

public class GameController : MonoBehaviour {

    private Map map;

    // create a map
    public static TileType[,] map_data= {
		{
            TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass,
            TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass
        },
		{
            TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass,
            TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass
        },
		{
            TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass,
            TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass
        },
		{
            TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass,
            TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass
        },
		{
            TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass,
            TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass
        }
	};

	// Use this for initialization
	void Start () {
        map = new Map(map_data);

        map.SetTile(2, 2, TileType.Wall);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
