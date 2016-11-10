using UnityEngine;
using System.Collections;

public class DieInWater : MonoBehaviour
{
	void OnTriggerEnter(Collider col)
	{
		if(col.CompareTag("Player"))
			PlayerStateManager.Instance.Death();
	}
}
