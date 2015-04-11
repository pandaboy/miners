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

    private bool in_motion = false;
    public bool InMotion
    {
        get { return in_motion; }
        set { in_motion = value; }
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
        objective = Vector3.zero;
        map = GameController.map;
        objective = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        current_tile = map.WhatTile(transform.position);
        agent.CurrentTile = current_tile.type;

        // stuff to do when the character isn't moving.
        if (!InMotion)
        {
            // update the Finite State Machine
            agent.Update();

            // Debug.Log("I'm currently at: " + agent.CurrentTile + ", my next location is: " + agent.NextTile);

            // are we moving yet? - check if we have a different NextTile to our CurrentTile
            if (agent.CurrentTile != agent.NextTile)
            {
                // set the goal to the agents next_tile
                Tile next_tile;
                map.FindTile(agent.NextTile, out next_tile);
                /*
                a_goal_node = new AStarNodeMap(null, null, 0, next_tile.x, next_tile.y);

                // set the starting point to the current location
                a_start_node = new AStarNodeMap(null, a_goal_node, 0, current_tile.x, current_tile.y);
                map.SetTileColor(current_tile.x, current_tile.y, Color.white);

                a_start_node.GoalNode = a_goal_node;

                // find a path to the goal
                a_star.FindPath(a_start_node, a_goal_node);
                */

                // tell the agent to move to the next Tile
                GoToTile();
            }
        }
    }

    // returns the path determined by the AStar code.
    public ArrayList GetPath()
    {
        return a_star.Solution;
    }

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

            in_motion = true;
        }
        else
        {
            in_motion = false;
        }
    }

    public void GoToTile()
    {
        Tile tmp = new Tile();
        
        map.FindTile(agent.NextTile, out tmp);
        
        Objective = new Vector3(
            tmp.obj.transform.position.x,
            1.2f,
            tmp.obj.transform.position.z
        );
    }
}
