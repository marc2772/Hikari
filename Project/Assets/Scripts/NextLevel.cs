using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NextLevel : MonoBehaviour
{

	void OnTriggerEnter(Collider col)
	{
		SceneManager.LoadScene("Level02");
	}
}
