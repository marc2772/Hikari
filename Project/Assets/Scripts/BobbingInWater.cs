using UnityEngine;
using System.Collections;

public class BobbingInWater : MonoBehaviour
{
	float maxBobbingRange;
	bool goingUp;
	float speed;
	float currentDelta;

	void Start()
	{
		maxBobbingRange = 0.1f;

		//Randomize the speed and starting position
		speed = Random.Range(0.01f, 0.1f);
		goingUp = Random.value < 0.5f;
		currentDelta = Random.Range(0.0f, maxBobbingRange);

		Vector3 newPos = transform.position;
		newPos.y = goingUp ? (newPos.y + currentDelta) : (newPos.y - currentDelta);
		transform.position = newPos;
	}

	void Update()
	{
		if(goingUp)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * speed, transform.position.z);
			currentDelta += Time.deltaTime * speed;
			if(currentDelta >= maxBobbingRange)
			{
				goingUp = false;
				currentDelta = -0.1f;
			}
		}
		else
		{
			transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * speed, transform.position.z);
			currentDelta += Time.deltaTime * speed;
			if(currentDelta >= maxBobbingRange)
			{
				goingUp = true;
				currentDelta = -0.1f;
			}
		}
	}
}
