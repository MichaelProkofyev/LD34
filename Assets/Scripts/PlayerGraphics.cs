using UnityEngine;
using System.Collections;

public class PlayerGraphics : MonoBehaviour {

	SpriteRenderer playerRenderer;
	Animator animator;

	void Awake() {
		playerRenderer = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ButtonPressed() {
		animator.SetTrigger("action");
	}

	public void FlipX (bool flipX) {
		playerRenderer.flipX = flipX;
	}
}
