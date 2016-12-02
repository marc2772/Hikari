using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour
{
	public MenuCameraAnimation cam;

	public GameObject MainMenu;
	public GameObject PlayMenu;
	public GameObject SettingsMenu;
	public GameObject InstructionsMenu;
	public GameObject ExitMenu;

	public GameObject deleteAllDataPopupPrefab;

	public GameObject level1Image;
	public GameObject level2Image;

	void Start()
	{
		int level = PlayerPrefs.GetInt("Level", 0);
		if(level >= 1)
			level1Image.SetActive(true);
		if(level >= 2)
			level2Image.SetActive(true);

		UpdateAllStrings();

		PlayMenu.SetActive(false);
		SettingsMenu.SetActive(false);
		InstructionsMenu.SetActive(false);
		ExitMenu.SetActive(false);
		MainMenu.SetActive(true);
	}

	public void Main()
	{
		//To lock the cursor within the window
		Cursor.lockState = CursorLockMode.Confined;

		if(PlayMenu.activeSelf)
		{
			if(!cam.IsPlaying())
			{
				PlayMenu.SetActive(false);
				cam.PlayBackwards();
			}
		}
		else if(SettingsMenu.activeSelf)
		{
			if(!cam.IsPlaying())
			{
				SettingsMenu.SetActive(false);
				cam.SettingsBackwards();
			}
		}
		else if(InstructionsMenu.activeSelf)
		{
			if(!cam.IsPlaying())
			{
				InstructionsMenu.SetActive(false);
				cam.InstructionsBackwards();
			}
		}
		else if(ExitMenu.activeSelf)
		{
			if(!cam.IsPlaying())
			{
				ExitMenu.SetActive(false);
				cam.ExitBackwards();
			}
		}

		MainMenu.SetActive(true);
	}

	public void Level01()
	{
		SceneManager.LoadScene("Level01");
	}

	public void Level02()
	{
		SceneManager.LoadScene("Level02");
	}

	public void Play()
	{
		StartCoroutine(PlayCoroutine());
	}

	IEnumerator PlayCoroutine()
	{
		if(!cam.IsPlaying())
		{
			cam.Play();
			yield return new WaitForSeconds(0.7f);
			PlayMenu.SetActive(true);
		}
	}

	public void Settings()
	{
		StartCoroutine(SettingsCoroutine());
	}

	IEnumerator SettingsCoroutine()
	{
		if(!cam.IsPlaying())
		{
			cam.Settings();
			yield return new WaitForSeconds(1.7f);
			SettingsMenu.SetActive(true);
		}
	}

	public void Instructions()
	{
		StartCoroutine(InstructionsCoroutine());
	}

	IEnumerator InstructionsCoroutine()
	{
		if(!cam.IsPlaying())
		{
			cam.Instructions();
			yield return new WaitForSeconds(0.7f);
			InstructionsMenu.SetActive(true);
		}
	}

	public void Exit()
	{
		StartCoroutine(ExitCoroutine());
	}

	IEnumerator ExitCoroutine()
	{
		if(!cam.IsPlaying())
		{
			cam.Exit();
			yield return new WaitForSeconds(0.7f);
			ExitMenu.SetActive(true);
		}
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void DeleteAllData()
	{
		PlayerPrefs.DeleteAll();
		Instantiate(deleteAllDataPopupPrefab, SettingsMenu.transform.Find("Panel"), false);
	}

	public void ChangeLanguage()
	{
		string lang = SettingsMenu.transform.Find("Panel/Dropdown").GetComponentInChildren<Text>().text;
		SettingsManager.Instance.SetLanguage(lang);

		UpdateAllStrings();
	}

	public void UpdateAllStrings()
	{
		MainMenu.transform.Find("Play").GetComponentInChildren<Text>().text = SettingsManager.Instance.GetString("Play");
		MainMenu.transform.Find("Settings").GetComponentInChildren<Text>().text = SettingsManager.Instance.GetString("Settings");
		MainMenu.transform.Find("Instructions").GetComponentInChildren<Text>().text = SettingsManager.Instance.GetString("Instructions");
		MainMenu.transform.Find("Exit").GetComponentInChildren<Text>().text = SettingsManager.Instance.GetString("Exit");

		/*PlayMenu;
		SettingsMenu;
		InstructionsMenu;
		ExitMenu;*/
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(!PlayMenu.activeSelf &&
				!SettingsMenu.activeSelf &&
				!InstructionsMenu.activeSelf &&
				!ExitMenu.activeSelf)
				Exit();
			else
				Main();
		}
	}
}
