using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HeadScript : MonoBehaviour {
	private int connectCount = 1;
	NetworkServerSimple server;

	void LaunchServer() {
		Network.incomingPassword = "bears";
		bool useNat = !Network.HavePublicAddress ();
		Network.InitializeServer (2, 25000, useNat);
	}
	void OnServerInitialized() {
		Debug.Log("Server initialized and ready");
	}
	void OnPlayerConnected(NetworkPlayer player) {
		Debug.Log ("Device " + connectCount++ + " connected from  " + player.ipAddress + ":" + player.port);
	}

	void OnConnected(NetworkMessage netMsg) {
		Debug.Log ("Server connected");
	}
		
	public void OnPos(NetworkMessage netMsg) {
		DataMessage msg = netMsg.ReadMessage<DataMessage> ();
		Debug.Log ("OnPos " + msg.comment + " " + msg.pos);
	}

	void Start() {
		server = new NetworkServerSimple ();
		server.RegisterHandler (MsgType.Connect, OnConnected);
		server.RegisterHandler (DataType.Data, OnPos);
		server.Initialize ();
		ConnectionConfig config = new ConnectionConfig ();
		config.AddChannel(QosType.Reliable);
		server.Configure (config, 2);
		server.Listen (25000);
		Debug.Log("Server Listening on " + server.listenPort);
	}

	void Update() {
	}
		
}
