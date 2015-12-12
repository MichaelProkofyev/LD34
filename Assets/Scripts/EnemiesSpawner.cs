using UnityEngine;
using System.Collections;

public class EnemiesSpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public bool spawnToRight;
	float spawnTimeOut = 1f;

	// Use this for initialization
	void Start () {
		spawnTimeOut = Random.Range(0f, 1f);
		StartCoroutine("SpawnEnemies");
	}




	IEnumerator SpawnEnemies() {
		while(true) {
			yield return new WaitForSeconds(spawnTimeOut);
			GameObject newEnemy = (GameObject)Instantiate(enemyPrefab, transform.position, Quaternion.identity);
			int runDirection = spawnToRight ? 1 : -1;
			newEnemy.GetComponent<EnemyMovementController>().SetDirection(runDirection);				
			spawnTimeOut = Random.Range(1f, 3f);
		}
	}
	

}
