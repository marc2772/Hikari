using UnityEngine;
using System.Collections;

public class ActivateCheckPoint : MonoBehaviour {

	public ParticleSystem inactivated;
	public ParticleSystem activated;

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.CompareTag("Player"))
		{ 
			inactivated.Stop ();
			activated.Play ();
			PlayerStateManager.Instance.ChangeSpawnPoint (transform);
		}
	}
}
