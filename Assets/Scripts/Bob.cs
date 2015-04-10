using UnityEngine;
using System.Collections;
using FSM;

public class Bob : MonoBehaviour {

    private Miner miner;

	// Use this for initialization
	void Start () {
        miner = new Miner();
        AgentManager.AddAgent(miner);
	}
	
	// Update is called once per frame
	void Update () {
        miner.Update();
	}
}
