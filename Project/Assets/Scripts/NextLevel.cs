using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class NextLevel : MonoBehaviour
{
	public AudioSource backgroundMusic;
	public Image blackImage;

	void Awake()
	{
		blackImage.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
	}

	void OnTriggerEnter(Collider col)
	{
		StartCoroutine(fadeOut());
	}

	IEnumerator fadeOut()
	{
		float volume = backgroundMusic.volume;
		while(volume > 0.1f)
		{
			volume -= Time.deltaTime;
			backgroundMusic.volume = volume;

			Color color = blackImage.color;
			color.a += 2*Time.deltaTime;
			blackImage.color = color;

			yield return new WaitForSeconds(0.08f);
		}
		backgroundMusic.volume = 0.0f;
		PlayerPrefs.SetInt("Level", 1); //to save the level as completed
		SceneManager.LoadScene("Level02");
	}
}
