using UnityEngine;
using System.Collections;

public class BackgroundUpdater : MonoBehaviour {



	public GameObject[] currentScenes;  //3 scenes around player
//	SpriteRenderer currentSceneScpriteRenderer;
	GameObject player;

	static float width = 0.16f * 16.8f;


	void Awake () {
		player = GameObject.FindGameObjectWithTag("Player");
//		Debug.Log(currentSceneScpriteRenderer.bounds);
	}

	void Start() {
//		currentSceneScpriteRenderer = currentScenes[1].GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		CheckForLeftBorder();
		CheckForRightBorder();
	}

	void CheckForRightBorder () {
		
		bool needToUpdateRightScene = player.transform.position.x > currentScenes[2].transform.position.x - 5f;
		if (needToUpdateRightScene) {
			GameObject oldCurrentScene = currentScenes[1];
			currentScenes[1] = currentScenes[2];
			currentScenes[2] = oldCurrentScene;
			currentScenes[2].transform.Translate(Vector2.right*width*2f);
			currentScenes[0].transform.Translate(Vector2.right*width);
		}
	}

	void CheckForLeftBorder () {
		
		bool needToUpdateLeftScene = player.transform.position.x < currentScenes[0].transform.position.x + 5f;
		if (needToUpdateLeftScene) {
			GameObject oldCurrentScene = currentScenes[1];
			currentScenes[1] = currentScenes[0];
			currentScenes[0] = oldCurrentScene;
			currentScenes[0].transform.Translate(-Vector2.right*width*2f);
			currentScenes[2].transform.Translate(-Vector2.right*width);
		}
	}
}
