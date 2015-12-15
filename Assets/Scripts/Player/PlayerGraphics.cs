using UnityEngine;
using System.Collections;

public class PlayerGraphics : MonoBehaviour {

	SpriteRenderer spriteRenderer;
	Animator animator;
	FlasherToWhite flasherToWhite;


	float flashDuration = 0.1f;

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

	public void ShortPunch() {
		if(Random.Range(0,2) == 0) {
			animator.SetTrigger("action");
		}else {
			animator.SetTrigger("action_2");
		}
	}

	public void LongPunch() {
		animator.SetTrigger("long_kick");
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

	public void Die () {
		animator.Play("Death");
	}

	public void TakeDamage () {
		animator.SetTrigger("take_damage");
	}
}
