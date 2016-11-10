using UnityEngine;
using System.Collections;

public class MenuManager : Singleton<MenuManager>
{
	public GameObject deathMenu;
	public GameObject escapeMenu;

	void Start()
	{
		CloseAllMenus();
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(!deathMenu.activeSelf)
			{
				escapeMenu.SetActive(!escapeMenu.activeSelf);
				CameraManager.Instance.ToggleCameraMoving(!escapeMenu.activeSelf);
			}
		}
	}

	public void CloseAllMenus()
	{
		deathMenu.SetActive(false);
		escapeMenu.SetActive(false);
	}

	public void Death()
	{
		deathMenu.SetActive(true);
		CameraManager.Instance.ToggleCameraMoving(false);
	}
}
