using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameController : MonoBehaviour {

	//START SEQUENCE CHANGES


	public GameObject [] startSequenceObjects;
	public GameObject [] startSequenceExcessObjects;

	int crowdNumbers = 0;

	enum GameState
	{
		STARTSEQUENCE,
		SHOWING_START_SEQUENCE,
		GAMESTARTED,
		GAMEOVER,
	}

	GameState currentState;
	ComboController comboController;
	CrowdSpawner crowdSpawnController;

	public bool shouldShowStartSequence = true;
	public Text scoreText;
	public GameObject gameOverTextObject;
	public Text endGameScore;
	int score = 0;
	int hearts = 3;
	public GameObject [] heartsImages; 
	public IntroductionTextController introTextController;

	public int lastCrowdScoreIncrease = 0;
	int crowdIncreaseThreashhold = 100;

	// Use this for initialization
	void Start () {
		currentState = shouldShowStartSequence ? GameState.STARTSEQUENCE : GameState.GAMESTARTED;


	}

	void Awake () {
		comboController = GameObject.FindGameObjectWithTag("ComboText").GetComponent<ComboController>();
		crowdSpawnController = GetComponent<CrowdSpawner> ();
	}

	// Update is called once per frame
	void Update () {


		switch (currentState) {
		case GameState.STARTSEQUENCE:
			foreach (GameObject gameObj in startSequenceExcessObjects) {
				gameObj.SetActive(false);
			}
			foreach (GameObject gameObj in startSequenceObjects) {
				gameObj.SetActive(true);
			}
			currentState = GameState.SHOWING_START_SEQUENCE;
			break;
		case GameState.SHOWING_START_SEQUENCE:
			if (Input.GetKeyDown(KeyCode.Space)) {
				introTextController.SkipAllThis();
			}


			if (introTextController.shouldRevealScene) {
				startSequenceObjects[0].SetActive(false); //big intro wall
				startSequenceExcessObjects[3].SetActive(true); //background
				introTextController.shouldRevealScene = false;
			}else if(introTextController.shouldRevealHero) {
				startSequenceExcessObjects[4].SetActive(true); //Player
				introTextController.shouldRevealHero = false;
			}else if(introTextController.finishedPrinting) {
				startSequenceObjects[1].SetActive(false);
				foreach (GameObject gameObj in startSequenceExcessObjects) {
					gameObj.SetActive(true);
				}
				startSequenceExcessObjects[1].GetComponent<SoundSystemController>().StartCoroutine("PlayMainTheme");
				startSequenceExcessObjects[2].GetComponent<EnemiesSpawner>().StartCoroutine("SpawnEnemies");
				foreach (GameObject lightObj in  GameObject.FindGameObjectsWithTag("Light")) {
					lightObj.GetComponent<LightChanger>().StartCoroutine("ChangeLights");
				}
				startSequenceExcessObjects[5].GetComponent<ButtonFlicker>().StartCoroutine("StartFlicker");
				currentState = GameState.GAMESTARTED;
			}
			break;
		case GameState.GAMEOVER:
			Camera.main.GetComponent<CameraController>().shake = 0;
			if (Input.GetKeyDown(KeyCode.Q)) {
				Quit();
			}else if (Input.GetKeyDown(KeyCode.R)) {
				Application.LoadLevel (1);
				Time.timeScale = 1;
			}
			break;
		}
	}


	public void AddScore(int recievedScore) {
		score += recievedScore * comboController.comboPoints;
		if (crowdNumbers < 50 && lastCrowdScoreIncrease + crowdIncreaseThreashhold < score) {
			lastCrowdScoreIncrease = score;
			crowdIncreaseThreashhold += 100;
			crowdSpawnController.SpawnHumans(1); 
		}
		scoreText.text = "Score: " + score;
	}

	public void HandlePlayerDamage () {
		if (currentState == GameState.GAMESTARTED) {
			heartsImages[hearts - 1].SetActive(false);
			hearts -= 1;
			if (hearts == 0) {
				gameOverTextObject.SetActive(true);
				endGameScore.text = "Final score: " + score;
				currentState = GameState.GAMEOVER;
				Time.timeScale = 0;
			}	
		}
	}

//	int GetSummoners() {
//		return Random.Range(2,3) * comboController.comboPoints;
//	}

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
