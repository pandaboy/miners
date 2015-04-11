using UnityEngine;
using System.Collections;
using FSM;
using Mapping;

public class TestFSMController : MonoBehaviour {

    // prefabs for the characters
    public GameObject miner_prefab;
    public GameObject wife_prefab;

    // basic map to test FSM based movement and state switching
    public static TileType[,] map_data = {
		{
            TileType.Grass, TileType.Pub, TileType.Grass, TileType.Grass, TileType.Grass
        },
		{
            TileType.Grass, TileType.Grass, TileType.Grass, TileType.Bank, TileType.Grass
        },
		{
            TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass
        },
		{
            TileType.Grass, TileType.Home, TileType.Grass, TileType.Grass, TileType.Grass
        },
		{
            TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Mine
        }
	};
    public static Map map;

	// Loads in the Characters - each character self updates
	void Start () {
        map = new Map(map_data);
        
        GameObject miner = (GameObject)Instantiate(miner_prefab, new Vector3(1, 1.2f, 1), Quaternion.identity);
        miner.GetComponent<Renderer>().material.color = Color.red;
        miner.GetComponent<Character>().agent = new Miner();
        AgentManager.AddAgent(miner.GetComponent<Character>().agent);

        // tell them where to go
        GoToTile(miner, TileType.Pub);
        /*
        map.FindTile(TileType.Pub, out found);
        miner.GetComponent<Character>().Objective = new Vector3(
            found.obj.transform.position.x,
            1.2f,
            found.obj.transform.position.z
        );
        */

        GameObject wife = (GameObject)Instantiate(wife_prefab, new Vector3(2, 1.2f, 2), Quaternion.identity);
        wife.GetComponent<Renderer>().material.color = Color.yellow;
        wife.GetComponent<Character>().agent = new Wife();
        AgentManager.AddAgent(wife.GetComponent<Character>().agent);

        // tell them where to go
        GoToTile(wife, TileType.Home);
        /*
        map.FindTile(TileType.Home, out found);
        wife.GetComponent<Character>().Objective = new Vector3(
            found.obj.transform.position.x,
            1.2f,
            found.obj.transform.position.z
        );
        */
	}
	
	// Update is called once per frame
    void Update()
    {
        Message.SendDelayed();
	}

    private void GoToTile(GameObject character, TileType tileType)
    {
        Tile tmp = new Tile();
        map.FindTile(tileType, out tmp);
        character.GetComponent<Character>().Objective = new Vector3(
            tmp.obj.transform.position.x,
            1.2f,
            tmp.obj.transform.position.z
        );
    }
}
