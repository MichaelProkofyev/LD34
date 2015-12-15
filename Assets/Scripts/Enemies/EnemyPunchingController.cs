using UnityEngine;
using System.Collections;

public class EnemyPunchingController : MonoBehaviour {

	public bool movingRight;

	protected Rigidbody2D rb;
	protected GameController gameController;
	public int scoreForThisEnemy = 100;
	public float deathInverseHeight = 3f;
	protected PlayerHealth playerHealth;

	void Awake() {
		rb = GetComponent<Rigidbody2D>();
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth> ();
	}

	// Use this for initialization
	virtual protected void Start () {
		playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PunchPlayer () {
		gameController.HandlePlayerDamage();
		Debug.Log(GameObject.FindWithTag("Player"));
		playerHealth.RecievePunchFromRight(!movingRight);
	}


	void OnCollisionEnter2D(Collision2D other) {
		
	}

	public virtual void RecievePunchFromRight(bool punchFromRight, float punchPower) {
		GetComponent<EnemyController>().Die();
		int direсtion = punchFromRight ? -1 : 1;
		rb.AddForce((direсtion * Vector3.right + Vector3.up/deathInverseHeight) * punchPower, ForceMode2D.Impulse);
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		gameController.AddScore(scoreForThisEnemy);
		Destroy(gameObject, 2f);
	}
}
