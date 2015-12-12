using UnityEngine;
using System.Collections;

public class EnemyGraphics : MonoBehaviour {

	public bool running = true;

	SpriteRenderer spriteRenderer;
	Animator animator;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
//		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartRunning () {
		if (running) {
//			animator.SetBool("running", true);
		}
	}

	public void FlipX (bool flipX) {
		spriteRenderer.flipX = flipX;
	}
}
