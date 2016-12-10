using UnityEngine;
using System.IO;
using System.Collections;

public class SettingsManager : Singleton<SettingsManager>
{
	Language language;
	string currentLanguage;

	bool Sound;
	float MouseSensitivity;

	void Awake()
	{
		//Make this object permanent
		DontDestroyOnLoad(gameObject);

		//Load the language database
		currentLanguage = PlayerPrefs.GetString("Language", "English");
		language = new Language("language", currentLanguage);

		//Get settings
		Sound = PlayerPrefs.GetInt("Sound", 1) == 1 ? true : false;
		MouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 120);
	}

	void OnApplicationFocus(bool hasFocus)
	{
		if(hasFocus)
			Cursor.lockState = CursorLockMode.Confined;
	}

	public void RefreshAllData()
	{
		Sound = PlayerPrefs.GetInt("Sound", 1) == 1 ? true : false;
		MouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 120);
	}

	public void SetSound(bool sound)
	{
		Sound = sound;
		PlayerPrefs.SetInt("Sound", Sound ? 1 : 0);
	}

	public bool GetSound()
	{
		return Sound;
	}

	public void SetMouseSensitivity(float sens)
	{
		MouseSensitivity = sens * 240;
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

	public string GetLanguage()
	{
		return currentLanguage;
	}

	public string GetString(string name)
	{
		return language.getString(name);
	}
}
