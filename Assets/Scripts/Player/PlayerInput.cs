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
		HandleKeys();
	}

	void HandleKeys () {
		float horizontalInput = Input.GetAxis("Horizontal");
		if (horizontalInput != 0) {
			bool pressedRight = horizontalInput > 0;
			graphics.FlipX(!pressedRight);
			graphics.ButtonPressed();
			punchController.EnableRightPunchTrigger(pressedRight);

		}
	}


	void OnCollisionEnter2D(Collision2D other) {
		Debug.Log("Enemy hit you!");
	}

}
