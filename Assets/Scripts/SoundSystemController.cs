using UnityEngine;
using System.Collections;

public class SoundSystemController : MonoBehaviour {


	public AudioClip [] punchClips;
	AudioSource audioSource;
	int lastPunchIndex = 0;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();		
	}

	// Update is called once per frame
	void Update () {

	}

	public void playPunchSound () {
//		audioSource.PlayOneShot(punchClips[Random.Range(0, punchClips.Length-1)]);
		audioSource.PlayOneShot(punchClips[lastPunchIndex]);
		lastPunchIndex++;
		if (lastPunchIndex == punchClips.Length) {
			lastPunchIndex = 0;
		}
	}
}
