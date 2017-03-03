using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataType {
	public static short Data = MsgType.Highest + 1;
};

public class DataMessage : MessageBase {
	public float pos;
	public string comment;
}

public class FootScript : MonoBehaviour {
	NetworkClient client;
	public const short RegisterHostMsgId = 888;

	void ConnectToServer() {
		NetworkConnectionError error = Network.Connect ("127.0.0.1", 25000, "bears");
		Debug.Log("Could not connect to server: " + error);
	}

	public void OnConnected(NetworkMessage netMsg) {
		Debug.Log("Connected to server");
	}

	void OnConnectedToServer() {
		Debug.Log ("Connected to server");
	}
	void OnFailedToConnect(NetworkConnectionError error) {
		Debug.Log("Could not connect to server: " + error);
	}

	public void SendDataMessage() {
		DataMessage msg = new DataMessage ();
		msg.comment = "test";
		msg.pos = 5.0f;

		client.Send (DataType.Data, msg);
	}


	void Start() {
		client = new NetworkClient ();
		client.RegisterHandler (MsgType.Connect, OnConnected);
		client.Connect ("127.0.0.1", 25000);
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.C)){
			client.Connect ("127.0.0.1", 25000);
			SendDataMessage ();
			if (client.isConnected) {
				SendDataMessage ();
			} else {
				Debug.Log ("not connected");
			}
		}
	}
}
