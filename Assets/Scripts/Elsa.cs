using UnityEngine;
using System.Collections;
using FSM;

public class Elsa : MonoBehaviour
{
    private Wife agent;

    // Use this for initialization
    void Start()
    {
        agent = new Wife();
        AgentManager.AddAgent(agent);
    }

    // Update is called once per frame
    void Update()
    {
        agent.Update();
    }
}
