﻿using UnityEngine;
using System.Collections;

public class EnemyMovementController : MonoBehaviour {

	float speed = 5f;
	public Vector3 movementVector;
	public bool moving = true;
	public int direction = 1;

	private int playerMask;


	// Use this for initialization
	void Start () {
		playerMask = LayerMask.GetMask("Player");	
	}

	public void SetDirection (float direction) {   //setting speed AND direction
		movementVector = Vector3.right * direction * speed;
	}

	void Update () {
		if (moving) {
			transform.Translate( movementVector * Time.deltaTime);	
			CheckForPlayerAhead();
		}
	}

	void CheckForPlayerAhead () {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right*direction, 1f, playerMask);
		if (hit.collider != null) {
			moving = false;
		}
	}



}
