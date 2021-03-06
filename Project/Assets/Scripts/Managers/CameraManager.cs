﻿using UnityEngine;
using System.Collections;

public class CameraManager : Singleton<CameraManager>
{
	GameObject player; //The player we want to follow

	float ySpeed; //The speed we want to rotate around the Y axis (the Mouse X axis)
	float xSpeed; //The speed we want to rotate around the X axis (the Mouse Y axis)

	public float xMinRotation; //The minimum angle allowed around the X axis
	public float xMaxRotation; //The maximum angle allowed around the X axis

	public float distanceMin; //The minimum distance from the player allowed
	public float distanceMax; //The maximum distance from the player allowed

	float angleY; //The current angle around the Y axis
	float angleX; //The current angle around the X axis
	float distance; //The current distance of the camera from the player
	float distanceBefore; //The distance from the player before colliding with something

	bool cameraMoving; //True if we can move the camera with the mouse

	Animator animator;
	float timer; //The time before the animation ends

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		cameraMoving = true;

		animator = GetComponent<Animator>();
	}
		
	void Start() 
	{
		ySpeed = SettingsManager.Instance.GetMouseSensitivity();
		xSpeed = SettingsManager.Instance.GetMouseSensitivity();
		Vector3 angles = transform.eulerAngles;
		angleY = angles.y;
		angleX = angles.x;
		distance = 6.0f;
		distanceBefore = distance;
	}

	void Update()
	{
		if(timer > 0.0f)
			timer -= Time.deltaTime;
		else
			timer = 0.0f;
	}

	//Toggle if the camera moves with the mouse or not
	public void ToggleCameraMoving(bool moving)
	{
		cameraMoving = moving;
		player.GetComponent<Rigidbody>().isKinematic = !moving;
	}

	void LateUpdate() 
	{
		if(cameraMoving && timer == 0)
		{
			//Disable the animator
			animator.enabled = false;

			angleY += Input.GetAxis("Mouse X") * ySpeed * Time.deltaTime;
			angleX -= Input.GetAxis("Mouse Y") * xSpeed * Time.deltaTime;

			angleX = ClampAngle(angleX, xMinRotation, xMaxRotation);

			Quaternion rotation = Quaternion.Euler(angleX, angleY, 0);

			distanceBefore = Mathf.Clamp(distanceBefore - Input.GetAxis("Mouse ScrollWheel") * 4, distanceMin, distanceMax);
			distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 4, distanceMin, distanceMax);

			Vector3 direction = (transform.position - player.transform.position).normalized;
			RaycastHit hit;
			if(Physics.Linecast(player.transform.position + direction * 0.3f, transform.position, out hit))
			{
				distance -= 0.08f;
			}
			else if(Physics.Linecast(player.transform.position + direction * 0.3f, transform.position + direction * 0.5f, out hit))
			{
				//Do nothing
			}
			else if(distance < distanceBefore)
			{
				distance += 0.08f;
			}
			else
			{
				distance = distanceBefore;
			}
			Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
			Vector3 position = rotation * negDistance + player.transform.position;

			transform.rotation = rotation;
			transform.position = position;
		}
		else
		{
			//Stop moving
		}
	}

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}

	public void CameraZoom()
	{
		animator.Play("CameraStartLevel01");
		timer = 1.3f;
	}

	public void StartShake()
	{
		StartCoroutine(Shake());
	}

	IEnumerator Shake()
	{
		ToggleCameraMoving(false);
		Vector3 position = transform.position;

		float magnitude = 0.3f;
		float elapsed = 0.0f;
		float duration = 0.4f;
		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;			

			float percentComplete = elapsed / duration;			
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

			// map noise to [-1, 1]
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			x *= magnitude * damper;
			y *= magnitude * damper;
			transform.position = new Vector3(x + position.x, y + position.y, position.z);

			yield return new WaitForSeconds(0.02f);
		}

		//Go back to where it was
		transform.position = position;
		ToggleCameraMoving(true);
	}

	public void CameraEndingAnimation()
	{
		animator.enabled = true;
		ToggleCameraMoving(false);

		animator.Play("CameraEndingLevel02");
	}
}