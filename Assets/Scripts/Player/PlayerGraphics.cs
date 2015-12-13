using UnityEngine;
using System.Collections;

public class PlayerGraphics : MonoBehaviour {

	SpriteRenderer spriteRenderer;
	Animator animator;
	FlasherToWhite flasherToWhite;

	float flashDuration = 0.1f;
	Color flashColor = new Color(1f, 1f, 1f, 0f);

	void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
		flasherToWhite = GetComponent<FlasherToWhite>();
	}

	// Use this for initialization
	void Start () {
		
	}

	void Update () {
	}

	public void ButtonPressed() {
		animator.SetTrigger("action");
	}

	public void FlipX (bool flipX) {
		spriteRenderer.flipX = flipX;
	}

	public void FlashSprite() {
		StartCoroutine("UpdateFlashingSprite");
	}

	IEnumerator UpdateFlashingSprite () {
		flasherToWhite.whiteSprite();
		yield return new WaitForSeconds(flashDuration);
		flasherToWhite.normalSprite();
	}
}
