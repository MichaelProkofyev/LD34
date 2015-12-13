using UnityEngine;
using System.Collections;

public class PlayerSFX : MonoBehaviour {

	public AudioClip dashSFX;
	public AudioClip punchSFX;
	public AudioClip take_damageSFX;
	AudioSource audioSource;

	// Use this for initialization
	void Awake () {
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void PlayPunchSound () {
		audioSource.Stop();		
		audioSource.PlayOneShot(punchSFX);
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
}
