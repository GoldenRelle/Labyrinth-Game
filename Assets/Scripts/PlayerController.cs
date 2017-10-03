using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController {

	const float lockRot = 0f;

	const int north = 0;
	const int south = 1;
	const int east = 2;
	const int west = 3;

	private bool looping;
	private float speed;
	private float loopTime;

	private float[] bounds;

	// Use this for initialization
	void Start () {
		bounds = new float[4];
		bounds [north] = (3 + transform.parent.GetComponent<StageManager>().getLevel() * 2 + 2) * 5;
		bounds [south] = -(3 + transform.parent.GetComponent<StageManager>().getLevel() * 2 + 2) * 5;
		bounds [east] = (3 + transform.parent.GetComponent<StageManager>().getLevel() * 2 + 2) * 5;
		bounds [west] = -(3 + transform.parent.GetComponent<StageManager>().getLevel() * 2 + 2) * 5;

		looping = false;
		speed = 5f;
		loopTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1) {
			Vector3 pos = transform.position;
			Vector3 des = pos + new Vector3(Input.touches[0].position.x - Screen.width/2,
				0.5f,
				Input.touches[0].position.y - Screen.height/2);
			
			transform.rotation = Quaternion.LookRotation(des - pos);
			transform.position = movement (pos, des, speed);
		}


		if (transform.position.z > bounds[north]) {
			looping = true;
			loopTime = Time.time;
			transform.position = new Vector3 (transform.position.x , transform.position.y, transform.position.z - bounds [north] + bounds [south]);
		}
		else if (transform.position.z < bounds[south]) {
			looping = true;
			loopTime = Time.time;
			transform.position = new Vector3 (transform.position.x , transform.position.y, transform.position.z - bounds [south] + bounds [north]);
		}
		else if (transform.position.x > bounds[east]) {
			looping = true;
			loopTime = Time.time;
			transform.position = new Vector3 (transform.position.x - bounds [east] + bounds [west], transform.position.y, transform.position.z);
		}
		else if (transform.position.x < bounds[west]) {
			looping = true;
			loopTime = Time.time;
			transform.position = new Vector3 (transform.position.x - bounds [west] + bounds [east], transform.position.y, transform.position.z);
		} else if (looping == true && Time.time - loopTime > 0.5f) {
			looping = false;
			loopTime = 0f;
		}
	}

	public bool getLooping() {
		return looping;
	}
}
