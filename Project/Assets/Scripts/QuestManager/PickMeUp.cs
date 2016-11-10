using UnityEngine;
using System.Collections;

public class PickMeUp : MonoBehaviour {

	private PickUpBranches questPickUp;

	// Use this for initialization
	void Start () {
		questPickUp = GameObject.Find("QuestManager").GetComponent<PickUpBranches>();
	}

	void Update () {
		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			if (this.name == "Branch(Clone)")
				questPickUp.countBranch++;
			Destroy(gameObject);
		}
	}
}