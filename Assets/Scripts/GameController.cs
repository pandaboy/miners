using UnityEngine;
using System;
using System.Collections;
using FSM;
using Mapping;
using Pathfinding;

public class GameController : MonoBehaviour
{
    // prefabs for the characters
    public GameObject miner_prefab;
    public GameObject wife_prefab;

    // assets
    public GameObject barrier_prefab;

    // used to move the camera
    public GameObject miner_camera;

    // start position
    private Vector3 miner_spawn = new Vector3(15, 1.2f, 15);

    // tracks the current tile the miner is on.
    private Tile current_tile;

    // globally accessible static instance of the map
    public static Map map;

    // create a map
    public static TileType[,] map_data = {
		{
            // [0, 0]      [0,1]           [0,2]           [0,3]           [0,4]           [0,5]           [0,6]           [0,7]          [0,8]           [0,9]
            TileType.Wall, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Home, TileType.Grass, TileType.Wall
        },
		{
            // [1, 0]      [1,1]           [1,2]           [1,3]           [1,4]           [1,5]           [1,6]           [1,7]          [1,8]           [1,9]
            TileType.Wall, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Wall
        },
		{
            // [2, 0]      [2,1]           [2,2]           [2,3]           [2,4]           [2,5]           [2,6]           [2,7]          [2,8]           [2,9]
            TileType.Wall, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Wall
        },
		{
            // [3, 0]      [3,1]           [3,2]           [3,3]           [3,4]           [3,5]           [3,6]           [3,7]          [3,8]           [3,9]
            TileType.Wall, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Pub, TileType.Grass, TileType.Wall
        },
		{
            // [4, 0]      [4,1]           [4,2]           [4,3]           [4,4]           [4,5]           [4,6]           [4,7]          [4,8]           [4,9]
            TileType.Wall, TileType.Grass, TileType.Grass, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall
        },
		{
            // [5, 0]      [5,1]           [5,2]           [5,3]           [5,4]           [5,5]           [5,6]           [5,7]          [5,8]           [5,9]
            TileType.Wall, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Wall
        },
		{
            // [6, 0]      [6,1]           [6,2]           [6,3]           [6,4]           [6,5]           [6,6]           [6,7]          [6,8]           [6,9]
            TileType.Wall, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Wall
        },
		{
            // [7, 0]      [7,1]           [7,2]           [7,3]           [7,4]           [7,5]           [7,6]           [7,7]          [7,8]           [7,9]
            TileType.Wall, TileType.Mine, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Wall
        },
		{
            // [8, 0]      [8,1]           [8,2]           [8,3]           [8,4]           [8,5]           [8,6]           [8,7]          [8,8]           [8,9]
            TileType.Wall, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Bank, TileType.Grass, TileType.Wall
        },
		{
            // [9, 0]      [9,1]           [9,2]           [9,3]           [9,4]           [9,5]           [9,6]           [9,7]          [9,8]           [9,9]
            TileType.Wall, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Wall
        }
	};

    private GameObject miner;

	// Use this for initialization
	void Start ()
    {
        map = new Map(map_data, barrier_prefab);

        miner = (GameObject)Instantiate(miner_prefab, miner_spawn, Quaternion.identity);
        miner.GetComponent<Character>().agent = new Miner();
        miner.GetComponent<Character>().agent.DestinationTile = TileType.Home;
        miner.GetComponent<Character>().ShowPath = true;
        AgentManager.AddAgent(miner.GetComponent<Character>().agent);

        GameObject wife = (GameObject)Instantiate(wife_prefab, new Vector3(2, 1.2f, 2), Quaternion.identity);
        wife.GetComponent<Character>().agent = new Wife();
        wife.GetComponent<Character>().agent.DestinationTile = TileType.Home;
        wife.GetComponent<Renderer>().material.color = Color.yellow;
        wife.GetComponent<Character>().path_color = Color.yellow;
        AgentManager.AddAgent(wife.GetComponent<Character>().agent);
	}
	
	// Update is called once per frame
    void Update()
    {
        miner.GetComponent<Character>().PrintInfo();

        miner_camera.transform.LookAt(miner.transform);

        // Handle delayed FSM messages
        Message.SendDelayed();
	}
}
