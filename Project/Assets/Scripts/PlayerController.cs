using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float verticalSpeed;
	float distToGround;

	Rigidbody rb;

	private Animator anim;

	void Start()
	{
		anim = GetComponentInChildren<Animator>();
		rb = GetComponent<Rigidbody>();
		distToGround = GetComponent<Collider>().bounds.extents.y;
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

			anim.SetBool ("isWalking", true);
			rb.AddForce(new Vector3 (move.x, rb.velocity.y, move.z), ForceMode.Acceleration);
		}
		else
			anim.SetBool ("isWalking", false);

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

	bool IsGrounded()
	{
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.05f);
	}

	void Jump()
	{
		if(IsGrounded())
		{
			anim.SetTrigger("jump");
			SoundManager.Instance.Jump();
			rb.AddForce(Vector3.up * verticalSpeed, ForceMode.Impulse);
		}
	}
}
