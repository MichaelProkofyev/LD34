using UnityEngine;
using System.Collections;

public class PlayerPunchController : MonoBehaviour {

	CameraController cameraController;
	ComboController comboController;
	PlayerGraphics graphics;
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
		graphics = GetComponent<PlayerGraphics> ();
		cameraController = Camera.main.GetComponent<CameraController> ();
		comboController = GameObject.FindGameObjectWithTag("ComboText").GetComponent<ComboController>();
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
		

	public void CastPunchRay(int direction) {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right*direction, shortPunchDistance, enemiesMask);
		bool punchFromRight = (direction == -1) ? true : false;
		if (hit.collider != null ) {
			ShortPunch(hit.collider.gameObject, punchFromRight);
		}
		else { //LONG ATTACK
			hit = Physics2D.Raycast(transform.position, Vector2.right*direction, longPunchDistance, enemiesMask);	
			if (hit.collider != null) {
				LongPunch(hit.collider.gameObject, punchFromRight, hit.distance - 0.5f);	
			}else {
				HandleMissingEnemy(null, punchFromRight, longPunchDistance - 0.5f);
			}
		}
	}

	void ShortPunch(GameObject enemyObj, bool punchFromRight) {
//		Debug.Log("Short punch");
		shortPunchAnimation(true);
		enemyObj.GetComponent<EnemyPunchingController>().RecievePunchFromRight(punchFromRight, punchPower);
		StartCoroutine("PauseWaitResume", 0.2f);
		cameraController.StartShake();
		playerSFX.PlayPunchSound();
		comboController.AddComboPoint();
	}

	void LongPunch(GameObject enemyObj, bool punchFromRight, float distance) {
//		Debug.Log("Long punch");
		shortPunchAnimation(false);
		int direction = punchFromRight ? -1 : 1;
		dashingRight = !punchFromRight;
		currDashTimeLeft = longPunchDashTime;
		dashDestination = transform.TransformPoint(direction*distance, 0, 0);
		dashStart = transform.position;
		dashEnemy = enemyObj;
		playerSFX.PlayDashSound();
		Time.timeScale = 0.5f;
	}

	void HandleMissingEnemy(GameObject enemyObj, bool punchFromRight, float distance) {
//		Debug.Log("Missed enemy");
		shortPunchAnimation(false);
		int direction = punchFromRight ? -1 : 1;
		dashingRight = !punchFromRight;
		currDashTimeLeft = longPunchDashTime;
		dashDestination = transform.TransformPoint(direction*distance, 0, 0);
		dashStart = transform.position;
		dashEnemy = enemyObj;
		playerSFX.PlayDashSound();
		Time.timeScale = 0.5f;
	}

	void MissFinish(){
		comboController.ResetComboPoints();
	}

	void LongDashFinish() {
		StartCoroutine("PauseWaitResume", 0.2f);
		cameraController.StartShake();
		dashEnemy.GetComponent<EnemyPunchingController>().RecievePunchFromRight(!dashingRight, punchPower);
		playerSFX.PlayPunchSound();
		comboController.AddComboPoint();
	}

	void shortPunchAnimation(bool shortAnimation) {
		if (shortAnimation) {
			graphics.ShortPunch();	
		}else {
			graphics.LongPunch();
		}
	}

	IEnumerator PauseWaitResume (float pauseDelay) {
		yield return new WaitForSeconds(0.05f);
		Time.timeScale = .0000001f;
		yield return new WaitForSeconds(pauseDelay * Time.timeScale);
		Time.timeScale = 1.0f;
	}



}