using UnityEngine;
using System.Collections;

public class EnemiesSpawner : MonoBehaviour {


	public Transform leftSpawnPoint;
	public Transform rightSpawnPoint;
	public GameObject enemyPrefab;
	public GameObject dogPrefab;
	public Transform playersTransform;

	float spawnTimeOut = 1f;
	public float spawnMin = 1.5f;
	public float spawnMax = 2f;

	int spawnTimeOutDecreaseThreshold = 1;
	int lastTimeOutDecreaseSpawnsCount = 0;

	float enemySpawnChance = -1f; //The lower the number, the better the chance of spawn
	float dogSpawnChance = 0.1f; //The lower the number, the better the chance of spawn

	bool lastSpawnRight = false;
	private bool DEBUG_SPEED = false;

	int enemiesSpawned  = 1;

	// Use this for initialization
	void Start () {
		spawnTimeOut = 0;
	}

	void Update () {
		

		if (Input.GetButtonDown("Jump")) {
			DEBUG_SPEED = !DEBUG_SPEED;
		}
	}

	public IEnumerator SpawnEnemies() {
		while(true) {
			yield return new WaitForSeconds(spawnTimeOut);

			GameObject newEnemy;

			if (Random.Range(enemySpawnChance, dogSpawnChance) >= 0) {
				if (shouldSpawnToRight()) {
					newEnemy = (GameObject)Instantiate(dogPrefab, (Vector2)rightSpawnPoint.position - Vector2.up*0.5f, Quaternion.identity);
				}else {
					newEnemy = (GameObject)Instantiate(dogPrefab, (Vector2)leftSpawnPoint.position - Vector2.up*0.5f, Quaternion.identity);
				}	
			}else {
				if (shouldSpawnToRight()) {
					newEnemy = (GameObject)Instantiate(enemyPrefab, (Vector2)rightSpawnPoint.position, Quaternion.identity);
				}else {
					newEnemy = (GameObject)Instantiate(enemyPrefab, (Vector2)leftSpawnPoint.position, Quaternion.identity);
				}
			}


			newEnemy.GetComponent<EnemyController>().movingRight = !lastSpawnRight;
			newEnemy.GetComponent<EnemyController>().StartMovingToPlayer();

			spawnTimeOut = DEBUG_SPEED ? 1f : Random.Range(spawnMin, spawnMax);

			enemiesSpawned++;
			if (spawnMin > 0.2 && spawnTimeOutDecreaseThreshold + lastTimeOutDecreaseSpawnsCount < enemiesSpawned) {
				spawnTimeOutDecreaseThreshold += 1;
				lastTimeOutDecreaseSpawnsCount = enemiesSpawned;
				if (dogSpawnChance < 1f) {
					dogSpawnChance += 0.05f;	
				}

				spawnMin -= 1f / spawnTimeOutDecreaseThreshold;
				spawnMax -= 1f / spawnTimeOutDecreaseThreshold;
			}

			if (spawnMin < 0.2f) {
				spawnMin = 0.5f;
				spawnMax = 0.6f;
			}
		}
	}

	bool shouldSpawnToRight() {
		float max = 1.0f;
		float min = -1.0f;
		if (lastSpawnRight) {
			max = 0.25f;
		}else {
			min = -0.25f;
		}
		lastSpawnRight = Random.Range(min, max) > 0;
		return lastSpawnRight;
	}

}
