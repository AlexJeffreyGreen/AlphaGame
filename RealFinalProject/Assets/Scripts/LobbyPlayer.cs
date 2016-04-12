using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyPlayer : NetworkLobbyPlayer {

    [SyncVar]
    public bool isPlayerReady = false;


    public GameObject myControlPanelPrefab;
    public GameObject myControlPanel;

    public static int readyCount = 0;

    public NetworkLobbyManager myLobby;


	// Use this for initialization
	void Start () {

        readyCount = 0;
        myControlPanel = (GameObject)GameObject.Instantiate(myControlPanelPrefab);
        if (!isLocalPlayer)
        {
            myControlPanel.transform.FindChild("ReadySet").gameObject.SetActive(false);
        }

        myControlPanel.transform.SetParent(GameObject.Find("Canvas").transform);
        myLobby = GameObject.Find("Network").GetComponent<NetworkLobbyManager>();
        if (isServer)
        {
            GameObject.Find("StartGame").GetComponent<NetworkLobbyManager>();
        }
	}

    public void startGame()
    {
        if (isServer)
        {
            this.SendReadyToBeginMessage();
        }
    }

    [Command]
    public void CmdSetR(bool R)
    {
        if (R != isPlayerReady)
        {
            isPlayerReady = R;
            if (R)
            {
                readyCount++;
            }
            else
            {
                readyCount--;
            }
        }
    }

    
	
	// Update is called once per frame
	void Update () {

        if (isLocalPlayer)
        {
            CmdSetR(myControlPanel.transform.FindChild("ReadySet").GetComponent<Toggle>().isOn);
            //CmdSetClass(myControlPanel.transform.FindChild("Class").GetComponent<Dropdown>().ToString());
            //CmdSetTeam(myControlPanel.transform.FindChild("Team").GetComponent<Dropdown>().ToString());
        }
        if (isPlayerReady)
        {
            myControlPanel.transform.FindChild("ReadyText").GetComponent<Text>().text = "Ready";
        }
        else
        {
            myControlPanel.transform.FindChild("ReadyText").GetComponent<Text>().text = "Not Ready";
        }

        if (isServer)
        {
            if (readyCount == myLobby.numPlayers)
            {
                GameObject.Find("StartGame").GetComponent<Button>().interactable = true;
            }
            else
            {
                GameObject.Find("StartGame").GetComponent<Button>().interactable = false;
            }
        }
    }
}
