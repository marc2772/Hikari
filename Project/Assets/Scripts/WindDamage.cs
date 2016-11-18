using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WindDamage : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
			TimerManager.Instance.ChangeTimeBetweenUpdates(0.25f);
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
