using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class AddHealth : MonoBehaviour
{
	public int additionalHealth; //add a percent to healthBar

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        { 
			TimerManager.Instance.AddHealth(additionalHealth);
            Destroy(gameObject); //self destruction immediatly 
        }
    }
}
