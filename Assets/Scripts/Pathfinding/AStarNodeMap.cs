using UnityEngine;
using System;
using System.Collections;

namespace Pathfinding
{
    public class AStarNodeMap : AStarNode
    {
        private int x;
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        private int y;
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public AStarNodeMap(AStarNode parent, AStarNode goal, double cost, int ax, int ay)
            : base(parent, goal, cost)
        {
            x = ax;
            y = ay;
        }

        private void AddSuccessor(ArrayList a_successors, int ax, int ay)
        {
            Mapping.Map map = GameController.map;

            int currentCost = 1;
            
            if ((ax < 0) || (ax > map.Length))
            {
                currentCost = (-1);
            } else if ((ay < 0) || (ay > map.Width))
            {
                currentCost = (-1);
            }
            else
            {
                currentCost = map.GetTileCost(ax, ay);
            }

            if (currentCost == -1)
            {
                return;
            }

            AStarNodeMap new_node = new AStarNodeMap(this, GoalNode, Cost + currentCost, ax, ay);
            if (new_node.IsSameState(Parent))
            {
                return;
            }

            a_successors.Add(new_node);
        }

        public override bool IsSameState(AStarNode node)
        {
            if (node == null)
            {
                return false;
            }

            return ((((AStarNodeMap)node).X == x) && (((AStarNodeMap)node).Y == y));
        }

        public override void Calculate()
        {
            if (GoalNode != null)
            {
                double x_dist = x - ((AStarNodeMap)GoalNode).X;
                double y_dist = y - ((AStarNodeMap)GoalNode).Y;

                // "Euclidean distance" - Used when search can move at any angle.
                // GoalEstimate = Math.Sqrt((x_dist * x_dist) + (y_dist * y_dist));

                // "Manhattan Distance" - Used when search can only move vertically and horizontally.
                // GoalEstimate = Math.Abs(x_dist) + Math.Abs(y_dist); 

                // "Diagonal Distance" - Used when the search can move in 8 directions.
                GoalEstimate = Math.Max(Math.Abs(x_dist), Math.Abs(y_dist));
            }
            else
            {
                GoalEstimate = 0;
            }
        }

        public override void GetSuccessors(ArrayList a_successors)
        {
            a_successors.Clear();

            AddSuccessor(a_successors, x - 1, y);
            AddSuccessor(a_successors, x - 1, y - 1);
            AddSuccessor(a_successors, x, y - 1);
            AddSuccessor(a_successors, x + 1, y - 1);
            AddSuccessor(a_successors, x + 1, y);
            AddSuccessor(a_successors, x + 1, y + 1);
            AddSuccessor(a_successors, x, y + 1);
            AddSuccessor(a_successors, x - 1, y + 1);
        }

        public override void PrintNodeInfo()
        {
            //Debug.Log("X:" + x + ", Y:" + y + ", Cost: " + Cost + ", Estimate: " + GoalEstimate + ", Total: " + TotalCost);
        }
    }
}
