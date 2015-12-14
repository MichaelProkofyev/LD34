﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundSystemController : MonoBehaviour {



	AudioSource audioSource;

	public AudioSource mainThemeStartAudioSource;
	public AudioSource mainThemeLoopAudioSource;
	public bool playMainTheme = true;

	void Awake () {
		audioSource = GetComponent<AudioSource> ();		
	}

	// Use this for initialization
	void Start () {
		if (playMainTheme) {
			StartCoroutine("PlayMainTheme");	
		}
	}

	// Update is called once per frame
	void Update () {

	}

	IEnumerator PlayMainTheme () {
		mainThemeStartAudioSource.Play();
//		yield return new WaitForSeconds(mainThemeStartAudioSource.clip.length);

		yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(mainThemeStartAudioSource.clip.length));


		mainThemeLoopAudioSource.Play();
	}

	public void StopPlayingMainTheme () {
		mainThemeLoopAudioSource.Stop();
		mainThemeStartAudioSource.Stop();
	}


}


public static class CoroutineUtil
{
	public static IEnumerator WaitForRealSeconds(float time)
	{
		float start = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup < start + time)
		{
			yield return null;
		}
	}
}

