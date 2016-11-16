using UnityEngine;
using System.Collections;

public class Cinematic : MonoBehaviour {

	public GameObject mainCamera;
	public GameObject cinematicCamera;
	public float rotationTime = 10.0f;

	private GameObject player;
	private float currentTime = 0.0f;
	private bool showGUI = false;

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.CompareTag("Player"))
		{
			TimerManager.Instance.StopTimer ();
			player = collider.gameObject;
			player.GetComponent<Rigidbody> ().isKinematic = true;
			player.transform.position = gameObject.transform.position;
			player.transform.LookAt (Vector3.zero);
			mainCamera.SetActive(false);
			cinematicCamera.SetActive (true);
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if (cinematicCamera.activeSelf == true && currentTime <= rotationTime) 
		{
			cinematicCamera.transform.RotateAround (cinematicCamera.transform.position, Vector3.up, 360.0f * Time.deltaTime / rotationTime);
			currentTime += Time.deltaTime;
			if (currentTime >= rotationTime) {
				showGUI = true;
				Cursor.visible = !Cursor.visible;
			}
		}
	}

	void OnGUI () {
		if (showGUI) {
			int BoxWidth = 300;
			int BoxHeight = 150;
			GUI.BeginGroup (new Rect ((Screen.width - BoxWidth) / 2, (Screen.height - BoxHeight) / 2, BoxWidth, BoxHeight));
			GUI.Box (new Rect (0, 0, BoxWidth, BoxHeight), "\" What have you done? \"");
			GUI.Label (new Rect (10, 20, BoxWidth - 10, BoxHeight), "Look all around! It's all burned: what have you done? Are you sure you want to keep on searching for the flame you're linked to? ...");
			if (GUI.Button (new Rect (110, 90, 80, 50), "Yes!")) {
				showGUI = false;
				mainCamera.SetActive(true);
				cinematicCamera.SetActive (false);
				Cursor.visible = !Cursor.visible;
				player.transform.position = new Vector3 (-2, 40, 360); // on top of cave entrance
				player.GetComponent<Rigidbody> ().isKinematic = false;
			}         
			GUI.EndGroup ();
		}
	}
}
