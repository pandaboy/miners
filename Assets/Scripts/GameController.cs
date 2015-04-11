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

    // private members
    private AStar a_star;

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
        a_star = new AStar();

        miner = (GameObject)Instantiate(miner_prefab, new Vector3(1, 1.2f, 1), Quaternion.identity);
        miner.GetComponent<Renderer>().material.color = Color.red;
        miner.GetComponent<Character>().agent = new Miner();
        AgentManager.AddAgent(miner.GetComponent<Character>().agent);
        
        // tell them where to go
        GoToTile(miner, TileType.Pub);

        GameObject wife = (GameObject)Instantiate(wife_prefab, new Vector3(2, 1.2f, 2), Quaternion.identity);
        wife.GetComponent<Renderer>().material.color = Color.yellow;
        wife.GetComponent<Character>().agent = new Wife();
        AgentManager.AddAgent(wife.GetComponent<Character>().agent);

        // tell them where to go
        GoToTile(wife, TileType.Home);
	}
	
	// Update is called once per frame
    void Update()
    {
        // keep track of what tile the miner is on
        current_tile = map.WhatTile(miner.transform.position);

        // if the miner has reached a location, decide what action to take next
        if (!miner.GetComponent<Character>().InMotion)
        {
            // set the goal
            AStarNodeMap GoalNode = new AStarNodeMap(null, null, 0, 5, 5);
            map.SetTileColor(5, 5, Color.blue);

            // set the starting point
            AStarNodeMap StartNode = new AStarNodeMap(null, GoalNode, 0, 1, 1);
            map.SetTileColor(0, 0, Color.blue);

            StartNode.GoalNode = GoalNode;

            // find a path to the goal
            a_star.FindPath(StartNode, GoalNode);

            renderPath(a_star.Solution);
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

                if (solution)
                {
                    map.SetTileColor(i, j, Color.red);
                }
            }
        }
    }

    private void GoToTile(GameObject character, TileType tileType)
    {
        Tile tmp = new Tile();
        map.FindTile(tileType, out tmp);
        character.GetComponent<Character>().Objective = new Vector3(
            tmp.obj.transform.position.x,
            1.2f,
            tmp.obj.transform.position.z
        );
    }
}
