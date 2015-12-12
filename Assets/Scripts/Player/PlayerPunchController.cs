using UnityEngine;
using System.Collections;

public class PlayerPunchController : MonoBehaviour {

	public BoxCollider2D leftPunchCollider;
	public BoxCollider2D rightPunchCollider;

	public float punchDuration = 0.5f;
	bool punchingRight = true;
	float currPunchTimeLeft;
	float punchPower = 25;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		UpdatePunchTriggers();
	}

	public void RecievePunchFromRight(bool punchFromRight) {
		currPunchTimeLeft = 0;
		return;
		int direсtion = punchFromRight ? -1 : 1;
		transform.Translate(Vector3.right * direсtion);
	}


	void UpdatePunchTriggers () {
		if (currPunchTimeLeft > 0) {
			currPunchTimeLeft -= Time.deltaTime;
		} else {
			DisablePunchTriggers ();
		}
	}

	void DisablePunchTriggers () {
		leftPunchCollider.enabled = false;
		rightPunchCollider.enabled = false;	
	}

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
		if (gameObj.tag == "Enemy") {
			HandlePunchingEnemy(gameObj);
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		GameObject gameObj = other.gameObject;
		if (gameObj.tag == "Enemy") {
			HandlePunchingEnemy(gameObj);
		}
	}

	void HandlePunchingEnemy(GameObject enemyObj) {
		enemyObj.GetComponent<EnemyPunchingController>().RecievePunchFromRight(!punchingRight, punchPower);
		StartCoroutine("PauseWaitResume", 0.05f);
		DisablePunchTriggers();
	}

	IEnumerator PauseWaitResume (float pauseDelay) {
		Time.timeScale = .0000001f;
		yield return new WaitForSeconds(pauseDelay * Time.timeScale);
		Time.timeScale = 1.0f;
	}

}
