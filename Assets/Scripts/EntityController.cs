using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual Vector3 movement (Vector3 loc, Vector3 target, float speed) {
		Vector3 newPos = loc;
		newPos = Vector3.MoveTowards (loc, target, speed * Time.deltaTime);
		return newPos;
    }

	public virtual Vector3 rotation (Vector3 loc, Vector3 target, float speed) {
		Vector3 newRot = target - loc;
		newRot = Vector3.RotateTowards (loc, newRot, speed * Time.deltaTime, 10f);
		newRot.y = 0f;
		return newRot;
	}
}
