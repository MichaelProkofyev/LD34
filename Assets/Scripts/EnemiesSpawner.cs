using UnityEngine;
using System.Collections;

public class EnemiesSpawner : MonoBehaviour {


	public Transform leftSpawnPoint;
	public Transform rightSpawnPoint;
	public GameObject enemyPrefab;
	float spawnTimeOut = 1f;
	bool lastSpawnRight = false;


	// Use this for initialization
	void Start () {
		spawnTimeOut = Random.Range(0f, 1f);
		StartCoroutine("SpawnEnemies");
	}




	IEnumerator SpawnEnemies() {
		while(true) {
			yield return new WaitForSeconds(spawnTimeOut);
			spawnTimeOut =5;// Random.Range(.75f, 2f);

			GameObject newEnemy;
			int runDirection;
			if (shouldSpawnToRight()) {
				newEnemy = (GameObject)Instantiate(enemyPrefab, rightSpawnPoint.position, Quaternion.identity);
				runDirection = -1;
			}else {
				newEnemy = (GameObject)Instantiate(enemyPrefab, leftSpawnPoint.position, Quaternion.identity);
				runDirection = 1;
			}
			newEnemy.GetComponent<EnemyMovementController>().SetDirection(runDirection);				
			newEnemy.GetComponent<EnemyMovementController>().direction = lastSpawnRight ?  -1 : 1;				
			newEnemy.GetComponent<EnemyPunchingController>().movingRight = !lastSpawnRight;

		}
	}

	bool shouldSpawnToRight() {
		float max = 1.0f;
		float min = -1.0f;
		if (lastSpawnRight) {
			max = 0.5f;
		}else {
			min = -0.5f;
		}
		lastSpawnRight = Random.Range(min, max) > 0;
		return lastSpawnRight;
	}

}
