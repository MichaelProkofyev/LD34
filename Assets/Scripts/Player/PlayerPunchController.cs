using UnityEngine;
using System.Collections;

public class PlayerPunchController : MonoBehaviour {

	CameraController cameraController;
	PlayerSFX playerSFX;
	float punchPower = 25;
	int enemiesMask;

	float shortPunchDistance = 1f;
	float longPunchDistance = 6f;

	float longPunchDashTime = .1f;
	float currDashTimeLeft;
	Vector2 dashDestination;
	Vector2 dashStart;
	GameObject dashEnemy;
	bool dashingRight;


	void Awake () {
		cameraController = Camera.main.GetComponent<CameraController> ();
		playerSFX = GetComponent<PlayerSFX>();
		enemiesMask = LayerMask.GetMask("Enemies");	
	}

	void Update () {
		if (currDashTimeLeft > 0) {
			UpdateDashing();	
		}
	}

	void UpdateDashing () {
		float dashProgress = 1 - currDashTimeLeft/longPunchDashTime;
		Vector2 newPosition = Vector2.Lerp(dashStart, dashDestination, dashProgress);
		transform.position = newPosition;
		cameraController.MoveToPlayer();
		currDashTimeLeft -= Time.deltaTime;
		if (currDashTimeLeft < 0) {
			Time.timeScale = 1f;
			if (dashEnemy != null) {
				LongDashFinish();	
			} else {
				MissFinish();
			}
		}
	}

	void MissFinish(){
		
	}

	void LongDashFinish() {
		StartCoroutine("PauseWaitResume", 0.2f);
		cameraController.StartShake();
		dashEnemy.GetComponent<EnemyPunchingController>().RecievePunchFromRight(!dashingRight, punchPower);
		playerSFX.PlayPunchSound();
	}

	public void CastPunchRay(int direction) {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right*direction, shortPunchDistance, enemiesMask);
		bool punchFromRight = (direction == -1) ? true : false;
		if (hit.collider != null) {
			HandlePunchingEnemy(hit.collider.gameObject, punchFromRight);
		}
		else { //LONG ATTACK
			hit = Physics2D.Raycast(transform.position, Vector2.right*direction, longPunchDistance, enemiesMask);	
			if (hit.collider != null) {
				HandleLongPunchingEnemy(hit.collider.gameObject, punchFromRight, hit.distance - 0.5f);	
			}else {
				HandleMissingEnemy(null, punchFromRight, longPunchDistance - 0.5f);
			}
		}
	}

	void HandlePunchingEnemy(GameObject enemyObj, bool punchFromRight) {
		Debug.Log("Short punch");
		enemyObj.GetComponent<EnemyPunchingController>().RecievePunchFromRight(punchFromRight, punchPower);
		StartCoroutine("PauseWaitResume", 0.2f);
		cameraController.StartShake();
		playerSFX.PlayPunchSound();
	}

	void HandleLongPunchingEnemy(GameObject enemyObj, bool punchFromRight, float distance) {
		Debug.Log("Long punch");
		int direction = punchFromRight ? -1 : 1;
		dashingRight = !punchFromRight;
		currDashTimeLeft = longPunchDashTime;
		dashDestination = transform.TransformPoint(direction*distance, 0, 0);
		dashStart = transform.position;
		dashEnemy = enemyObj;
		playerSFX.PlayDashSound();
		Time.timeScale = 0.8f;
	}

	void HandleMissingEnemy(GameObject enemyObj, bool punchFromRight, float distance) {
		Debug.Log("Missed enemy");
		int direction = punchFromRight ? -1 : 1;
		dashingRight = !punchFromRight;
		currDashTimeLeft = longPunchDashTime;
		dashDestination = transform.TransformPoint(direction*distance, 0, 0);
		dashStart = transform.position;
		dashEnemy = enemyObj;
		playerSFX.PlayDashSound();
		Time.timeScale = 0.8f;
	}

	IEnumerator PauseWaitResume (float pauseDelay) {
		yield return new WaitForSeconds(0.05f);
		Time.timeScale = .0000001f;
		yield return new WaitForSeconds(pauseDelay * Time.timeScale);
		Time.timeScale = 1.0f;
	}



}