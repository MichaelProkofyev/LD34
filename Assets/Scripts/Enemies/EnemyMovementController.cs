using UnityEngine;
using System.Collections;

public class EnemyMovementController : MonoBehaviour {

//	public bool movingRight;
	public float speed = 10f;
	public Vector3 movementVector;
	public bool moving = true;

	// Use this for initialization
	void Start () {
		
	}

	public void SetDirection (float direction) {   //setting speed AND direction
		movementVector = Vector3.right * direction * speed;
	}

	void Update () {
		if (moving) {
			transform.Translate( movementVector * Time.deltaTime);	
		}
	}
}
