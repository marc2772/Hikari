using UnityEngine;
using System.Collections;

public class WindPush : MonoBehaviour
{
	public float windForce; //the force of the wind
	public Vector3 windDirection;
	public float repeatingPush;
	private Rigidbody rb;

	float timeBetweenUpdates = 0.05f;
	IEnumerator coroutine;

	void Awake()
	{
		coroutine = Push ();
	}

	void Start ()
	{
		rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.CompareTag("Player"))
		{
			StartTimer ();
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.gameObject.CompareTag("Player"))
		{
			StopTimer ();
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

	IEnumerator Push()
	{
		while(true)
		{
			rb.AddForce(windDirection * windForce, ForceMode.Acceleration);

			yield return new WaitForSeconds(timeBetweenUpdates);
		}
	}
}