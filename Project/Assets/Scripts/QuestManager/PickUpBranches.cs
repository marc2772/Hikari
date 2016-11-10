using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PickUpBranches : MonoBehaviour
{
	public GameObject branch;
	private const int numberOfBranches = 6;
	[Range(0, numberOfBranches)]
	public int countBranch;

	public Transform areaOfSpawn;
	public GameObject[] iceFloating;

	private bool showGUI = true;
	private bool accepted = false;
	private bool finished = false;
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		player.GetComponent<Rigidbody> ().isKinematic = true;
		// TimerManager.Instance.StopTimer ();

		countBranch = 0;        
	}

	void OnGUI () {
		if (showGUI) {
			int BoxWidth = 300;
			int BoxHeight = 150;
			GUI.BeginGroup (new Rect ((Screen.width - BoxWidth) / 2, (Screen.height - BoxHeight) / 2, BoxWidth, BoxHeight));
			GUI.Box (new Rect (0, 0, BoxWidth, BoxHeight), "\" Hold your breath! \"");
			GUI.Label (new Rect (10, 20, BoxWidth - 10, BoxHeight), "You're holding you're beath! It's so cold and windy out there, maybe you should collect some branches to let you breath again before leaving the camp ...");
			if (GUI.Button (new Rect (110, 90, 80, 50), "Accept")) {
				showGUI = false;
				accepted = true;
				InstantiateObjects ();
				player.GetComponent<Rigidbody> ().isKinematic = false;
			}         
			GUI.EndGroup ();
		} else if (accepted)
			SetCountText ();
		else if (finished) 
		{
			int BoxWidth = 300;
			int BoxHeight = 150;
			GUI.BeginGroup (new Rect ((Screen.width - BoxWidth) / 2, (Screen.height - BoxHeight) / 2, BoxWidth, BoxHeight));
			GUI.Box (new Rect (0, 0, BoxWidth, BoxHeight), "\" Into the wild! \"");
			GUI.Label (new Rect (10, 20, BoxWidth - 10, BoxHeight), "Warmer now ? Fine, let's have a go ahead! See? Ice blocks in front of the campfire, maybe you should try and jump your way trough ...");
			if (GUI.Button (new Rect (110, 90, 80, 50), "Let's go")) {
				finished = false;
				foreach (var ice in iceFloating)
					ice.SetActive (true);
				player.GetComponent<Rigidbody> ().isKinematic = false;
				// TimerManager.Instance.StartTimer ();
			}         
			GUI.EndGroup ();
		}
	}

	void InstantiateObjects ()
	{
		List<Vector3> spawnPoints = new List<Vector3>();
		Vector3 spawnPoint = new Vector3 ();
		for (int i = 0; i < numberOfBranches; i++)
		{
			do
			{
				spawnPoint.x = areaOfSpawn.position.x + Random.insideUnitCircle.x * 6 ;
				spawnPoint.y = 1.2f; // avoid its being floating in mid air
				spawnPoint.z = areaOfSpawn.position.z + Random.insideUnitCircle.y * 6;
			} while (spawnPoints.Contains(spawnPoint)); // avoid its being on the same location as another branch
			spawnPoints.Add(spawnPoint);
		}

		for (int i = 0; i < spawnPoints.Count; i++)
		{
			Vector3 spawn = spawnPoints[i];
			Instantiate(branch, spawn, Quaternion.Euler(0, Random.Range(0, 360), 90));
		}
	}    

	void SetCountText()
	{   
		if (countBranch >= numberOfBranches)
		{
			accepted = false;
			finished = true;
			player.GetComponent<Rigidbody> ().isKinematic = true;
		}
		else
		{
			int BoxWidth = 220;
			int BoxHeight = 100;
			GUI.BeginGroup(new Rect(0, (Screen.height - BoxHeight) / 2, BoxWidth, BoxHeight));
			GUI.Box(new Rect(0, 0, BoxWidth, BoxHeight), "Don't loose your breath!");
			if (countBranch > 0)
				GUI.Label(new Rect(20, 60, BoxWidth - 20, 20), "Branches : " + countBranch.ToString() + " / " + numberOfBranches);
			else
				GUI.Label(new Rect(20, 60, BoxWidth - 20, 20), "Collect branches ...");
			GUI.EndGroup();
		}        
	}
}
