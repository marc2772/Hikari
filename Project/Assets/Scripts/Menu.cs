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
		MainMenuButtonAble();
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
			MainMenuButtonDisable();
			cam.Play();
			yield return new WaitForSeconds(0.9f);
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
			MainMenuButtonDisable();
			cam.Settings();
			yield return new WaitForSeconds(1.9f);
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
			MainMenuButtonDisable();
			cam.Instructions();
			yield return new WaitForSeconds(0.9f);
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
			MainMenuButtonDisable();
			cam.Exit();
			yield return new WaitForSeconds(0.9f);
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
		GameObject popup = (GameObject)Instantiate(deleteAllDataPopupPrefab, SettingsMenu.transform.Find("Panel"), false);
		popup.GetComponent<Text>().text = SettingsManager.Instance.GetString("DataCleared");
		level1Image.SetActive(false);
		level2Image.SetActive(false);
		ChangeLanguage();
	}

	public void ChangeLanguage()
	{
		if(SettingsMenu.activeSelf)
		{
			string lang = SettingsMenu.transform.Find("Panel/Dropdown").GetComponentInChildren<Text>().text;
			SettingsManager.Instance.SetLanguage(lang);

			UpdateAllStrings();
		}
	}

	public void UpdateAllStrings()
	{
		MainMenu.transform.Find("Play").GetComponentInChildren<Text>().text = SettingsManager.Instance.GetString("Play");
		MainMenu.transform.Find("Settings").GetComponentInChildren<Text>().text = SettingsManager.Instance.GetString("Settings");
		MainMenu.transform.Find("Instructions").GetComponentInChildren<Text>().text = SettingsManager.Instance.GetString("Instructions");
		MainMenu.transform.Find("Exit").GetComponentInChildren<Text>().text = SettingsManager.Instance.GetString("Exit");

		Transform panel = PlayMenu.transform.Find("Panel");
		panel.Find("Title").GetComponent<Text>().text = SettingsManager.Instance.GetString("Play");
		panel.Find("Level01").GetComponentInChildren<Text>().text = SettingsManager.Instance.GetString("Level01");
		panel.Find("Level02").GetComponentInChildren<Text>().text = SettingsManager.Instance.GetString("Level02");

		panel = SettingsMenu.transform.Find("Panel");
		panel.Find("Title").GetComponent<Text>().text = SettingsManager.Instance.GetString("Settings");
		panel.Find("Audio").GetComponentInChildren<Text>().text = SettingsManager.Instance.GetString("Audio");
		panel.Find("Mouse Sensitivity Text").GetComponent<Text>().text = SettingsManager.Instance.GetString("MouseSensitivity");
		panel.Find("DeleteAllData").GetComponentInChildren<Text>().text = SettingsManager.Instance.GetString("ClearAllData");
		panel.Find("Dropdown").GetComponent<Dropdown>().value = SettingsManager.Instance.GetLanguage() == "English" ? 0 : 1;

		panel = InstructionsMenu.transform.Find("Panel");
		panel.Find("Title").GetComponent<Text>().text = SettingsManager.Instance.GetString("Instructions");
		panel.Find("Text").GetComponent<Text>().text = SettingsManager.Instance.GetString("InstructionsText");

		panel = ExitMenu.transform.Find("Panel");
		panel.Find("Title").GetComponent<Text>().text = SettingsManager.Instance.GetString("AreYouSure");
		panel.Find("Yes").GetComponentInChildren<Text>().text = SettingsManager.Instance.GetString("Yes");
		panel.Find("No").GetComponentInChildren<Text>().text = SettingsManager.Instance.GetString("No");
	}

	void MainMenuButtonDisable()
	{
		Button[] buttons = MainMenu.GetComponentsInChildren<Button>();
		foreach(Button b in buttons)
			b.interactable = false;
	}

	void MainMenuButtonAble()
	{
		Button[] buttons = MainMenu.GetComponentsInChildren<Button>();
		foreach(Button b in buttons)
			b.interactable = true;
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
