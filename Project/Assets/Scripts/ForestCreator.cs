using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ForestCreator : MonoBehaviour
{
	public List<GameObject> trees;

	void Start ()
	{
		for(int i = 0; i < 600; i++)
		{
			float x = Random.Range(-100.0f, 100.0f) + gameObject.transform.position.x;

			float z = Random.Range(-100.0f, 100.0f) + gameObject.transform.position.z;
			Vector3 position = new Vector3(x, gameObject.transform.position.y, z);

			Quaternion rotation = Quaternion.Euler(-90, Random.Range(0.0f, 360.0f), 0);

			GameObject which = trees[Random.Range(0, 10)];
			GameObject tree = (GameObject)Instantiate(which, position, rotation);
			tree.transform.localScale *= 3;
		}
	}
}
