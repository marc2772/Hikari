using UnityEngine;
using System.Collections;

public class DieInWater : MonoBehaviour
{
	void OnTriggerEnter(Collider col)
	{
		PlayerStateManager.Instance.Death();
	}
}
