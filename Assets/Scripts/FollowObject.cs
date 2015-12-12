using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour {

	public Transform targetTransform;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(targetTransform.position.x, transform.position.y, transform.position.z);
	}
}
