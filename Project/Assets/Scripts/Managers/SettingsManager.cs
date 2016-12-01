using UnityEngine;
using System.IO;
using System.Collections;

public class SettingsManager : Singleton<SettingsManager>
{
	Language language;
	string currentLanguage;

	bool Mute;
	float MouseSensitivity;

	void Awake()
	{
		//Make this object permanent
		DontDestroyOnLoad(gameObject);

		//Load the language database
		currentLanguage = PlayerPrefs.GetString("Language", "English");
		Debug.Log(currentLanguage);
		language = new Language(Path.Combine(Application.dataPath, "language.xml"), currentLanguage);

		//Get settings
		Mute = PlayerPrefs.GetInt("Mute", 0) == 1 ? true : false;
		MouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 120);
	}

	void OnApplicationFocus(bool hasFocus)
	{
		if(hasFocus)
			Cursor.lockState = CursorLockMode.Confined;
	}

	public void SetMute(bool mute)
	{
		Mute = mute;
		PlayerPrefs.SetInt("Mute", Mute ? 1 : 0);
	}

	public bool GetMute()
	{
		return Mute;
	}

	public void SetMouseSensitivity(float sens)
	{
		MouseSensitivity = sens;
		PlayerPrefs.SetFloat("MouseSensitivity", MouseSensitivity);
	}

	public float GetMouseSensitivity()
	{
		return MouseSensitivity;
	}

	public void SetLanguage(string lang)
	{
		language.setLanguage(Path.Combine(Application.dataPath, "language.xml"), lang);
		currentLanguage = lang;
		PlayerPrefs.SetString("Language", lang);
	}

	public string GetString(string name)
	{
		return language.getString(name);
	}
}
