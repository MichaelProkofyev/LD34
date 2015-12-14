using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroductionTextController : MonoBehaviour {

	Text textElement;

	public AudioClip[] chars;


	string msg1 = "This regimes wall has oppressed our people far too long!";
	string msg2 = "Who on the scene can unite us with their outrageous moves!";//"But who is going to unite us with outrageously good dancing on this scene?"; 
	string msg3 = "Hey Stu, come here"; 
	string response1 = "Army interns, stop him before he dances this wall to the ground!";

	public bool finishedPrinting = false;
	public bool shouldRevealScene = false;
	public bool shouldRevealHero = false;

	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		StartCoroutine("PrintMessages");
	}

	void Awake () {
		textElement = GetComponent<Text>();
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SkipAllThis () {
		shouldRevealScene = true;
		shouldRevealHero = true;
		finishedPrinting = true;
	}

	IEnumerator PrintMessages () {
		yield return StartCoroutine("UpdateMessage", msg1);
		yield return new WaitForSeconds(3f);
		yield return StartCoroutine("UpdateMessage", msg2);
		shouldRevealScene = true;
		yield return new WaitForSeconds(3f);
		yield return StartCoroutine("UpdateMessage", msg3);
		shouldRevealHero = true;
		yield return new WaitForSeconds(1f);
		yield return StartCoroutine("UpdateMessage", response1);
		yield return new WaitForSeconds(3f);
		finishedPrinting = true;
	}

	IEnumerator UpdateMessage (string message) {
		int charIndex = 0;
		textElement.text = "";
		while(charIndex != message.Length) {
			textElement.text += message[charIndex++];
			audioSource.Stop();
			audioSource.PlayOneShot(chars[3]);
			yield return new WaitForSeconds(0.05f);
		}
	}
}
