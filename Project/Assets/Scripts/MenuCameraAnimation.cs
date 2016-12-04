using UnityEngine;
using System.Collections;

public class MenuCameraAnimation : MonoBehaviour
{
	Animator animator;

	float timer;

	void Start ()
	{
		timer = 0;
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		if(timer > 0)
			timer -= Time.deltaTime;
		else
			timer = 0;
	}

	public bool IsPlaying()
	{
		return timer > 0;
	}
	
	public void Play()
	{
		animator.Play("CameraPlay");
		timer = 0.9f;
	}

	public void PlayBackwards()
	{
		animator.Play("CameraPlayToMain");
		timer = 0.9f;
	}

	public void Settings()
	{
		animator.Play("CameraSettings");
		timer = 1.9f;
	}

	public void SettingsBackwards()
	{
		animator.Play("CameraSettingsToMain");
		timer = 1.9f;
	}

	public void Instructions()
	{
		animator.Play("CameraInstructions");
		timer = 0.9f;
	}

	public void InstructionsBackwards()
	{
		animator.Play("CameraInstructionsToMain");
		timer = 0.9f;
	}

	public void Exit()
	{
		animator.Play("CameraExit");
		timer = 0.9f;
	}

	public void ExitBackwards()
	{
		animator.Play("CameraExitToMain");
		timer = 0.9f;
	}
}
