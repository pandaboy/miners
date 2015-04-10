using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FSM
{
    public static class AgentManager
    {
        static List<Agent> agents = new List<Agent>();
        public static int AddAgent(Agent agent)
        {
            agents.Add(agent);
            return agents.IndexOf(agent);
        }

        public static Agent GetAgent(int i)
        {
            Debug.Log("GET AGENT " + i + " OF " + agents.Count + " agents");
            return agents[i];
        }

        public static void RemoveAgent(Agent agent)
        {
            agents.Remove(agent);
        }
    }
}
