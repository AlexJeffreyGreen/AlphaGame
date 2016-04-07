using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayableChar : GameManager {


	public float x;
	public float y;

    [SyncVar]
	public float movementSpeed = 2.0f;

    [SyncVar]
    public float SpTimer = 2.0f;

    [SyncVar]
    public bool canShoot = true;

	Rigidbody2D rig;


	// Use this for initialization
	void Start () {
		rig = this.gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		x = Input.GetAxisRaw ("Horizontal");
		y = Input.GetAxisRaw ("Vertical");

		CmdHandleMovement (x, y);
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
            if (Input.GetButton("Fire1"))
            {
                CmdHandleSpecialAbility();
                canShoot = false;
            }
        }
	}

	[Command]
	void CmdHandleMovement(float x, float y){
		rig.velocity = new Vector2 (x, y) * movementSpeed;
	}

    [Command]
    void CmdHandleSpecialAbility(){
        Debug.Log("SpecialAbility");
    }


}
