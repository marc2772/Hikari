using UnityEngine;
using System.Collections;

public class SoundManager : Singleton<SoundManager>
{
	public AudioClip woodCollect;
	public AudioClip levelStart;

	public AudioClip jump1;
	public AudioClip jump2;
	public AudioClip playerDeath;

	public AudioClip rockFalling;

	AudioSource source;

	void Awake()
	{
		source = GetComponent<AudioSource>();
	}
	
	public void WoodCollect()
	{
		source.PlayOneShot(woodCollect);
	}

	public void LevelStart()
	{
		source.PlayOneShot(levelStart);
	}

	public void PlayerDeath()
	{
		source.PlayOneShot(playerDeath);
	}

	public void Jump()
	{
		if(Random.Range(0,2) == 0)
			GetComponent<AudioSource>().PlayOneShot(jump1, 0.5f);
		else
			GetComponent<AudioSource>().PlayOneShot(jump2, 0.5f);
	}

	public void RockFalling()
	{
		source.PlayOneShot(rockFalling);
	}
}
