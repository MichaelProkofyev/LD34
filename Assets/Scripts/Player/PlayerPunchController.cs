using UnityEngine;
using System.Collections;

public class PlayerPunchController : MonoBehaviour {

	public BoxCollider2D leftPunchCollider;
	public BoxCollider2D rightPunchCollider;

	public float punchDuration = 0.5f;
	bool punchingRight = true;
	float currPunchTimeLeft;
	float punchPower = 15;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (currPunchTimeLeft > 0) {
			currPunchTimeLeft -= Time.deltaTime;
		} else {
			leftPunchCollider.enabled = false;
			rightPunchCollider.enabled = false;	
		}
	}

//	void DisablePunchTriggers() {
//		leftPunchCollider.enabled = false;
//		rightPunchCollider.enabled = false;
//	}

	public void EnableRightPunchTrigger (bool rightTrigger) {
		currPunchTimeLeft = punchDuration;
		if (rightTrigger) {
			punchingRight = true;
			rightPunchCollider.enabled = true;
			leftPunchCollider.enabled = false;
		}else {
				punchingRight = false;
				leftPunchCollider.enabled = true;
				rightPunchCollider.enabled = false;
		}

	}

	void OnTriggerEnter2D(Collider2D other) {
		GameObject gameObj = other.gameObject;
//		Debug.Log(gameObj.tag);
		if (gameObj.tag == "Enemy") {
			Rigidbody2D enemyRb =  gameObj.GetComponent<Rigidbody2D>();
			gameObj.GetComponent<EnemyMovementController> ().moving = false;
			int direсtion = punchingRight ? 1 : -1;
			enemyRb.AddForce((direсtion * Vector3.right + Vector3.up) * punchPower, ForceMode2D.Impulse);
			enemyRb.constraints = RigidbodyConstraints2D.FreezeRotation;

		}
	}

//	void OnTriggerStay(Collider2D other) {
//		GameObject gameObj = other.gameObject;
//		Debug.Log(gameObj.tag);
//		if (gameObj.tag == "Enemy") {
//			//			gameObj.rigidbody2D
//
//		}
//	}
}
