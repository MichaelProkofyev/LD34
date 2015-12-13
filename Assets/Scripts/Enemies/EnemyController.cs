using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {


	enum EnemyState
	{
		MOVING_TO_PLAYER,
		WAITING_TO_PUNCH,
		PUNCHING,
		DEAD
	}

	EnemyState state = EnemyState.MOVING_TO_PLAYER;
	EnemyMovementController movement;
	EnemyGraphics enemyGraphics;
	EnemyPunchingController enemyPunch;
	PlayerHealth playerHealth;

	int playerMask;

	public float punchWait = 1f;
	public bool movingRight;
	float punchWaitLeft = 1f;



	// Use this for initialization
	void Start () {
		state = EnemyState.MOVING_TO_PLAYER;
	}

	void Awake() {
		movement = GetComponent<EnemyMovementController> ();
		enemyGraphics = GetComponent<EnemyGraphics> ();
		enemyPunch = GetComponent<EnemyPunchingController> ();
		playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth> ();
		playerMask = LayerMask.GetMask("Player");	
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case EnemyState.MOVING_TO_PLAYER:
			if (PlayerAhead()) {
				state = EnemyState.WAITING_TO_PUNCH;
				StopMovingToPlayer();
				punchWaitLeft = punchWait;
			}
			break;
		case EnemyState.WAITING_TO_PUNCH:
			if (punchWaitLeft < 0) {
				state = EnemyState.PUNCHING;
			}else {
				if (PlayerAhead()) {
					punchWaitLeft -= Time.deltaTime;
				}else {
					state = EnemyState.MOVING_TO_PLAYER;
				}
			}
			break;
		case EnemyState.PUNCHING:
			playerHealth.RecievePunchFromRight(!movingRight);
			punchWaitLeft = punchWait;
			state = EnemyState.WAITING_TO_PUNCH;
			break;
		default:
			break;
		}
	}

	public void StartMovingToPlayer () {
		movement.SetDirection(dirFromRight(movingRight));				
		movement.StartMoving();
		enemyGraphics.FlipX(!movingRight);
		enemyGraphics.StartMoving();
		enemyPunch.movingRight = movingRight;
	}

	public void StopMovingToPlayer () {
		movement.StopMoving();
		enemyGraphics.StopMoving();
	}

	bool PlayerAhead () {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right*dirFromRight(movingRight), 1f, playerMask);
		if (hit.collider != null) {
			return true;
		}
		return false;
	}

	int dirFromRight(bool movingRight) {
		return movingRight ? 1 : -1;
	}
}
