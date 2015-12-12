using UnityEngine;
using System.Collections;

public class PlayerPunchController : MonoBehaviour {

	CameraController cameraController;
	float punchPower = 25;
	int enemiesMask;

	float shortPunchDistance = 2f;
	float longPunchDistance = 20f;

	float longPunchDashTime = 1f;
	float currDashTimeLeft;

	void Start () {
		cameraController = Camera.main.GetComponent<CameraController> ();
		enemiesMask = LayerMask.GetMask("Enemies");	
	}

	void Update () {
		if (currDashTimeLeft > 0) {
//			transform.position = Vector2.Lerp(transform.position, dashDestination, 1 - currDashTimeLeft/longPunchDashTime);
		}

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
				HandleLongPunchingEnemy(hit.collider.gameObject, punchFromRight, hit.distance - 1);	
			}
		}
	}

	void HandlePunchingEnemy(GameObject enemyObj, bool punchFromRight) {
		enemyObj.GetComponent<EnemyPunchingController>().RecievePunchFromRight(punchFromRight, punchPower);
		StartCoroutine("PauseWaitResume", 0.2f);
		cameraController.StartShake();
	}

	void HandleLongPunchingEnemy(GameObject enemyObj, bool punchFromRight, float distance) {
		int direction = punchFromRight ? -1 : 1;

//		currDashTimeLeft = longPunchDashTime;

		gameObject.transform.Translate(new Vector2(direction*distance, 0), Space.Self);
		enemyObj.GetComponent<EnemyPunchingController>().RecievePunchFromRight(punchFromRight, punchPower);
		cameraController.MoveToPlayer();
//		StartCoroutine("PauseWaitResume", 0.2f);
		//cameraController.StartShake();  //TURN ON FOR SCREENSHAKE
	}

	IEnumerator PauseWaitResume (float pauseDelay) {
		yield return new WaitForSeconds(0.05f);
		Time.timeScale = .0000001f;
		yield return new WaitForSeconds(pauseDelay * Time.timeScale);
		Time.timeScale = 1.0f;

	}

}
