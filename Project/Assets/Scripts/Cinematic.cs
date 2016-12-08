using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Cinematic : MonoBehaviour
{
	public GameObject questWindow;
	public GameObject mainCamera;
	public GameObject cinematicCamera;

	private GameObject player;
	private Animation anim;

	void Awake ()
	{
		anim = cinematicCamera.GetComponent<Animation> ();
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.CompareTag("Player"))
		{
			TimerManager.Instance.StopTimer();
			CameraManager.Instance.ToggleCameraMoving(false);
			player.transform.position = gameObject.transform.position;
			player.transform.LookAt (Vector3.zero);
			mainCamera.SetActive(false);
			cinematicCamera.SetActive (true);
			StartCoroutine ("PlayAnimation");
		}
	}

	void ShowCinematicWindow()
	{
		questWindow.SetActive(true);
		Transform panel = questWindow.transform.Find("Panel");
		panel.Find("Title").GetComponent<Text>().text = SettingsManager.Instance.GetString("WhatHaveYouDone");
		panel.Find("Description").GetComponent<Text>().text = SettingsManager.Instance.GetString("CinematicDescription");
		Transform okButton = panel.Find("Ok");
		okButton.GetComponentInChildren<Text>().text = SettingsManager.Instance.GetString("Yes");
		Button ok = okButton.GetComponent<Button>();
		ok.onClick.RemoveAllListeners();
		ok.onClick.AddListener(YesButton);

		Cursor.visible = true;
	}

	IEnumerator PlayAnimation ()
	{
		anim ["CameraCinematicLevel01"].speed = 0.06f;
		anim.Play();

		yield return new WaitForSeconds(5.0f);

		ShowCinematicWindow();
	}

	public void YesButton()
	{
		questWindow.SetActive(false);
		Cursor.visible = false;
		cinematicCamera.SetActive (false);
		mainCamera.SetActive(true);
		CameraManager.Instance.ToggleCameraMoving(true);
		player.transform.position = new Vector3 (-2, 40, 360); // on top of cave entrance
		player.GetComponent<Rigidbody> ().isKinematic = false;
	}
}
