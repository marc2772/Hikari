using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndOfLevel2 : MonoBehaviour
{
	public GameObject rockWall;
	public GameObject endingWindow;

	BoxCollider[] cols;

	void Awake()
	{
		cols = GetComponents<BoxCollider>();
	}

	void OnTriggerEnter(Collider collider)
	{
		TimerManager.Instance.StopTimer();

		SoundManager.Instance.RockFalling();
		CameraManager.Instance.StartShake();
		rockWall.SetActive(true);

		foreach(BoxCollider col in cols)
			col.enabled = false;

		StartEnding();
	}

	void StartEnding()
	{
		StartCoroutine(Ending());
	}

	IEnumerator Ending()
	{
		yield return new WaitForSeconds(2.0f);

		CameraManager.Instance.CameraEndingAnimation();

		yield return new WaitForSeconds(2.0f);

		endingWindow.SetActive(true);
		Transform panel = endingWindow.transform.Find("Panel");
		panel.Find("Title").GetComponent<Text>().text = SettingsManager.Instance.GetString("CloseCall");
		panel.Find("Description").GetComponent<Text> ().text = SettingsManager.Instance.GetString("EndingDescription");
		Transform okButton = panel.Find("Ok");
		okButton.GetComponentInChildren<Text> ().text = SettingsManager.Instance.GetString("Ok");
		okButton.GetComponent<Button>().onClick.RemoveAllListeners ();
		okButton.GetComponent<Button>().onClick.AddListener(OkButton);

		Cursor.visible = true;
	}

	void OkButton()
	{
		PlayerPrefs.SetInt("Level02", 1); //to save the level as completed
		SceneManager.LoadScene("Menu");
	}
}
