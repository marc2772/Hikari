using UnityEngine;
using System.Collections;

public class WindPush : MonoBehaviour
{
	
	public float windForce; //the force of the wind
	public Vector3 windDirection;
	public float repeatingPush;
	private Rigidbody rb;

	void Start ()
	{
		rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.CompareTag("Player"))
		{
			InvokeRepeating("Push", 0.0f, repeatingPush);
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.gameObject.CompareTag("Player"))
		{
			CancelInvoke();
		}
	}

	void Push()
	{
		rb.AddForce(windDirection * windForce, ForceMode.Acceleration);
	}
}
