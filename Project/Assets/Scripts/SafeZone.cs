using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SafeZone : MonoBehaviour
{
	public WindPush windzone;

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.CompareTag("Player"))
		{
			TimerManager.Instance.ChangeTimeBetweenUpdates(1.0f);
			windzone.StopTimer ();
		}
	}


	void OnTriggerExit(Collider collider)
	{
		if (collider.gameObject.CompareTag("Player"))
		{
			TimerManager.Instance.ChangeTimeBetweenUpdates(0.25f);
			windzone.StartTimer ();
		}
	}
}