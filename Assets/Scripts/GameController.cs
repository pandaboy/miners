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

    public static Map map;

    private GameObject miner;

	// Use this for initialization
	void Start ()
    {
        map = new Map(map_data);

        miner = (GameObject)Instantiate(miner_prefab, miner_spawn, Quaternion.identity);
        miner.GetComponent<Renderer>().material.color = Color.red;
        miner.GetComponent<Character>().agent = new Miner();
        // set Objective to spawn location so that the agent is at rest
        AgentManager.AddAgent(miner.GetComponent<Character>().agent);

        /*
        GameObject wife = (GameObject)Instantiate(wife_prefab, new Vector3(2, 1.2f, 2), Quaternion.identity);
        wife.GetComponent<Renderer>().material.color = Color.yellow;
        wife.GetComponent<Character>().agent = new Wife();
        AgentManager.AddAgent(wife.GetComponent<Character>().agent);
        //wife.GetComponent<Character>().GoToTile(TileType.Home);
         * */
	}
	
	// Update is called once per frame
    void Update()
    {
        // if the miner has reached a destination, decide what action to take next
        if (!miner.GetComponent<Character>().InMotion)
        {
            //renderPath(miner.GetComponent<Character>().GetPath());
        }

        // Handle delayed FSM messages
        Message.SendDelayed();
	}

    void renderPath(ArrayList ASolution)
    {
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map.Width; j++)
            {
                bool solution = false;
                foreach (AStarNodeMap n in ASolution)
                {
                    AStarNodeMap tmp = new AStarNodeMap(null, null, 0, i, j);
                    solution = n.IsSameState(tmp);
                    if (solution)
                        break;
                }

                if (solution) {
                    map.SetTileColor(i, j, Color.red);
                }
            }
        }
    }
}
