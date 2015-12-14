using UnityEngine;
using System.Collections;

public class CrowdSpawner : MonoBehaviour {


	public GameObject [] groundLine;
	public GameObject human;

	public Transform leftSpawnPoint;
	public Transform rightSpawnPoint;

	// Use this for initialization
	void Start () {
//		SpawnHumans(50);
	}

	public void SpawnHumans (int numberOfHumans) {

//		int lineIdx = 1;
		for (int lineIdx = 0; lineIdx < groundLine.Length; lineIdx++) {
			Bounds b = groundLine[lineIdx].GetComponent<SpriteRenderer>().bounds;	
			for (int i = 0; i < numberOfHumans; i++) {
//				bool righPoint = false;
//				if (lineIdx == 2) {
//					righPoint = true;
//				}else if (lineIdx == 1) {
//					righPoint = Random.Range(0,2) == 0 ? false : true;
//				}


//				float spawnx = righPoint ? rightSpawnPoint.position.x : leftSpawnPoint.position.x;
				Vector2 spawnPoint = new Vector2(b.center.x+Random.Range(-b.extents.x, b.extents.x), Random.Range(-2.8f, -0.5f));//new Vector2(spawnx, Random.Range(-2.8f, -0.5f));
				GameObject newHuman = (GameObject)Instantiate(human, spawnPoint, Quaternion.identity);
//				newHuman.GetComponent<CrowdMemberMover>().destination = new Vector2(b.center.x+Random.Range(-b.extents.x, b.extents.x), Random.Range(-2.8f, -0.5f));
				newHuman.GetComponent<CrowdMemberMover>().Invoke("StartJumping", Random.Range(0,1f));
				newHuman.transform.parent = groundLine[lineIdx].transform;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
