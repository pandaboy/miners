using System;
using System.Collections;

namespace Pathfinding
{
    public class AStarNode : IComparable
    {
        private AStarNode parent;
        public AStarNode Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        private double cost;
        public double Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        private double goal_estimate;
        public double GoalEstimate
        {
            get {
                Calculate();
                return (goal_estimate);
            }
            set { goal_estimate = value; }
        }

        public double TotalCost
        {
            get
            {
                return (Cost + GoalEstimate);
            }
        }

        private AStarNode goal_node;
        public AStarNode GoalNode
        {
            set {
                goal_node = value;
                Calculate();
            }

            get
            {
                return goal_node;
            }
        }

        public AStarNode(AStarNode a_parent, AStarNode a_goal, double a_cost)
        {
            parent = a_parent;
            goal_node = a_goal;
            cost = a_cost;
        }

        public bool IsGoal()
        {
            return IsSameState(goal_node);
        }

        public virtual bool IsSameState(AStarNode node)
        {
            return false;
        }

        public virtual void Calculate()
        {
            goal_estimate = 0.0f;
        }

        public virtual void GetSuccessors(ArrayList successors)
        {

        }

        public virtual void PrintNodeInfo()
        {

        }

        public override bool Equals(object O)
        {
            return IsSameState((AStarNode)O);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo(object O)
        {
            return (-TotalCost.CompareTo(((AStarNode)O).TotalCost));
        }
    }

    public sealed class AStar
    {
        private AStarNode startNode;
        private AStarNode goalNode;
        private Heap openList;
        private Heap closedList;
        private ArrayList successors;

        private ArrayList solution;
        public ArrayList Solution
        {
            get { return solution; }
        }

        public AStar()
        {
            openList = new Heap();
            closedList = new Heap();
            successors = new ArrayList();
            solution = new ArrayList();
        }

        private void PrintNodeList(object nodes)
        {
            Console.WriteLine("Nodes:");
            foreach (AStarNode n in (nodes as IEnumerable))
            {
                n.PrintNodeInfo();
            }
        }

        public void FindPath(AStarNode start, AStarNode goal)
        {
            startNode = start;
            goalNode = goal;

            openList.Add(startNode);
            while (openList.Count > 0)
            {
                AStarNode current = (AStarNode)openList.Pop();

                if (current.IsGoal())
                {
                    while (current != null)
                    {
                        solution.Insert(0, current);
                        current = current.Parent;
                    }
                    break;
                }

                current.GetSuccessors(successors);
                foreach (AStarNode successor in successors)
                {
                    AStarNode open = null;
                    if (openList.Contains(successor))
                    {
                        open = (AStarNode)openList[openList.IndexOf(successor)];
                    }

                    if (open != null && (successor.TotalCost > open.TotalCost))
                    {
                        continue;
                    }

                    AStarNode closed = null;
                    if (closedList.Contains(successor))
                    {
                        closed = (AStarNode)closedList[closedList.IndexOf(successor)];
                    }

                    if (closed != null && (successor.TotalCost > closed.TotalCost))
                    {
                        continue;
                    }

                    openList.Remove(open);
                    closedList.Remove(closed);
                    openList.Push(successor);

                }

                closedList.Add(current);
            }
        }
    }
}
