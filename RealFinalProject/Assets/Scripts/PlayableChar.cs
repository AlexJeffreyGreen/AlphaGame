using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public abstract class PlayableChar : GameManager {


	public float x;
	public float y;

    
    //*************************************************
    //Sync Vars for client information
    //*************************************************

    [SyncVar]
	public float movementSpeed = 2.0f;

    [SyncVar]
    public float SpTimer = 2.0f;

    [SyncVar]
    public bool canShoot = true;

    [SyncVar]
    public int DIR;

    [SyncVar]
    public bool ghostAbil = true;

    [SyncVar]
    public float ghostAbilTimer = 6.0f;


    public Rigidbody2D rig;

    public GameObject bulletObj;



    // Use this for initialization
    void Start () {
		rig = this.gameObject.GetComponent<Rigidbody2D> ();
        bulletObj = GameObject.FindGameObjectWithTag("bullet");
       // Debug.Log(bulletObj);
       // bulletRig = GameObject.Find("Bullet");
	}

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        Debug.Log("INSIDE UPDATE");
        if (isLocalPlayer)
        {
            Debug.Log("Inside local player");
            x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Vertical");

            //*********************************************
            //Directional changes for shooting bullets
            //*********************************************



            //*********************************************
            //Directional changes for shooting bullets
            //*********************************************
            Debug.Log("Sending " + x + "," + y);
            CmdHandleMovement(x, y);


            //*********************************************
            //Handling the shooting variable based on a timer
            //which is based on Time delta time
            //*********************************************


            if (canShoot == false)
            {
                if (SpTimer > 0)
                {
                    SpTimer -= Time.deltaTime;
                }

                if (SpTimer <= 0)
                {
                    canShoot = true;
                    SpTimer += 2.0f;
                }
            }

            if (canShoot == true)
            {
                if (Input.GetButton("Jump"))
                {
                    CmdHandleSpecialAbility();
                    canShoot = false;
                }
            }

            //********************************************
            //End of time delta timer for bullet movement
            //********************************************

            if (ghostAbil == false)
            {
                if (ghostAbilTimer > 0)
                {
                    ghostAbilTimer -= Time.deltaTime;
                }

                if (ghostAbilTimer <= 0)
                {
                    ghostAbil = true;
                    ghostAbilTimer += 6.0f;
                }
            }

            if (ghostAbil == true)
            {
                if (Input.GetKey("p"))
                {
                    CmdGhostAbility();
                    ghostAbil = false;
                }
            }
        }
    }

    //*************************************************
    //Commands for client movement and an abstraction
    //special ability method, which is overwritten by the 
    //player classes. Gunner, Det, and Poultry
    //*************************************************

	[Command]
	void CmdHandleMovement(float xc, float yc)
    {
        x = xc;
        y = yc;
        Debug.Log("Client called handle movement");
        if (x < 0)
        {
            DIR = 2;
            Debug.Log(DIR);
        }

        if (x > 0)
        {
            DIR = 0;
            Debug.Log(DIR);
        }

        //y = Input.GetAxisRaw("Vertical");

        if (y > 0)
        {
            DIR = 1;
            Debug.Log(DIR);
        }

        if (y < 0)
        {
            DIR = 3;
            Debug.Log(DIR);
        }
        rig.velocity = new Vector2 (x, y) * movementSpeed;
	}

    [Command]
    public abstract void CmdHandleSpecialAbility();

    [Command]
    public abstract void CmdGhostAbility();
    


}
