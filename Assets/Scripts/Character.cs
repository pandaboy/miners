using UnityEngine;
using System.Collections;
using FSM;
using Pathfinding;
using Mapping;

public class Character : MonoBehaviour
{
    // Agent script
    public Agent agent;

    // move speed
    public float speed = 1f;

    // private members
    private AStar a_star;
    private AStarNodeMap a_start_node;
    private AStarNodeMap a_goal_node;

    private Map map;
    private Tile current_tile;

    private int path_step = 0;
    public int PathStep
    {
        get { return path_step; }
        set { path_step = value; }
    }
    public Color path_color = Color.red;

    private ArrayList path;

    private bool at_destination = false;
    public bool AtDestination
    {
        get { return at_destination; }
        set { at_destination = value; }
    }

    private bool in_motion = false;
    public bool InMotion
    {
        get { return in_motion; }
        set { in_motion = value; }
    }

    private bool show_path = false;
    public bool ShowPath
    {
        get { return show_path; }
        set { show_path = value; }
    }

    // where the agent should go to next
    private Vector3 objective;
    public Vector3 Objective
    {
        get { return objective; }
        set { objective = value; }
    }

    // Use this for initialization
    void Awake()
    {
        a_star = new AStar();
    }

    void Start()
    {
        // keeps character at rest at the beginning.
        objective = transform.position;

        // fetch the map
        map = GameController.map;

        //Debug.Log(agent.DestinationTile);
        //agent.CurrentTile = agent.DestinationTile;
        agent.Update();
        DeterminePath();
    }

    // Update is called once per frame
    void Update()
    {
        current_tile = map.WhatTile(transform.position);
        agent.CurrentTile = current_tile.type;

        // update the Finite State Machine when we reach the destination
        // to find a new destination
        if (!InMotion && AtDestination)
        {
            // check with the State Machine to determine where to go next.
            agent.Update();

            // reset path values.
            path.Clear();
            path_step = 0;

            // calculate the path to the destination
            //if (agent.CurrentTile != agent.DestinationTile)
            //{
                DeterminePath();
                at_destination = false;
            //}

            //GoToNextInPath();
        }

        // stuff to do when the character isn't moving and hasn't reached the destination
        if (!InMotion && !AtDestination)
        {
            GoToNext();
            //GoToTile();
        }
    }

    // moves the character to the objective.
    void FixedUpdate()
    {
        // move the character to the target.
        Vector3 current = transform.position;

        if (Vector3.Distance(current, objective) > .1f)
        {
            Vector3 direction = objective - current;
            direction.Normalize();

            float st = speed * Time.deltaTime;
            transform.Translate(
                (direction.x * st),
                (direction.y * st),
                (direction.z * st),
            Space.World);

            InMotion = true;
        }
        else
        {
            InMotion = false;
        }
    }

    // move straight to the destination
    public void GoToTile()
    {
        Tile tmp = new Tile();

        map.FindTile(agent.DestinationTile, out tmp);
        
        Objective = new Vector3(
            tmp.obj.transform.position.x,
            1.2f,
            tmp.obj.transform.position.z
        );
    }

    public void GoToNext()
    {
        if (PathStep < path.Count) {
            // get the next step in the path
            AStarNodeMap next = (AStarNodeMap)path[PathStep];

            // fetch the tile, update the NextTile attribute.
            Tile nextTile = map.GetTile(next.X, next.Y);
            agent.NextTile = nextTile.type;

            /*
            // Update the agent objective.
            objective = nextTile.obj.transform.position;
            nextTile.obj.transform.Translate(
                objective.x,
                1.2f,
                objective.z
            );
            Debug.Log(objective);
            Objective = objective;
            */
            Objective = new Vector3(
                nextTile.obj.transform.position.x,
                1.2f,
                nextTile.obj.transform.position.z
            );

            PathStep++;
        }

        if (PathStep >= path.Count)
        {
            at_destination = true;
        }
    }

    // returns the path determined by the AStar code.
    public void GoToNextInPath()
    {
        Tile curr = new Tile();
        Tile nex = new Tile();

        map.FindTile(agent.CurrentTile, out curr);
        // map.FindTile(agent.NextTile, out nex);

        //Debug.Log(curr.x + ":" + nex.x + ", " + curr.y + ":" + nex.y);

        if (curr.x == nex.x && curr.y == nex.y && path_step < path.Count)
        {
            //Debug.Log("Need to Switch");
            AStarNodeMap next = (AStarNodeMap)path[path_step];
            agent.NextTile = map.GetTile(next.X, next.Y).type;

            PathStep++;
        }

        //Debug.Log("Next: " + agent.NextTile);

        Objective = new Vector3(
            nex.obj.transform.position.x,
            1.2f,
            nex.obj.transform.position.z
        );
    }

    void DeterminePath()
    {
        //agent.NextTile = agent.CurrentTile;

        // set the goal to the agents DestinationTile
        Tile destination_tile, start_tile;
        map.FindTile(agent.DestinationTile, out destination_tile);
        //map.FindTile(TileType.Bank, out destination_tile);
        map.FindTile(agent.CurrentTile, out start_tile);
        //map.FindTile(TileType.Pub, out start_tile);

        // Goal to find a path to destination
        a_goal_node = new AStarNodeMap(null, null, 0, destination_tile.x, destination_tile.y);
        
        // set the starting point to the current location
        a_start_node = new AStarNodeMap(null, a_goal_node, 0, start_tile.x, start_tile.y);

        a_start_node.GoalNode = a_goal_node;

        // find a path to the goal
        a_star.FindPath(a_start_node, a_goal_node);

        // store the path in a local var
        path = a_star.Solution;

        if (ShowPath)
        {
            RenderPath(path_color);
        }
    }

    public void RenderPath(Color color)
    {
        map.ColorMap();

        ArrayList ASolution = a_star.Solution;

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
                    map.SetTileColor(i, j, color);
                }
            }
        }
    }

    public void PrintInfo()
    {
        Debug.Log("CurrentTile: [" + current_tile.x + "," + current_tile.y + "] " + agent.CurrentTile);
        Debug.Log("NextTile: " + agent.NextTile);
        Debug.Log("DestinationTile: " + agent.DestinationTile);
        Debug.Log("Miner Path Step: " + path_step + "/" + path.Count);
    }
}
