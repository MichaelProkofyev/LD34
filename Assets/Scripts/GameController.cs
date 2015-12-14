using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameController : MonoBehaviour {


	enum GameState
	{
		GAMESTARTED,
		GAMEOVER,
	}

	GameState currentState;
	ComboController comboController;

	public Text scoreText;
	public GameObject gameOverTextObject;
	int score = 0;
	int hearts = 3;
	public GameObject [] heartsImages; 

	// Use this for initialization
	void Start () {
		currentState = GameState.GAMESTARTED;
	}

	void Awake () {
		comboController = GameObject.FindGameObjectWithTag("ComboText").GetComponent<ComboController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (currentState == GameState.GAMEOVER) {
			Camera.main.GetComponent<CameraController>().shake = 0;
			if (Input.GetKeyDown(KeyCode.Q)) {
				Quit();
			}else if (Input.GetKeyDown(KeyCode.R)) {
				Application.LoadLevel (1);
				Time.timeScale = 1;
			}
		}
	}


	public void AddScore(int recievedScore) {
		score += GetSummoners();
		scoreText.text = "Rebels: " + score;
	}

	public void HandlePlayerDamage () {
		if (currentState == GameState.GAMESTARTED) {
			heartsImages[hearts - 1].SetActive(false);
			hearts -= 1;
			if (hearts == 0) {
				gameOverTextObject.SetActive(true);
				currentState = GameState.GAMEOVER;
				Time.timeScale = 0;
			}	
		}
	}

	int GetSummoners() {
		return Random.Range(2,3) * comboController.comboPoints;
	}

	public void PauseTime()
	{
//		Time.timeScale = 0;
		//call the ShowPausePanel function of the ShowPanels script
//		showPanels.ShowPausePanel ();
	}



	public void Quit()
	{
		//If we are running in a standalone build of the game
		#if UNITY_STANDALONE
		//Quit the application
		Application.Quit();
		#endif

		//If we are running in the editor
		#if UNITY_EDITOR
		//Stop playing the scene
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}
}
