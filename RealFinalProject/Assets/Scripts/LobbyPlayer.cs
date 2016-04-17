using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyPlayer : NetworkLobbyPlayer {

    //Sync var
    [SyncVar]
    public bool isPlayerReady = false;
    [SyncVar]
    public bool didGameStart = false;
    [SyncVar]
    public string charClass = "None";
    [SyncVar]
    public float bg_trans = .5f;

    public GameObject networkCanvas;
    public GameObject myControlPanelPrefab;
    public GameObject myControlPanel;

    public static int readyCount = 0;
    public NetworkLobbyManager myLobby;


    void Start()
    {
        networkCanvas = GameObject.Find("CanvasLobbyMenu");
        myControlPanel = (GameObject)GameObject.Instantiate(myControlPanelPrefab);

        if (!isLocalPlayer)
        {
            myControlPanel.transform.FindChild("ReadySet").gameObject.SetActive(false);
            //myControlPanel.transform.FindChild("")
            myControlPanel.transform.FindChild("ClassSelect").gameObject.GetComponent<Dropdown>().interactable = false;
        }

        //add that shit to the canvas
        myControlPanel.transform.SetParent(GameObject.Find("PlayerHolder").transform);
        myLobby = GameObject.Find("Network").GetComponent<NetworkLobbyManager>();
        //server is the only thing that can start the game
        if (isServer)
        {
            GameObject.Find("StartGame").GetComponent<Button>().onClick.AddListener(startGame);
        }
    }//end of start

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }//end of awake


    public void startGame()
    {
        //apparently according to Dr. T, this is what "gets the party started"
        if(isServer && isLocalPlayer)
        {
            readyCount = 0;
            this.readyToBegin = true;
            didGameStart = true;
            this.SendReadyToBeginMessage();
        }
    }//end of start game 

    [Command]
    public void CmdSetR(bool R)
    {
        //Toggle is player is ready compare the numeric val with the lobby val
        if (R != isPlayerReady)
        {
            isPlayerReady = R;

            if (R)
            {
                bg_trans = 1;
                readyCount++;
            }
            else
            {
                bg_trans = .5f;
                readyCount--;
            }
        }
    }//end of command set ready function

    [Command]
    public void CmdSetCharClass(string c)
    {
        charClass = c;
    }//end of command set char class

    void Update()
    {
        //look at the user inpu and set the variables from there
        if(isLocalPlayer && myControlPanel != null)
        {
            CmdSetR(myControlPanel.transform.FindChild("ReadySet").GetComponent<Toggle>().isOn);
            switch (myControlPanel.transform.FindChild("ClassSelect").GetComponent<Dropdown>().value)
            {
                case 0:
                    CmdSetCharClass("Gunner");
                    break;
                case 1:
                    CmdSetCharClass("Ghost");
                    break;
                case 2:
                    CmdSetCharClass("Detective");
                    break;
                default:
                    break;
            }//end of switch
        }//end of if

        //print whether or not the user is ready
        if(isPlayerReady && myControlPanel != null)
        {
            myControlPanel.transform.FindChild("ReadyText").GetComponent<Text>().text = "Ready";
        }

        else if (myControlPanel != null)
        {
            myControlPanel.transform.FindChild("ReadyText").GetComponent<Text>().text = "Not Ready";
        }

        //there may be a bug with this and unity 5.2
        if (isServer && myControlPanel != null)
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
