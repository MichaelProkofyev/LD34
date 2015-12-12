using UnityEngine;
using System.Collections;

public class PlayerPunchController : MonoBehaviour {

	CameraController cameraController;
	float punchPower = 25;
	int enemiesMask;

	float shortPunchDistance = 2f;
	float longPunchDistance = 20f;

	float longPunchDashTime = .1f;
	float currDashTimeLeft;
	Vector2 dashDestination;
	Vector2 dashStart;
	GameObject dashEnemy;
	bool dashingRight;


	void Start () {
		cameraController = Camera.main.GetComponent<CameraController> ();
		enemiesMask = LayerMask.GetMask("Enemies");	
	}

	void Update () {
		if (currDashTimeLeft > 0) {
			float dashProgress = 1 - currDashTimeLeft/longPunchDashTime;
			Vector2 newPosition = Vector2.Lerp(dashStart, dashDestination, dashProgress);
			transform.position = newPosition;//, Space.World);
//			Debug.Log(dashProgress);
//			Debug.Log(transform.position);
			cameraController.MoveToPlayer();
			currDashTimeLeft -= Time.deltaTime;
			if (currDashTimeLeft < 0) {
				LongDashFinish();
			}
		}
	}

	void LongDashFinish() {
		StartCoroutine("PauseWaitResume", 0.2f);
		cameraController.StartShake();
		dashEnemy.GetComponent<EnemyPunchingController>().RecievePunchFromRight(!dashingRight, punchPower);
		PlayPunchSound();
	}

	public void RecievePunchFromRight(bool punchFromRight) {
		return;
		int direсtion = punchFromRight ? -1 : 1;
		transform.Translate(Vector3.right * direсtion);
	}

	public void CastPunchRay(int direction) {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right*direction, shortPunchDistance, enemiesMask);
		if (hit.collider != null) {
			bool punchFromRight = (direction == -1) ? true : false;
			HandlePunchingEnemy(hit.collider.gameObject, punchFromRight);
		}
		else {
			hit = Physics2D.Raycast(transform.position, Vector2.right*direction, longPunchDistance, enemiesMask);	
			if (hit.collider != null) {
				bool punchFromRight = (direction == -1) ? true : false;
				HandleLongPunchingEnemy(hit.collider.gameObject, punchFromRight, hit.distance - .5f);	
			}
		}
	}

	void HandlePunchingEnemy(GameObject enemyObj, bool punchFromRight) {
		enemyObj.GetComponent<EnemyPunchingController>().RecievePunchFromRight(punchFromRight, punchPower);
		StartCoroutine("PauseWaitResume", 0.2f);
		cameraController.StartShake();
		PlayPunchSound();
	}

	void HandleLongPunchingEnemy(GameObject enemyObj, bool punchFromRight, float distance) {
		int direction = punchFromRight ? -1 : 1;
		dashingRight = !punchFromRight;
		currDashTimeLeft = longPunchDashTime;
		dashDestination = transform.TransformPoint(direction*distance, 0, 0);
		dashStart = transform.position;
		dashEnemy = enemyObj;
	}

	IEnumerator PauseWaitResume (float pauseDelay) {
		yield return new WaitForSeconds(0.05f);
		Time.timeScale = .0000001f;
		yield return new WaitForSeconds(pauseDelay * Time.timeScale);
		Time.timeScale = 1.0f;

	}

	void PlayPunchSound () {
		GetComponent<AudioSource>().Play();
	}

}
