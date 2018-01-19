using UnityEngine;
using System.Collections;

public class PACScript : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		if (Input.touchCount >= 1) {
			animator.SetBool ("IsRunning", true);
		} else {
			animator.SetBool ("IsRunning", false);
		}
	}

	void OnCollisionEnter(Collision col) {
		/*if (col.gameObject.CompareTag("Enemy"))
		{
			animator.SetTrigger("Die");
		}*/
	}
}
