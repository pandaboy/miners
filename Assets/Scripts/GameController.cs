using UnityEngine;
using System;
using System.Collections;
using Mapping;
using Pathfinding;

public class GameController : MonoBehaviour
{
    private AStar a_star;

    // create a map
    public static TileType[,] map_data = {
		{
            TileType.Wall, TileType.Grass, TileType.Grass, TileType.Wall, TileType.Grass,
            TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Wall
        },
		{
            TileType.Wall, TileType.Grass, TileType.Grass, TileType.Wall, TileType.Grass,
            TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Wall
        },
		{
            TileType.Wall, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass,
            TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Wall
        },
		{
            TileType.Wall, TileType.Grass, TileType.Grass, TileType.Wall, TileType.Grass,
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

	// Use this for initialization
	void Start () {
        map = new Map(map_data);
        a_star = new AStar();
	}
	
	// Update is called once per frame
    void Update()
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
}
