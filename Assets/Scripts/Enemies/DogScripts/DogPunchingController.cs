using UnityEngine;
using System.Collections;

public class DogPunchingController : EnemyPunchingController {

	// Use this for initialization
	override protected void Start () {
		scoreForThisEnemy = 300;
	}
	


	public override void RecievePunchFromRight(bool punchFromRight, float punchPower) {
		GetComponent<EnemyController>().Die();
		int direсtion = punchFromRight ? -1 : 1;
		rb.AddForce((direсtion * Vector3.right + Vector3.up/10f) * punchPower, ForceMode2D.Impulse);
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		gameController.AddScore(scoreForThisEnemy);
		Destroy(gameObject, 2f);
	}
}
