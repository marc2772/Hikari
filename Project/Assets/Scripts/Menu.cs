using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour
{
	public GameObject MainMenu;
	public GameObject PlayMenu;
	public GameObject SettingsMenu;
	public GameObject InstructionsMenu;
	public GameObject ExitMenu;

	public GameObject level2Button;

	void Start()
	{
		int level = PlayerPrefs.GetInt("Level", 1);
		if(level == 2)
			level2Button.GetComponent<Button>().interactable = true;
		
		Main();
	}

	public void Main()
	{
		//To lock the cursor within the window
		Cursor.lockState = CursorLockMode.Confined;

		MainMenu.SetActive(true);
		PlayMenu.SetActive(false);
		SettingsMenu.SetActive(false);
		InstructionsMenu.SetActive(false);
		ExitMenu.SetActive(false);
	}

	public void Play()
	{
		MainMenu.SetActive(false);
		PlayMenu.SetActive(true);
	}

	public void Level01()
	{
		SceneManager.LoadScene("Level01");
	}

	public void Level02()
	{
		SceneManager.LoadScene("Level02");
	}

	public void Settings()
	{
		MainMenu.SetActive(false);
		SettingsMenu.SetActive(true);
	}

	public void Instructions()
	{
		MainMenu.SetActive(false);
		InstructionsMenu.SetActive(true);
	}

	public void Exit()
	{
		MainMenu.SetActive(false);
		ExitMenu.SetActive(true);
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(MainMenu.activeSelf)
				Exit();
			else
				Main();
		}
	}
}
