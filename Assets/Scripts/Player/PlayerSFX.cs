using UnityEngine;
using System.Collections;

public class PlayerSFX : MonoBehaviour {

	public AudioClip dashSFX;
	public AudioClip [] punchClips;
	public AudioClip  punchSnareClip;
	public AudioClip take_damageSFX;
	public AudioClip gameOverSFX;
	AudioSource audioSource;
	bool lastPunchDrum = false;

	// Use this for initialization
	void Awake () {
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void PlayPunchSound () {
		audioSource.Stop();		
		int randomDrumIdx = Random.Range(0, punchClips.Length);
		audioSource.PlayOneShot(punchClips[randomDrumIdx]);

//		lastPunchDrum = !lastPunchDrum;
	}

	public void PlayLongPunchSound () {
		audioSource.Stop();
		int randomDrumIdx = Random.Range(0, punchClips.Length);
		audioSource.PlayOneShot(punchClips[randomDrumIdx]);
	}

	public void PlayDashSound () {
		audioSource.Stop();		
		audioSource.PlayOneShot(dashSFX);
	}

	public void StopPlaying () {
		audioSource.Stop();		
	}

	public void PlayDamageTakenSound () {
		audioSource.PlayOneShot(take_damageSFX);
	}

	public void PlayGameOverSound () {
		audioSource.PlayOneShot(gameOverSFX);
	}
}