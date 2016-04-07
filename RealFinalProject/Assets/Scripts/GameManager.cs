using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {

	public int currentLevel;
	public int currentGameMode;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (NetworkServer.active) {
			UpdateServer ();
		}

		if (NetworkClient.active) {
			UpdateClient ();
		}
	}

	void UpdateServer(){
		
	}

	void UpdateClient(){
	
	}
}
