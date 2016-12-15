using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : Singleton<MenuManager>
{
	public GameObject deathMenu;
	public GameObject escapeMenu;

	void Awake()
	{
		CloseAllMenus();

		Transform panel = deathMenu.transform.Find("Panel");
		panel.Find("Text").GetComponent<Text>().text = SettingsManager.Instance.GetString("GameOver");
		panel.Find("Try Again").GetComponentInChildren<Text>().text = SettingsManager.Instance.GetString("TryAgain");
		panel.Find("Quit").GetComponentInChildren<Text>().text = SettingsManager.Instance.GetString("Quit");

		panel = escapeMenu.transform.Find("Panel");
		panel.Find("Text").GetComponent<Text>().text = SettingsManager.Instance.GetString("Menu");
		panel.Find("Try Again").GetComponentInChildren<Text>().text = SettingsManager.Instance.GetString("TryAgain");
		panel.Find("Quit").GetComponentInChildren<Text>().text = SettingsManager.Instance.GetString("Quit");
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			//If we're not dead
			if(!deathMenu.activeSelf)
			{
				escapeMenu.SetActive(!escapeMenu.activeSelf);
				CameraManager.Instance.ToggleCameraMoving(!escapeMenu.activeSelf);
				Cursor.visible = !Cursor.visible;
			}
		}
	}

	public void CloseAllMenus()
	{
		deathMenu.SetActive(false);
		escapeMenu.SetActive(false);
		Cursor.visible = false;
	}

	public void Death()
	{
		deathMenu.SetActive(true);
		CameraManager.Instance.ToggleCameraMoving(false);
		Cursor.visible = true;
	}

	public void MainMenu()
	{
		SceneManager.LoadScene("Menu");
	}
}
