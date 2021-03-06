﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	float masterVolumePercent = 0.2f;
	float sfxVolumePercent = 1;
	float musicVolumePercent = 1;
	
	AudioSource[] musicSource;
	int activeMusicSourceIndex;

	public static AudioManager instance;

	Transform audioListener;
	Transform playerT;
	void Awake()
	{
		instance = this;
		musicSource = new AudioSource[2];
		for(int i = 0; i < 2; i ++)
		{
			GameObject newMusicSource = new GameObject("Music Source " + (i + 1));
			musicSource[i] = newMusicSource.AddComponent<AudioSource>();
			newMusicSource.transform.parent = transform;
		}

		audioListener = FindObjectOfType<AudioListener>().transform;
		playerT = FindObjectOfType<Player>().transform; 
	}

	void Update()
	{
		if(playerT != null)
		{
			audioListener.position = playerT.position;
		}
	}

	public void PlayMusic(AudioClip clip, float fadeDuration = 1)
	{
		activeMusicSourceIndex = 1 - activeMusicSourceIndex;
		musicSource[activeMusicSourceIndex].clip = clip;
		musicSource[activeMusicSourceIndex].Play();

		StartCoroutine(AnimateMusicCrossfade(fadeDuration));
	}
	public void PlaySound(AudioClip clip, Vector3 pos)
	{
		if(clip != null)
		{
			AudioSource.PlayClipAtPoint(clip, pos, sfxVolumePercent * masterVolumePercent);
		}
	}

	IEnumerator AnimateMusicCrossfade(float duration)
	{
		float percent = 0;

		while(percent < 1)
		{
			percent = percent + Time.deltaTime * 1 / duration;
			musicSource[activeMusicSourceIndex].volume = Mathf.Lerp(0, musicVolumePercent * masterVolumePercent, percent);
			musicSource[1 - activeMusicSourceIndex].volume = Mathf.Lerp(musicVolumePercent * masterVolumePercent, 0, percent);
			yield return null; 
		}
	}
}
