using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TimerManager : Singleton<TimerManager>
{
    public Image currentHealth;
    public Text timeText;

	int time;
	float timeBetweenUpdates;
	IEnumerator coroutine;

	void Awake()
	{
		coroutine = UpdateTime();

		time = 100;
		timeBetweenUpdates = 1.0f;

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
				GetComponent<AudioSource> ().Play();
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
		ChangeTimeBetweenUpdates(1.0f);
		if(SceneManager.GetActiveScene().name == "Level01")
		{
			if(TutorialManager.Instance.accepted)
				time = 75 + TutorialManager.Instance.countBranch * 5;
			else
				time = 100;
		}
		else
			time = 100;
		
		currentHealth.fillAmount = time / 100.0f;
		timeText.text = time.ToString("00") + " % ";

		if(SceneManager.GetActiveScene().name == "Level01")
		{
			if(!TutorialManager.Instance.accepted)
				StartTimer();
		}
		else
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

	public void SetHealth(int health)
	{
		time = health;
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
}
