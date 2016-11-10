using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float verticalSpeed;
	bool grounded = false;

	Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		//Move
		float inputX = Input.GetAxis("Horizontal");
		float inputZ = Input.GetAxis("Vertical");

		//Set the rotation if we move
		if(inputX != 0 || inputZ != 0)
		{
			Quaternion rotation = Quaternion.Euler(0.0f, Camera.main.transform.rotation.eulerAngles.y, 0.0f);
			rb.rotation = rotation;

			//Now add the force
			Vector3 moveX = transform.right * inputX * speed;
			Vector3 moveZ = transform.forward * inputZ * speed;

			Vector3 move = moveX + moveZ;

			rb.AddForce(new Vector3 (move.x, rb.velocity.y, move.z), ForceMode.Acceleration);
		}

		rb.freezeRotation = true;
	}

	void Update ()
	{
		//Jump
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
		}
	}

	void Jump()
	{
		if(grounded)
		{
			rb.AddForce(Vector3.up * verticalSpeed, ForceMode.Impulse);
			grounded = false;
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if(transform.position.y - col.contacts[0].point.y > 0.12)
			grounded = true;
	}
}
