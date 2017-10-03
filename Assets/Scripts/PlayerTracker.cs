using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour {

	private GameObject character;
	private Vector3 offset = new Vector3(0, 20, -10f);

	// Use this for initialization
	void Start () {
		character = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = character.transform.position + offset;
	}
}
