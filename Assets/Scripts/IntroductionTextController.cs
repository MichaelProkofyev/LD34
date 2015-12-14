using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroductionTextController : MonoBehaviour {

	Text textElement;

	public AudioClip[] chars;


	string msg1 = "The regimes wall has oppressed our people for far too long!";
	string msg2 = "But who can unite us with their outrageous moves on the scene!?";//"But who is going to unite us with outrageously good dancing on this scene?"; 
	string msg3 = "Hey Stu, come here"; 
	string response1 = "Army interns, stop him before he DANCES this wall to the ground!";

	public Sprite head1;
	public Sprite head2;
	public Image headSpriteRenderer;

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
		headSpriteRenderer.enabled = true;
		StartCoroutine("UpdateHead");
		yield return StartCoroutine("UpdateMessage", response1);
		yield return new WaitForSeconds(3f);
		StopCoroutine("UpdateHead");
		finishedPrinting = true;
	}

	IEnumerator UpdateHead (){
		while (true) {
			headSpriteRenderer.sprite = head1;
			yield return new WaitForSeconds(0.2f);
			headSpriteRenderer.sprite = head2;	
			yield return new WaitForSeconds(0.2f);
		}
	}

//	IEnumerator RotateHead () {
//		while (true) {
//			yield return new WaitForSeconds(0.01f);
//			headSpriteRenderer.gameObject.transform.Rotate(0,1,0, Space.Self);
//		}
//	}

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
