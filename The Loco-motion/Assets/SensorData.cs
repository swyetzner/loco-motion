using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SensorData : NetworkBehaviour {
	private Vector3 pos;
	private Quaternion rot;

	void Update () {
		if (!isLocalPlayer)
			return;
		pos = Input.acceleration;
		rot = Input.gyro.attitude;
	}	
}
