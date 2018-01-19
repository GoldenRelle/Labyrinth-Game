using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedOpponentAIScript : EntityController {

	string state;
	private float speed;
	Vector3 destination;

	// Use this for initialization
	void Start () {
		state = "SearchKey";
		speed = 5f;
	}
	
	// Update is called once per frame
	void Update () {
		if (state == "SearchKey") {
			//Move towards emptiest cluster of hidden tiles unless key is visible on map
			//Move towards the key
			//Pathfinding around walls
		} else if (state == "SearchKeyHolder") {
			//Chase player with the key
		} else if (state == "SearchExit") {
			//Avoid players and move towards the level exit
		}
	}

	void OnCollider(Collider c) {
		if (c.transform.GetComponent<KeyHoldScript> ().hasKey) {
			state = "SearchExit";
		}
	}

	void moveToDestination() {
		Vector3 pos = transform.position;
		Vector3 des = pos + new Vector3(Input.touches[0].position.x - Screen.width/2,
			0.5f,
			Input.touches[0].position.y - Screen.height/2);

		transform.rotation = Quaternion.LookRotation(des - pos);
		transform.position = movement (pos, des, speed);
	}

	void setDestination() {

	}
}
