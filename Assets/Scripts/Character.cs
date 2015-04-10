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
    }

    // Update is called once per frame
    void Update()
    {
        // update the Finite State Machine
        agent.Update();
    }
}
