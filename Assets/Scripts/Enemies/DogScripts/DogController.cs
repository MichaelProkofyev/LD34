using UnityEngine;
using System.Collections;

public class DogController : EnemyController {


	// Use this for initialization
	override protected void Start () {
		punchAnimationDelay = 0.2f;

		punchWait = 0.05f;
		punchWaitLeft = 0.05f;
	}
	
	// Update is called once per frame
	override protected void  Update () {
	
	}
}
