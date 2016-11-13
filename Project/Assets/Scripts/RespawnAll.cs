using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RespawnAll : MonoBehaviour
{
	public List<AddHealth> branches;

	public void Respawn()
	{
		PlayerStateManager.Instance.Respawn();

		foreach(AddHealth branch in branches)
			branch.Respawn();
	}
}
