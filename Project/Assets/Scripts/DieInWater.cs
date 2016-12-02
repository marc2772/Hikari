using UnityEngine;
using System.Collections;

public class DieInWater : MonoBehaviour
{
	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag ("Player")) {
			GetComponent<AudioSource> ().Play ();
			PlayerStateManager.Instance.Death ();
		}
	}
}
