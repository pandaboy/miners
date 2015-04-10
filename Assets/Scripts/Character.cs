using UnityEngine;
using System.Collections;
using FSM;

public class Character : MonoBehaviour
{
    // Agent script
    public Agent agent;

    // Use this for initialization
    void Start()
    {
        //agent = new Miner();
        //AgentManager.AddAgent(agent);
    }

    // Update is called once per frame
    void Update()
    {
        agent.Update();
    }
}
