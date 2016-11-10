using UnityEngine;
using System.Collections;

public class Cinematic : MonoBehaviour {

	public GameObject mainCamera;
	public GameObject cinematicCamera;
	public float rotationTime = 10.0f;

	private float currentTime = 0.0f;

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.CompareTag("Player"))
		{
			collider.GetComponent<Rigidbody> ().isKinematic = true;
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
		}
	}
}
