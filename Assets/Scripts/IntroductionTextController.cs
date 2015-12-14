using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroductionTextController : MonoBehaviour {

	Text textElement;

	string msg1 = "This regime's wall oppressed our people far too long!";
	string msg2 = "But who is going to unite us with outrageously good dancing on this scene?"; 
	string response1 = "Army interns, stop him before he dances this wall to the ground!";

	public bool finishedPrinting = false;

	// Use this for initialization
	void Start () {
		StartCoroutine("PrintMessages");
	}

	void Awake () {
		textElement = GetComponent<Text>();
	}

	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator PrintMessages () {
		yield return StartCoroutine("UpdateMessage", msg1);
		yield return new WaitForSeconds(3f);
		yield return StartCoroutine("UpdateMessage", msg2);
		yield return new WaitForSeconds(3f);
		yield return StartCoroutine("UpdateMessage", response1);
		yield return new WaitForSeconds(3f);
		finishedPrinting = true;
	}

	IEnumerator UpdateMessage (string message) {
		int charIndex = 0;
		textElement.text = "";
		while(charIndex != message.Length) {
			textElement.text += message[charIndex++];
			yield return new WaitForSeconds(0.05f);
		}
	}
}
