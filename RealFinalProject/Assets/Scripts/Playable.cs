using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Playable : GameManager {

//****************************************
// Sync Vars
//****************************************

	[SyncVar]
	public int Ammo;

	[SyncVar]
	public bool canShoot;

	[SyncVar]
	public bool canUseSpAbil;

	[SyncVar]
	public int batteryLife;

	[SyncVar]
	public float seeingDistance;

	[SyncVar]
	public float movementSpeed = 10.0f;

	[SyncVar]
	public float specialAbilTimer = 2.0f;


//*************************************************
// End of Sync Vars
//*************************************************


//****Generic Rig/Animator**********
	public Rigidbody2D Rig;
	public Animator anim;
	public Vector2 playerPos;
//**********************************



	// Use this for initialization
	void Start () {
	//Instantiate Rig & Animator
		Rig = this.gameObject.GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
	//Movement vars
		float x = 0;
		float y = 0;

		//if local
		if (isLocalPlayer) {
			Debug.Log ("Local");
		//Assign the x & y vars to the inputs at horizontal and vertical
			x = Input.GetAxisRaw ("Horizontal");
			y = Input.GetAxisRaw ("Vertical");

			CmdPlayerMove (x, y);
		//IF Can use ability is false
			if (canUseSpAbil == false) {
				//Decrement timer using Time.delta time
				if (specialAbilTimer > 0) {
					specialAbilTimer -= Time.deltaTime;
				}
				// if timer is less than or equal to zero
				if (specialAbilTimer <= 0) {
					//reset and assign true to can use ability
					specialAbilTimer = 2.0f;
					canUseSpAbil = true;
				}
			
			}
		
//*************************************************
// If isServer
//*************************************************
		if (isServer) {
		//Assign rig velocity
			Rig.velocity = new Vector2 (x, y) * movementSpeed;
			//Same timer loop for using ability
			if (canUseSpAbil == false){
				if (specialAbilTimer > 0){
					specialAbilTimer -= Time.deltaTime;
				}

				if (specialAbilTimer <= 0){
					specialAbilTimer = 2.0f;
					canUseSpAbil = true;
				}

			} if (canUseSpAbil == true){
				CmdSpecialAbility();
			}
		
		}
//*************************************************
//Calls command move regardless of server or client
//*************************************************
		else {
		
			CmdPlayerMove (x, y);
			if (Input.GetButton("Fire1") && canUseSpAbil == true){
				CmdSpecialAbility ();
			}
		}

	}
	}

	[Command]
	public void CmdUseFlashlight(){
	
	}

	[Command]
	public void CmdSpecialAbility(){
	
	}

	[Command]
	public void CmdCollectBattery(){
	
	}

	[Command]
	private void CmdMelee(){
	
	}

	[Command]
	private void CmdDecoy(){
	
	}

	//Handles movement for local player
	[Command]
	public void CmdPlayerMove(float x, float y){
		//x = transform.position.x + (Input.GetAxisRaw("Horizontal") * movementSpeed);
		//y = transform.position.y + (Input.GetAxisRaw("Vertical") * movementSpeed);
		//playerPos = new Vector2(x, y);
		//transform.position = playerPos;

		Debug.Log("Hello");
		//Velocity of rig set based on provided parameters * movement speed
		Rig.velocity = new Vector3 (x, y, 0) * movementSpeed;


	}

	void OnCollision(Collider C){
		if (C.tag == "Player") {
			//do stuff
			//if bullet do other stuff
		}

		if (C.tag == "Wall") {

			//For bullet destory the bullet if it hits wall
		
		}
	}
}
