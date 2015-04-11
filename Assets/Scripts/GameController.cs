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

    // start position
    public Vector3 miner_spawn = new Vector3(5, 1.2f, 5);

    // tracks the current tile the miner is on.
    private Tile current_tile;

    // globally accessible static instance of the map
    public static Map map;

    // create a map
    public static TileType[,] map_data = {
		{
            TileType.Wall, TileType.Grass, TileType.Grass, TileType.Wall, TileType.Grass,
            TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Wall
        },
		{
            TileType.Wall, TileType.Home, TileType.Grass, TileType.Grass, TileType.Grass,
            TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Wall
        },
		{
            TileType.Wall, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass,
            TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Wall
        },
		{
            TileType.Grass, TileType.Grass, TileType.Grass, TileType.Wall, TileType.Grass,
            TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Wall
        },
		{
            TileType.Wall, TileType.Grass, TileType.Pub, TileType.Wall, TileType.Wall,
            TileType.Wall, TileType.Wall, TileType.Grass, TileType.Grass, TileType.Wall
        },
		{
            TileType.Wall, TileType.Grass, TileType.Grass, TileType.Wall, TileType.Grass,
            TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Wall
        },
		{
            TileType.Wall, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass,
            TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Bank
        },
		{
            TileType.Mine, TileType.Grass, TileType.Grass, TileType.Wall, TileType.Grass,
            TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Wall
        },
		{
            TileType.Wall, TileType.Grass, TileType.Grass, TileType.Wall, TileType.Wall,
            TileType.Wall, TileType.Wall, TileType.Grass, TileType.Grass, TileType.Wall
        },
		{
            TileType.Wall, TileType.Grass, TileType.Grass, TileType.Wall, TileType.Grass,
            TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Wall
        }
	};

	// Use this for initialization
	void Start ()
    {
        map = new Map(map_data);

        GameObject miner = (GameObject)Instantiate(miner_prefab, miner_spawn, Quaternion.identity);
        miner.GetComponent<Renderer>().material.color = Color.red;
        miner.GetComponent<Character>().agent = new Miner();
        AgentManager.AddAgent(miner.GetComponent<Character>().agent);

        /*
        GameObject wife = (GameObject)Instantiate(wife_prefab, new Vector3(2, 1.2f, 2), Quaternion.identity);
        wife.GetComponent<Renderer>().material.color = Color.yellow;
        wife.GetComponent<Character>().agent = new Wife();
        AgentManager.AddAgent(wife.GetComponent<Character>().agent);
        //wife.GetComponent<Character>().GoToTile(TileType.Home);
        */
	}
	
	// Update is called once per frame
    void Update()
    {
        // Handle delayed FSM messages
        Message.SendDelayed();
	}
}
