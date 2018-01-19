using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

	private int level;
	private int bounds;
	private bool isLooping;
	private GameObject[,] stage;
	public GameObject Panel;
	public GameObject Key;
	public GameObject Exit;
	public GameObject[] characters;

	// Use this for initialization
	void Start () {
		isLooping = false;
		level = 1;
		bounds = 3 + level * 2 + 4;
		stage = new GameObject[bounds, bounds];
		makeLevel ();
		if (isLooping) {
			assignSamePanels ();
		} else {
			assignEdges ();
		}
		assignNeighbours ();
		assignStartEnd ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int getLevel() {
		return level;
	}

	public int getBounds() {
		return bounds;
	}

	void makeLevel () {
		int counter = 0;

		for (int i = 0; i < bounds; i++) {
			for (int j = 0; j < bounds; j++) {
				GameObject panelInstance = (GameObject)Instantiate (Panel, transform);
				panelInstance.name = "Panel" + (counter);
				stage [i, j] = panelInstance;
				stage [i, j].transform.position = new Vector3 ((-bounds / 2 + j) * 10, 0f, (-bounds / 2 + i) * 10);
				if (i < 2 || i > bounds - 3 || j < 2 || j > bounds - 3) {
					if (i < 2 && j < 2 || i < 2 && j > (bounds - 1) - 2 || i > (bounds - 1) - 2 && j < 2 || i > (bounds - 1) - 2 && j > (bounds - 1) - 2) {
						stage [i, j].GetComponent<PanelSpawn> ().setEdgeType ("corner");
					} else {
						stage [i, j].GetComponent<PanelSpawn> ().setEdgeType ("edge");
					}
				} else {
					stage [i, j].GetComponent<PanelSpawn> ().setEdgeType ("middle");
				}
				counter++;
			}
		}
	}

	void assignSamePanels() {
		//set same panels and neighbours
		for (int i = 0; i < bounds; i++) {
			for (int j = 0; j < bounds; j++) {
				if (stage [i, j].GetComponent<PanelSpawn> ().getEdgeType () == "edge") {
					
					//EDGES
					if (i == 0) {
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [(bounds - 2), j]);
					} else if (i == 1) {
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [(bounds - 1), j]);
					} else if (i == (bounds - 1)) {
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [1, j]);
					} else if (i == (bounds - 1) - 1) {
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [0, j]);
					} else if (j == 0) {
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [i, (bounds - 2)]);
					} else if (j == 1) {
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [i, (bounds - 1)]);
					} else if (j == (bounds - 1)) {
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [i, 1]);
					} else if (j == (bounds - 1) - 1) {
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [i, 0]);
					}
					//-------------------------------------------------------------------------------------------------------------------------------------------------------
					//CORNERS
					//top left
				} else if (stage [i, j].GetComponent<PanelSpawn> ().getEdgeType () == "corner"){
					if (i == 0 && j == 0) {
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [i, (bounds - 2)]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [(bounds - 2), j]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [(bounds - 2), (bounds - 2)]);
					} else if (i == 0 && j == 1) {
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [i, (bounds - 1)]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [(bounds - 2), j]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [(bounds - 2), (bounds - 1)]);
					} else if (i == 1 && j == 0) {
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [i, (bounds - 2)]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [(bounds - 1), j]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [(bounds - 1), (bounds - 2)]);
					} else if (i == 1 && j == 1) {
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [i, (bounds - 1)]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [(bounds - 1), j]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [(bounds - 1), (bounds - 1)]);

					//top right
					} else if (i == 0 && j == (bounds - 2)) {
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [0, 0]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [(bounds - 2), 0]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [(bounds - 2), j]);
					} else if (i == 0 && j == (bounds - 1)) {
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [0, 1]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [(bounds - 2), 1]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [(bounds - 2), j]);
					} else if (i == 1 && j == (bounds - 2)) {
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [1, 0]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [(bounds - 1), 0]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [(bounds - 1), j]);
					} else if (i == 1 && j == (bounds - 1)) {
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [1, 1]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [(bounds - 1), 1]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [(bounds - 1), j]);

					//bottom left
					} else if (i == (bounds - 2) && j == 0) {
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [0, j]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [0, (bounds - 2)]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [i, (bounds - 2)]);
					} else if (i == (bounds - 2) && j == 1) {
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [0, j]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [0, (bounds - 1)]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [i, (bounds - 1)]);
					} else if (i == (bounds - 1) && j == 0) {
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [1, j]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [1, (bounds - 2)]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [i, (bounds - 2)]);
					} else if (i == (bounds - 1) && j == 1) {
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [1, j]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [1, (bounds - 1)]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [i, (bounds - 1)]);

					//bottom right
					} else if (i == (bounds - 2) && j == (bounds - 2)) {
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [0, 0]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [0, j]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [i, 0]);
					} else if (i == (bounds - 2) && j == (bounds - 1)) {
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [0, 1]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [0, j]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [i, 1]);
					} else if (i == (bounds - 1) && j == (bounds - 2)) {
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [1, 0]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [1, j]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [i, 0]);
					} else if (i == (bounds - 1) && j == (bounds - 1)) {
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [1, 1]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [1, j]);
						stage [i, j].GetComponent<PanelSpawn> ().setSamePanel (stage [i, 1]);
					}
				}
			}
		}
	}

	void assignNeighbours () {
		for (int i = 0; i < bounds; i++) {
			for (int j = 0; j < bounds; j++) {

				if (i - 1 == -1) {
					stage [i, j].GetComponent<PanelSpawn> ().setNeighbour (stage [(bounds - 3), j], "north");
					stage [i, j].GetComponent<PanelSpawn> ().setNeighbourWallNorth (stage [(bounds - 3), j].GetComponent<PanelSpawn> ().getWallSouth());
				} else if (i - 1 > 0) {
					stage [i, j].GetComponent<PanelSpawn> ().setNeighbour (stage [i - 1, j], "north");
					stage [i, j].GetComponent<PanelSpawn> ().setNeighbourWallNorth (stage [i - 1, j].GetComponent<PanelSpawn> ().getWallSouth());
				}

				if (j + 1 == bounds) {
					stage [i, j].GetComponent<PanelSpawn> ().setNeighbour (stage [i, 2], "east");
					stage [i, j].GetComponent<PanelSpawn> ().setNeighbourWallEast (stage [i, 2].GetComponent<PanelSpawn> ().getWallWest());
				} else if (j + 1 <= (bounds - 1)) {
					stage [i, j].GetComponent<PanelSpawn> ().setNeighbour (stage [i, j + 1], "east");
					stage [i, j].GetComponent<PanelSpawn> ().setNeighbourWallEast (stage [i, j + 1].GetComponent<PanelSpawn> ().getWallWest());
				}

				if (i + 1 == bounds) {
					stage [i, j].GetComponent<PanelSpawn> ().setNeighbour (stage [2, j], "south");
					stage [i, j].GetComponent<PanelSpawn> ().setNeighbourWallSouth (stage [2, j].GetComponent<PanelSpawn> ().getWallNorth());
				} else if (i + 1 <= (bounds - 1)) {
					stage [i, j].GetComponent<PanelSpawn> ().setNeighbour (stage [i + 1, j], "south");
					stage [i, j].GetComponent<PanelSpawn> ().setNeighbourWallSouth (stage [i + 1, j].GetComponent<PanelSpawn> ().getWallNorth());
				}

				if (j + 1 == bounds) {
					stage [i, j].GetComponent<PanelSpawn> ().setNeighbour (stage [i, (bounds - 3)], "west");
					stage [i, j].GetComponent<PanelSpawn> ().setNeighbourWallWest (stage [i, (bounds - 3)].GetComponent<PanelSpawn> ().getWallEast());
				} else if (j - 1 >= 0) {
					stage [i, j].GetComponent<PanelSpawn> ().setNeighbour (stage [i, j - 1], "west");
					stage [i, j].GetComponent<PanelSpawn> ().setNeighbourWallWest (stage [i, j - 1].GetComponent<PanelSpawn> ().getWallEast());
				}

			}
		}
	}

	void assignEdges() {
		for (int i = 0; i < bounds - 1; i++) {
			for (int j = 0; j < bounds - 1; j++) {
				if (i == 0 || i == bounds || j == 0 || j == bounds) {
					stage [i, j].GetComponent<PanelSpawn> ().setEdgeType("edge");
				}
			}
		}
	}

	void assignStartEnd () {
		int si = Random.Range (2, (bounds - 3));
		int sj = Random.Range (2, (bounds - 3));
		int ei = si;
		int ej = sj;

		while (si == ei && sj == ej) {
			ei = Random.Range (2, (bounds - 3));
			ej = Random.Range (2, (bounds - 3));
		}

		stage [si, sj].GetComponent<PanelSpawn>().setStart (true);
		Vector3 startPos = new Vector3(stage[si, sj].transform.position.x, 0.5f, stage[si, sj].transform.position.z);
		transform.GetChild(0).position = startPos;
		GameObject exitInstance = (GameObject)Instantiate (Exit, stage [ei, ej].transform);
		//Exit.transform.position.y += 0.5f;
		//stage [ei, ej].GetComponent<PanelSpawn> ().setEdgeType ("end");
	}

	public bool getLooping() {
		return isLooping;
	}
}
