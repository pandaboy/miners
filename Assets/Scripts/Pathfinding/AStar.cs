using System;
using System.Collections;

namespace Pathfinding
{
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
