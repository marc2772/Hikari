using UnityEngine;
using System.Collections;

public class SnowballRoll : MonoBehaviour
{
	public Vector3 force;
	Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate ()
	{
		rb.velocity = force;
	}

	void OnTriggerEnter(Collider col)
	{
		if(LayerMask.LayerToName(col.gameObject.layer) == "Water")
			Destroy(gameObject);
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.CompareTag("Player"))
			PlayerStateManager.Instance.Death();
	}
}
