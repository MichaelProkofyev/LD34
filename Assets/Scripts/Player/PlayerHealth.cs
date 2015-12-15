using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	PlayerGraphics playerGraphicsController;
	CameraController cameraController;
	PlayerSFX playerSFX;

	int health = 3;
	// Use this for initialization
	void Start () {
	
	}

	void Awake() {
		playerGraphicsController = GetComponent<PlayerGraphics>();
		cameraController = Camera.main.GetComponent<CameraController> ();
		playerSFX = GetComponent<PlayerSFX>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RecievePunchFromRight(bool punchFromRight) {
		Debug.Log("Player punched");
		playerSFX.PlayDamageTakenSound();
		playerGraphicsController.FlashSprite();
//		playerGraphicsController.TakeDamage();
		health--;
		if (health ==0) {
			playerGraphicsController.Die();	
			return;
		}
		StartCoroutine("PauseWaitResume", 0.2f);
		cameraController.StartShake();
	}

	IEnumerator PauseWaitResume (float pauseDelay) {
		yield return new WaitForSeconds(0.05f);
		Time.timeScale = .0000001f;
		yield return new WaitForSeconds(pauseDelay * Time.timeScale);
		Time.timeScale = 1.0f;
	}




}
