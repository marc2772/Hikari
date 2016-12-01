using UnityEngine;
using System.Collections;

public class Popup : MonoBehaviour
{
	Animator animator;

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Invisible"))
		{
			Destroy(gameObject);
		}
	}
}
