using UnityEngine;
using System.Collections;

public class EnemiesSpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public bool spawnToRight;

	// Use this for initialization
	void Start () {
		InvokeRepeating("SpawnEnemy", 0f, 2f);
	}

	void SpawnEnemy (){
		GameObject newEnemy = (GameObject)Instantiate(enemyPrefab, transform.position, Quaternion.identity);
		int runDirection = spawnToRight ? 1 : -1;
		newEnemy.GetComponent<EnemyMovementController>().SetDirection(runDirection);				
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
