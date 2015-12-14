using UnityEngine;
using System.Collections;

public class CrowdMemberMover : MonoBehaviour {

	public Vector2 destination;
	float speed = 5;
	float lastDistance = Mathf.Infinity;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector2.Distance(destination, transform.position) < lastDistance) {
			lastDistance = Vector2.Distance(destination, transform.position);
			transform.Translate(destination * Time.deltaTime, Space.World);
		} else {
			GetComponent<CrowdMemberMover>().enabled = false;
		}
	}

	public void StartJumping () {
		GetComponent<Animator>().SetTrigger("startDancing");
	}
}
