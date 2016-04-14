using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyPlayer : NetworkLobbyPlayer {

    [SyncVar]
    public string playerClassChoice;

    [SyncVar]
    public bool isReady = false;


   void Start()
    {

    }

    void Update()
    {



    }



    [Command]
    public void CmdreadyToPlay(bool R)
    {
       // return R;
    }

    [Command]
    public void CmdplayerClass(string playerClass)
    {
        //return playerClass;
    }


}
