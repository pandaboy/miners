using UnityEngine;
using System.Collections;
using FSM;

public class TestFSMController : MonoBehaviour {

    // prefabs for the characters
    public GameObject miner_prefab;
    public GameObject wife_prefab;

	// Use this for initialization
	void Start () {
        GameObject miner = (GameObject)Instantiate(miner_prefab, new Vector3(0, 1.2f, 0), Quaternion.identity);
        miner.GetComponent<Character>().agent = new Miner();
        AgentManager.AddAgent(miner.GetComponent<Character>().agent);

        GameObject wife = (GameObject)Instantiate(wife_prefab, new Vector3(0, 1.2f, 0), Quaternion.identity) as GameObject;
        wife.GetComponent<Character>().agent = new Wife();
        AgentManager.AddAgent(wife.GetComponent<Character>().agent);
	}
	
	// Update is called once per frame
    void Update()
    {
        Message.SendDelayed();
	}
}
