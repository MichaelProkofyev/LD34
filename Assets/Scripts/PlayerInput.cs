using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

	SpriteRenderer renderer;
	PlayerGraphics graphics;

	void Awake () {
		graphics = GetComponent<PlayerGraphics> ();
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
			graphics.FlipX(horizontalInput < 0);
			graphics.ButtonPressed();

		}
	}


}
