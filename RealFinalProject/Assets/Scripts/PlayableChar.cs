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

    public Rigidbody2D rig;

    // Use this for initialization
    void Start () {
		rig = this.gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	public virtual void Update () {
		x = Input.GetAxisRaw ("Horizontal");

        //*********************************************
        //Directional changes for shooting bullets
        //*********************************************

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

		y = Input.GetAxisRaw ("Vertical");

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

        //*********************************************
        //Directional changes for shooting bullets
        //*********************************************

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
	}

    //*************************************************
    //Commands for client movement and an abstraction
    //special ability method, which is overwritten by the 
    //player classes. Gunner, Det, and Poultry
    //*************************************************

	[Command]
	void CmdHandleMovement(float x, float y){
		rig.velocity = new Vector2 (x, y) * movementSpeed;
	}

    [Command]
    public abstract void CmdHandleSpecialAbility();
  

    


}
