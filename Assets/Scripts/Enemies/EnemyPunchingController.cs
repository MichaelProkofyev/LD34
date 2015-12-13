using UnityEngine;
using System.Collections;

public class EnemyPunchingController : MonoBehaviour {

	public bool movingRight;

	Rigidbody2D rb;
	EnemyController enemyController;
	GameController gameController;
	int scoreForThisEnemy = 100;
	PlayerHealth playerHealth;

	void Awake() {
		rb = GetComponent<Rigidbody2D>();
		enemyController = GetComponent<EnemyController>();
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PunchPlayer () {
		gameController.HandlePlayerDamage();
		playerHealth.RecievePunchFromRight(!movingRight);
	}


	void OnCollisionEnter2D(Collision2D other) {
		
	}

	public void RecievePunchFromRight(bool punchFromRight, float punchPower) {
		enemyController.Die();
		int direсtion = punchFromRight ? -1 : 1;
		rb.AddForce((direсtion * Vector3.right + Vector3.up/3f) * punchPower, ForceMode2D.Impulse);
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		gameController.AddScore(scoreForThisEnemy);
		Destroy(gameObject, 2f);
	}
}
