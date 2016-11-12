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
			if (PickUpBranches.Instance.accepted && this.name == "Twig(Clone)")
				PickUpBranches.Instance.countBranch++;
			TimerManager.Instance.AddHealth(additionalHealth);
			gameObject.SetActive(false); 
        }
    }

	void Respawn()
	{
		gameObject.SetActive(true);
	}
}
