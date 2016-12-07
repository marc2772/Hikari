using UnityEngine;
using System.Collections;

public class EndOfLevel2 : MonoBehaviour
{
	public GameObject rockWall;
	public AudioClip rockFallingSound;

	BoxCollider[] cols;

	void Awake()
	{
		cols = GetComponents<BoxCollider>();
	}

	void OnTriggerEnter(Collider collider)
	{

		CameraManager.Instance.StartShake();
		rockWall.SetActive(true);

		foreach(BoxCollider col in cols)
			col.enabled = false;
	}
}
