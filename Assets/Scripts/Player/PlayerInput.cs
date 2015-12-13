using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

	PlayerGraphics graphics;
	PlayerPunchController punchController;

	void Awake () {
		graphics = GetComponent<PlayerGraphics> ();
		punchController = GetComponent<PlayerPunchController> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Right")) {
			HandleRightKey(true);
		} else if(Input.GetButtonDown("Left")) {
			HandleRightKey(false);
		}

	}

	void HandleRightKey (bool pressedRightKey) {
		graphics.FlipX(!pressedRightKey);
		graphics.ButtonPressed();
		int hitDirection = pressedRightKey ? 1 : -1;
		punchController.CastPunchRay(hitDirection);
	}
}
