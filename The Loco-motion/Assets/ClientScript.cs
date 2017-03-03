using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ClientScript : NetworkBehaviour {
	public Text PositionOutput;
	public Text RotationOutput;

	[Command]
	void CmdSendPosition(Vector3 pos, Quaternion rot) {
		Debug.Log ("Sending " + pos + " " + rot);
		//PositionOutput.text = pos.ToString ();
		//RotationOutput.text = rot.ToString ();
	}


	void Update () {
		if (isClient) {
			Vector3 pos = Input.acceleration;
			Quaternion rot = Input.gyro.attitude;
			CmdSendPosition (pos, rot);
		}
	}
}
