using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WindDamage : MonoBehaviour
{
	public float newTimeUpdate;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
			TimerManager.Instance.ChangeTimeBetweenUpdates(newTimeUpdate);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
			TimerManager.Instance.ChangeTimeBetweenUpdates(1.0f);
        }
    }
}
