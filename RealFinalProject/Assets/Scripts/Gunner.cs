using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Gunner : PlayableChar {

   

    public float bulletSpeed = 5.0f;
    public Transform bulletSpawn;
    public Rigidbody2D bulletRig;
   // public float bulletTimer = 4.0f;


    public override void Update()
    {
        base.Update();

    }

    [Command]
    public override void CmdHandleSpecialAbility()
    {
        Rigidbody2D clone;
        clone = Instantiate(bulletRig, bulletSpawn.transform.position, bulletSpawn.transform.rotation) as Rigidbody2D;
        clone.velocity = bulletSpawn.transform.TransformDirection(Vector2.right * bulletSpeed);
        Debug.Log("Override");
      
    }


}
