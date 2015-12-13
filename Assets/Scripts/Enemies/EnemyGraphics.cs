using UnityEngine;
using System.Collections;

public class EnemyGraphics : MonoBehaviour {


	SpriteRenderer spriteRenderer;
	Animator enemyAnimator;

	void Awake() {
		enemyAnimator = GetComponent<Animator> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartMoving () {
		enemyAnimator.SetBool("running", true);

	}

	public void StopMoving () {
		enemyAnimator.SetBool("running", false);
	}

	public void FlipX (bool flipX) {
		spriteRenderer.flipX = flipX;
	}

	public void Punch () {
		enemyAnimator.SetTrigger("kick");
	}
}
