using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class AddHealth : MonoBehaviour
{
	public int additionalHealth; //add a percent to healthBar

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        { 
			if(SceneManager.GetActiveScene().name == "Level01")
				if (TutorialManager.Instance.accepted && this.name == "Twig(Clone)")
					TutorialManager.Instance.countBranch++;
			Camera.main.GetComponent<AudioSource> ().Play ();
			TimerManager.Instance.AddHealth(additionalHealth);
			gameObject.SetActive(false); 
        }
    }

	public void Respawn()
	{
		gameObject.SetActive(true);
	}
}
