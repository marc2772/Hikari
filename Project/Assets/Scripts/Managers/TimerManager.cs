﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerManager : Singleton<TimerManager>
{
    public Image currentHealth;
    public Text timeText;

	int time;
	float timeBetweenUpdates;
	IEnumerator coroutine;

	void Start ()
    {
		time = 100;
		timeBetweenUpdates = 1.0f;

		coroutine = UpdateTime();

		StartTimer();
	}

	IEnumerator UpdateTime()
	{
		while(true)
		{
			time--;
			if(time <= 0)
			{
				currentHealth.fillAmount = time / 100.0f;
				timeText.text = time.ToString("00") + " % ";
				PlayerStateManager.Instance.Death();
			}
			else
			{
				currentHealth.fillAmount = time / 100.0f;
				timeText.text = time.ToString("00") + " % "; 
			}

			yield return new WaitForSeconds(timeBetweenUpdates);
		}
	}

	public void StopTimer()
	{
		StopCoroutine(coroutine);
	}

	public void StartTimer()
	{
		StartCoroutine(coroutine);
	}

	public void RestartTimer()
	{
		time = 100;
		currentHealth.fillAmount = time / 100.0f;
		timeText.text = time.ToString("00") + " % ";
		StartTimer();
	}

	public void AddHealth(int health)
	{
		time += health;
		if(time > 100)
			time = 100;
		
		if(time <= 0)
		{
			timeText.text = "Game over";
			PlayerStateManager.Instance.Death();
		}
		else
		{
			currentHealth.fillAmount = time / 100.0f;
			timeText.text = time.ToString("00") + " % "; 
		}
	}

	public void ChangeTimeBetweenUpdates(float newTime)
	{
		timeBetweenUpdates = newTime;
	}
}