using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Video : MonoBehaviour
{
    public MovieTexture movie;

	Renderer ren;
	AudioSource a;

	void Awake()
	{
		ren = GetComponent<Renderer>();
		a = GetComponent<AudioSource>();
	}

	void Start()
	{
        ren.material.mainTexture = movie as MovieTexture;
        a.clip = movie.audioClip;

        movie.Play() ;
        a.Play();
    }
    
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(0) || !movie.isPlaying)
			End();
	}

	void End()
	{
		movie.Stop();
		SceneManager.LoadScene("Menu");
	}
	
}
