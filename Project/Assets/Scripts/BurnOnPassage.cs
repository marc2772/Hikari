using UnityEngine;
using System.Collections;

public class BurnOnPassage : MonoBehaviour {

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (player.transform.position.z >= transform.position.z) {
			GetComponent<ParticleSystem> ().Play ();
			this.enabled = false;
		}
	}
}
