using UnityEngine;
using System.Collections;

public class PlayerGraphics : MonoBehaviour {

	SpriteRenderer spriteRenderer;
	Animator animator;

	void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer> ();
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
		spriteRenderer.flipX = flipX;
	}
}
