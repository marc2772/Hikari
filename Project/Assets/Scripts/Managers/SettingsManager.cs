using UnityEngine;
using System.Collections;

public class SettingsManager : Singleton<SettingsManager>
{
	bool Mute;
	float MouseSensitivity;

	void Awake()
	{
		//Make this object permanent
		DontDestroyOnLoad(gameObject);

		//Get settings
		Mute = PlayerPrefs.GetInt("Mute", 0) == 1 ? true : false;
		MouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 120);
	}

	void SetMute(bool mute)
	{
		Mute = mute;
		PlayerPrefs.SetInt("Mute", Mute ? 1 : 0);
	}

	bool GetMute()
	{
		return Mute;
	}

	void SetMouseSensitivity(float sens)
	{
		MouseSensitivity = sens;
		PlayerPrefs.SetFloat("MouseSensitivity", MouseSensitivity);
	}

	float GetMouseSensitivity()
	{
		return MouseSensitivity;
	}
}
