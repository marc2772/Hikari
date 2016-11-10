using UnityEngine;
using System.Collections;

public class PlayerStateManager : Singleton<PlayerStateManager>
{
	public enum PlayerState
	{
		Alive,
		Dead
	}

	public PlayerState currentState { get; set; }

	void Start()
	{
		currentState = PlayerState.Alive;
	}

	public void Death()
	{
		currentState = PlayerState.Dead;

		Destroy(gameObject);
		TimerManager.Instance.StopTimer();
		MenuManager.Instance.Death();

		CameraManager.Instance.ToggleCameraMoving(false);
	}
}
