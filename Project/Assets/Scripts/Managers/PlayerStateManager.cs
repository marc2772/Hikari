using UnityEngine;
using System.Collections;

public class PlayerStateManager : Singleton<PlayerStateManager>
{
	private Transform spawnPoint;
	private bool spawnPointActive = false; 

	public enum PlayerState
	{
		Alive,
		Dead
	}

	public PlayerState currentState { get; set; }

	void Start()
	{
		spawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
		currentState = PlayerState.Alive;
		SoundManager.Instance.LevelStart();
	}

	public void Death()
	{
		currentState = PlayerState.Dead;

		gameObject.SetActive(false);
		TimerManager.Instance.StopTimer();
		MenuManager.Instance.Death();
		SoundManager.Instance.PlayerDeath();

		CameraManager.Instance.ToggleCameraMoving(false);
	}

	public void Respawn()
	{
		Vector3 position = spawnPoint.position;

		gameObject.transform.position = position;
		GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
		currentState = PlayerState.Alive;
		gameObject.SetActive(true);

		MenuManager.Instance.CloseAllMenus();
		if(spawnPointActive)
			TimerManager.Instance.RestartTimer(50);
		else
			TimerManager.Instance.RestartTimer(100);
		SoundManager.Instance.LevelStart();

		CameraManager.Instance.ToggleCameraMoving(true);
	}

	public void ChangeSpawnPoint(Transform newSpawnPoint)
	{
		spawnPoint = newSpawnPoint;
		spawnPointActive = true;
	}
}
