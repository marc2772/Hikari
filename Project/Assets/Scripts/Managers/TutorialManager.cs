using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TutorialManager : Singleton<TutorialManager>
{
	public GameObject questWindowStart;
	public GameObject questWindowEnd;
	public GameObject countWindow;

	public GameObject branch;
	const int numberOfBranches = 5;
	[Range(0, numberOfBranches)]
	public int countBranch;
	Text countText;

	public Transform areaOfSpawn;
	public GameObject[] iceFloating;

	public bool Active = false; // to know if addhealth can increase countBranch

	void Start ()
	{
		foreach (var ice in iceFloating)
			ice.SetActive (false);

		questWindowStart.SetActive(false);
		questWindowEnd.SetActive(false);
		countWindow.SetActive(false);

		TimerManager.Instance.StopTimer ();
		TimerManager.Instance.SetHealth (75);

		countBranch = 0;

		StartTutorial();
	}

	void Update()
	{
		if(countWindow.activeSelf)
			SetCountText();
	}

	void StartTutorial()
	{
		questWindowStart.SetActive (true);
		Transform panel = questWindowStart.transform.Find("Panel");
		panel.Find("Title").GetComponent<Text>().text = SettingsManager.Instance.GetString("DontLetTheFlameGoOut");
		panel.Find("Description").GetComponent<Text> ().text = SettingsManager.Instance.GetString("TutorialDescription");
		panel.Find("Ok").GetComponentInChildren<Text> ().text = SettingsManager.Instance.GetString("Ok");

		CameraManager.Instance.ToggleCameraMoving(false);
		Cursor.visible = true;
	}

	void BeginTutorial()
	{
		countWindow.SetActive(true);
		Transform panel = countWindow.transform.Find("Panel");
		panel.Find("Branches").GetComponent<Text> ().text = SettingsManager.Instance.GetString("Branches") + ":";

		countText = panel.Find("Count").GetComponent<Text>();
	}

	void EndTutorial()
	{
		questWindowEnd.SetActive(true);
		Transform panel = questWindowEnd.transform.Find("Panel");
		panel.Find("Title").GetComponent<Text> ().text = SettingsManager.Instance.GetString("IntoTheWild");
		panel.Find("Description").GetComponent<Text> ().text = SettingsManager.Instance.GetString("EndTutorialDescription");
		panel.Find("LetsGo").GetComponentInChildren<Text> ().text = SettingsManager.Instance.GetString("LetsGo");

		CameraManager.Instance.ToggleCameraMoving(false);
		Cursor.visible = true;
	}

	public void OkButton ()
	{
		Active = true;

		questWindowStart.SetActive(false);
		InstantiateObjects ();
		Cursor.visible = false;
		CameraManager.Instance.ToggleCameraMoving(true);
		CameraZoom();

		BeginTutorial();
	}

	public void LetsGoButton ()
	{
		Active = false;

		countWindow.SetActive(false);
		questWindowEnd.SetActive(false);
		foreach (var ice in iceFloating)
			ice.SetActive (true);
		Cursor.visible = false;

		CameraManager.Instance.ToggleCameraMoving(true);
		TimerManager.Instance.StartTimer ();
	}
			
	void InstantiateObjects ()
	{
		List<Vector3> spawnPoints = new List<Vector3>();
		Vector3 spawnPoint = new Vector3 ();
		//Create the spawnpoints
		for (int i = 0; i < numberOfBranches; i++)
		{
			do
			{
				spawnPoint.x = areaOfSpawn.position.x + Random.insideUnitCircle.x * 6 ;
				spawnPoint.y = 1.1f; // avoid its being floating in mid air
				spawnPoint.z = areaOfSpawn.position.z + Random.insideUnitCircle.y * 6;
			} while (spawnPoints.Contains(spawnPoint)); // avoid its being on the same location as another branch
			spawnPoints.Add(spawnPoint);
		}

		//Create the branches
		for (int i = 0; i < spawnPoints.Count; i++)
		{
			Vector3 spawn = spawnPoints[i];
			Instantiate(branch, spawn, Quaternion.Euler(0, Random.Range(0, 360), 0));
		}
	}    

	void SetCountText()
	{   
		if(countBranch >= numberOfBranches)
		{
			countText.text = countBranch + "/5";
			EndTutorial();
		}
		else
		{
			countText.text = countBranch + "/5";
		}
	}

	void CameraZoom()
	{
		CameraManager.Instance.CameraZoom();
	}
}