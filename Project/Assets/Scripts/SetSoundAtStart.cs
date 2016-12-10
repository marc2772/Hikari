using UnityEngine;
using System.Collections;

public class SetSoundAtStart : MonoBehaviour
{
	void Start()
	{
		GetComponent<AudioSource>().enabled = SettingsManager.Instance.GetSound();
	}
}
