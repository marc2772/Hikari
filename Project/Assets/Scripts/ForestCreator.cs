using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ForestCreator : MonoBehaviour
{
	public List<GameObject> trees;

	void Start ()
	{
		StartCoroutine(spawnTree());
	}

	IEnumerator spawnTree()
	{
		for(int i = 0; i < 100; i++)
		{
			float x = Random.Range(-1.0f, 1.0f) + gameObject.transform.position.x;
			yield return new WaitForSeconds(0.1f);

			float z = Random.Range(-1.0f, 1.0f) + gameObject.transform.position.z;
			Vector3 position = new Vector3(x, gameObject.transform.position.y, z);
			yield return new WaitForSeconds(0.1f);

			Quaternion rotation = Quaternion.Euler(-90, Random.Range(0.0f, 360.0f), 0);
			yield return new WaitForSeconds(0.1f);

			GameObject which = trees[Random.Range(0, 10)];
			GameObject tree = (GameObject)Instantiate(which, position, rotation);
			tree.transform.localScale *= 3;
		}
	}
}
