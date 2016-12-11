using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RespawnAll : MonoBehaviour
{
	public List<AddHealth> branches;
	public List<WindPush> windPush;

	public void Respawn()
	{
		
		PlayerStateManager.Instance.Respawn();

		foreach(AddHealth branch in branches)
			branch.Respawn();
		foreach(WindPush w in windPush)
			w.StopTimer();
	}
}
