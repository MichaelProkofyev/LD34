using UnityEngine;
using System.Collections;

public class EnemyPunchingController : MonoBehaviour {

	public bool movingRight;

	Rigidbody2D rb;
	EnemyMovementController movingController;

	void Awake() {
		rb = GetComponent<Rigidbody2D>();
		movingController = GetComponent<EnemyMovementController>();

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<PlayerPunchController>().RecievePunchFromRight(!movingRight);
		}
	}

	public void RecievePunchFromRight(bool punchFromRight, float punchPower) {
		Debug.Log("getting punched!");
		movingController.moving = false;
		GetComponent<Collider2D>().enabled = false;
		int direсtion = punchFromRight ? -1 : 1;
		rb.AddForce((direсtion * Vector3.right + Vector3.up) * punchPower, ForceMode2D.Impulse);
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		Destroy(gameObject, 2f);
	}
}
