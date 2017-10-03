using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelSpawn : MonoBehaviour {

	const int north = 0;
	const int south = 1;
	const int east = 2;
	const int west = 3;

	const int neighbourNorth = 4;
	const int neighbourSouth = 5;
	const int neighbourEast = 6;
	const int neighbourWest = 7;

	private GameObject[] walls;
	private List<GameObject> neighbours;
	private List<GameObject> samePanels;
	private List<string> neighbourPos;

	private GameObject player;
	private GameObject stageManager;

	private bool IsActive;
	private bool IsStart;

	private float startTimer;

	private string edgeType;

	void Awake () {
		walls = new GameObject[8];
		neighbours = new List<GameObject>();
		samePanels = new List<GameObject>();
		neighbourPos = new List<string> ();

		player = GameObject.Find ("Player");
		stageManager = GameObject.Find ("StageManager");

		walls[north] = GameObject.Find ("Wall North");
		walls[south] = GameObject.Find ("Wall South");
		walls[east] = GameObject.Find ("Wall East");
		walls[west] = GameObject.Find ("Wall West");

		walls[north].SetActive (false);
		walls[south].SetActive (false);
		walls[east].SetActive (false);
		walls[west].SetActive (false);

		startTimer = 0.1f;

		IsActive = false;
		IsStart = false;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (IsStart && Time.time > startTimer) {
			despawnWalls ();
			IsActive = true;
			IsStart = false;
			ensureSafePassage ();
		}
	}

	void OnTriggerEnter(Collider c){
		if (c.tag == "LoS" && !IsActive) {
			if (edgeType == "middle") {
				spawnWalls (Random.Range (0, 14));
			} else if ((edgeType == "edge" || edgeType == "corner") && !player.GetComponent<PlayerController> ().getLooping ()) {
				spawnWalls (Random.Range (0, 14));
				spawnSame ();
			}
			//ensureSafePassage ();
		} else if (c.tag == "Player") {
			if (edgeType == "end")
				SceneManager.LoadScene ("Win Screen");
		}
	}

	void OnTriggerExit(Collider c) {
		if (c.tag == "LoS") {
			if (edgeType != "edge" && edgeType != "corner") {
				if (IsStart)
					IsStart = false;
				despawnWalls ();
			} else {
				if (!player.GetComponent<PlayerController>().getLooping()) {
					despawnWalls ();
				}
			}
		}
	}

	void spawnSame () {
		IsActive = true;
		foreach (GameObject samePanel in samePanels) {
			if (walls[north].activeInHierarchy) {
				samePanel.GetComponent<PanelSpawn>().setWallNorth (true);
			}
			if (walls[south].activeInHierarchy) {
				samePanel.GetComponent<PanelSpawn>().setWallSouth (true);
			}
			if (walls[east].activeInHierarchy) {
				samePanel.GetComponent<PanelSpawn>().setWallEast (true);
			}
			if (walls[west].activeInHierarchy) {
				samePanel.GetComponent<PanelSpawn>().setWallWest (true);
			}
			samePanel.GetComponent<PanelSpawn> ().ensureSafePassage ();
			samePanel.GetComponent<PanelSpawn> ().setActive (true);
		}
	}

	void ensureSafePassage () {
		foreach (GameObject neighbour in neighbours) {
			if (!walls [north].activeInHierarchy && neighbour.transform.position.z > transform.position.z) {
				neighbour.GetComponent<PanelSpawn> ().setWallSouth (false);
			}
			if (!walls [east].activeInHierarchy && neighbour.transform.position.x > transform.position.x) {
				neighbour.GetComponent<PanelSpawn> ().setWallWest (false);
			}
			if (!walls [south].activeInHierarchy && neighbour.transform.position.z < transform.position.z) {
				neighbour.GetComponent<PanelSpawn> ().setWallNorth (false);
			}
			if (!walls [west].activeInHierarchy && neighbour.transform.position.x < transform.position.x) {
				neighbour.GetComponent<PanelSpawn> ().setWallEast (false);
			}
		}
	}

	void spawnWalls(int roll){
		switch (roll) {
		case 0:
			walls[north].SetActive (false);
			walls[south].SetActive (false);
			walls[east].SetActive (false);
			walls[west].SetActive (false);
			break;
		case 1:
			walls[north].SetActive (false);
			walls[south].SetActive (true);
			walls[east].SetActive (true);
			walls[west].SetActive (true);
			break;
		case 2:
			walls[north].SetActive (true);
			walls[south].SetActive (false);
			walls[east].SetActive (true);
			walls[west].SetActive (true);
			break;
		case 3:
			walls[north].SetActive (true);
			walls[south].SetActive (true);
			walls[east].SetActive (false);
			walls[west].SetActive (true);
			break;
		case 4:
			walls[north].SetActive (true);
			walls[south].SetActive (true);
			walls[east].SetActive (true);
			walls[west].SetActive (false);
			break;
		case 5:
			walls[north].SetActive (false);
			walls[south].SetActive (false);
			walls[east].SetActive (true);
			walls[west].SetActive (true);
			break;
		case 6:
			walls[north].SetActive (false);
			walls[south].SetActive (true);
			walls[east].SetActive (false);
			walls[west].SetActive (true);
			break;
		case 7:
			walls[north].SetActive (false);
			walls[south].SetActive (true);
			walls[east].SetActive (true);
			walls[west].SetActive (false);
			break;
		case 8:
			walls[north].SetActive (true);
			walls[south].SetActive (false);
			walls[east].SetActive (false);
			walls[west].SetActive (true);
			break;
		case 9:
			walls[north].SetActive (true);
			walls[south].SetActive (false);
			walls[east].SetActive (true);
			walls[west].SetActive (false);
			break;
		case 10:
			walls[north].SetActive (true);
			walls[south].SetActive (true);
			walls[east].SetActive (false);
			walls[west].SetActive (false);
			break;
		case 11:
			walls[north].SetActive (false);
			walls[south].SetActive (false);
			walls[east].SetActive (false);
			walls[west].SetActive (true);
			break;
		case 12:
			walls[north].SetActive (false);
			walls[south].SetActive (false);
			walls[east].SetActive (true);
			walls[west].SetActive (false);
			break;
		case 13:
			walls[north].SetActive (false);
			walls[south].SetActive (true);
			walls[east].SetActive (false);
			walls[west].SetActive (false);
			break;
		case 14:
			walls[north].SetActive (true);
			walls[south].SetActive (false);
			walls[east].SetActive (false);
			walls[west].SetActive (false);
			break;
		default:
			walls[north].SetActive (false);
			walls[south].SetActive (false);
			walls[east].SetActive (false);
			walls[west].SetActive (false);
			Debug.Log ("Walls didn't spawn");
			break;
		}
		IsActive = true;
	}

	void despawnWalls(){
		walls[north].SetActive (false);
		walls[south].SetActive (false);
		walls[east].SetActive (false);
		walls[west].SetActive (false);
		walls [neighbourNorth].SetActive (false);
		walls [neighbourSouth].SetActive (false);
		walls [neighbourEast].SetActive (false);
		walls [neighbourWest].SetActive (false);
		if (edgeType == "edge" || edgeType == "corner") {
			foreach (GameObject panel in samePanels) {
				for (int i = 0; i < 8; i++) {
					panel.GetComponent<PanelSpawn>().setWallSpecific (i, false);
				}
				panel.GetComponent<PanelSpawn>().setActive (false);
			}
		}
		IsActive = false;
	}

	public void setSamePanel (GameObject panel) {
		samePanels.Add (panel);
	}

	public void setNeighbour (GameObject panel, string npos) {
		neighbours.Add (panel);
		neighbourPos.Add (npos);
	}

	public void setEdgeType (string et) {
		edgeType = et;
	}

	public void setStart(bool b) {
		IsStart = b;
	}

	public bool getStart() {
		return IsStart;
	}

	public string getEdgeType () {
		return edgeType;
	}

	public void setActive (bool b) {
		IsActive = b;
	}

	public bool getActive () {
		return IsActive;
	}

	public void setWallSpecific(int i, bool b) {
		walls [i].SetActive (b);
	}

	public GameObject getWallNorth () {
		return walls[north];
	}

	public void setWallNorth (bool b) {
		walls[north].SetActive(b);
	}

	public GameObject getWallSouth () {
		return walls[south];
	}

	public void setWallSouth (bool b) {
		walls[south].SetActive(b);
	}

	public GameObject getWallEast () {
		return walls[east];
	}

	public void setWallEast (bool b) {
		walls[east].SetActive(b);
	}

	public GameObject getWallWest () {
		return walls[west];
	}

	public void setWallWest (bool b) {
		walls[west].SetActive(b);
	}

	public GameObject getNeighbourWallNorth () {
		return walls[neighbourNorth];
	}

	public void setNeighbourWallNorth (GameObject wall) {
		walls[neighbourNorth] = wall;
	}

	public GameObject getNeighbourWallSouth () {
		return walls[neighbourSouth];
	}

	public void setNeighbourWallSouth (GameObject wall) {
		walls[neighbourSouth] = wall;
	}

	public GameObject getNeighbourWallEast () {
		return walls[neighbourEast];
	}

	public void setNeighbourWallEast (GameObject wall) {
		walls[neighbourEast] = wall;
	}

	public GameObject getNeighbourWallWest () {
		return walls[neighbourWest];
	}

	public void setNeighbourWallWest (GameObject wall) {
		walls[neighbourWest] = wall;
	}
}
