using UnityEngine;
using System.Collections;
using FSM;

public class TestFSMController : MonoBehaviour {

    public Miner bob;
    public Wife elsa;

	// Use this for initialization
	void Start () {
        bob = new Miner();
        elsa = new Wife();

        AgentManager.AddAgent(bob);
        AgentManager.AddAgent(elsa);

        AgentManager.GetAgent(0);
        AgentManager.GetAgent(1);
	}
	
	// Update is called once per frame
    void Update()
    {
        bob.Update();
        elsa.Update();
        Message.SendDelayed();
	}
}
