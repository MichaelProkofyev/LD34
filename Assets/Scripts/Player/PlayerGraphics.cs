using UnityEngine;
using System.Collections;

public class PlayerGraphics : MonoBehaviour {

	SpriteRenderer spriteRenderer;
	Animator animator;
	FlasherToWhite flasherToWhite;

	float flashDuration = 0.1f;
	float currFlashDuration = 0f;
	Color flashColor = new Color(1f, 1f, 1f, 0f);

	void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
		flasherToWhite = GetComponent<FlasherToWhite>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
//		if (currFlashDuration > 0) {
////			if (currFlashDuration > 0.5f) {
////				spriteRenderer.color = Color.Lerp(Color.white, new Color(1f, 1f, 1f, 0f), 1 - (currFlashDuration-0.5f)*2);
////			} else {
////				
////			}
//			spriteRenderer.color = Color.Lerp(Color.white, flashColor, 1 - currFlashDuration);
//		}
	}

	public void ButtonPressed() {
		animator.SetTrigger("action");
	}

	public void FlipX (bool flipX) {
		spriteRenderer.flipX = flipX;
	}

	public void FlashSprite() {
//		currFlashDuration = flashDuration;
		StartCoroutine("UpdateFlashingSprite");
	}

	IEnumerator UpdateFlashingSprite () {
//		for (int i = 0; i < 3; i++) {
			flasherToWhite.whiteSprite();
//			spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
			yield return new WaitForSeconds(flashDuration);
			flasherToWhite.normalSprite();
//		}
	}
}
