using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

	PlayerGraphics graphics;
	PlayerPunchController punchController;

	float punchTimeOut = .1f;
	public float currPunchTimeout = 0;

	void Awake () {
		graphics = GetComponent<PlayerGraphics> ();
		punchController = GetComponent<PlayerPunchController> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (currPunchTimeout > 0) {
			currPunchTimeout -= Time.deltaTime;
			return;
		}

		if (Input.GetButtonDown("Right")) {
			HandleRightKey(true);
		} else if(Input.GetButtonDown("Left")) {
			HandleRightKey(false);
		}
	}

	void HandleRightKey (bool pressedRightKey) {
		currPunchTimeout = punchTimeOut;
		graphics.FlipX(!pressedRightKey);
		int hitDirection = pressedRightKey ? 1 : -1;
		punchController.CastPunchRay(hitDirection);
	}
}
