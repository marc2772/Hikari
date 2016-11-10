using UnityEngine;
using System.Collections;

public class BridgeFall : MonoBehaviour
{
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.CompareTag("Player"))
			Fall();
		if(LayerMask.LayerToName(col.gameObject.layer) == "Water")
			Destroy(gameObject);
	}

	void Fall()
	{
		if(!gameObject.GetComponent<Rigidbody>())
			gameObject.AddComponent<Rigidbody>();
	}
}
