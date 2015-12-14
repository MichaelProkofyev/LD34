using UnityEngine;
using System.Collections;

public class EnemiesSpawner : MonoBehaviour {


	public Transform leftSpawnPoint;
	public Transform rightSpawnPoint;
	public GameObject enemyPrefab;
	public Transform playersTransform;

	float spawnTimeOut = 1f;
	bool lastSpawnRight = false;
	private bool DEBUG_SPEED = false;

	// Use this for initialization
	void Start () {
		spawnTimeOut = Random.Range(0f, 1f);
//		StartCoroutine("SpawnEnemies");
	}

	void Update () {
		

		if (Input.GetButtonDown("Jump")) {
			DEBUG_SPEED = !DEBUG_SPEED;
		}
	}





	public IEnumerator SpawnEnemies() {
		while(true) {
			yield return new WaitForSeconds(spawnTimeOut);
			spawnTimeOut = DEBUG_SPEED ? 20f : Random.Range(.5f, .5f);

			GameObject newEnemy;
			if (shouldSpawnToRight()) {
				newEnemy = (GameObject)Instantiate(enemyPrefab, rightSpawnPoint.position, Quaternion.identity);
			}else {
				newEnemy = (GameObject)Instantiate(enemyPrefab, leftSpawnPoint.position, Quaternion.identity);
			}
			newEnemy.GetComponent<EnemyController>().movingRight = !lastSpawnRight;
			newEnemy.GetComponent<EnemyController>().StartMovingToPlayer();

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
