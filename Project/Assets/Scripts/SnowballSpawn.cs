using UnityEngine;
using System.Collections;

public class SnowballSpawn : MonoBehaviour
{
	public GameObject snowball;

	void Start ()
	{
		InvokeRepeating("SpawnSnowball", 0.0f, 10.0f);
	}

	void SpawnSnowball()
	{
		Instantiate(snowball, transform.position, transform.rotation);
	}
}
