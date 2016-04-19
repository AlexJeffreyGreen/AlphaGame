using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class Gunner : PlayableChar {

   

    public float bulletSpeed = 5.0f;
    public Transform bulletSpawn;
    public GameObject bulletRig;
    
    // public float bulletTimer = 4.0f;
    public Vector2 shootingDirection;

    //public override void Update()
    //{
    //    base.Update();
    //}

    [Command]
    public override void CmdHandleSpecialAbility()
    {
      //  bulletRig = (GameObject)Resources.Load("Prefabs/Bullet", typeof (GameObject));
     //   bulletRig = (GameObject)Resources.Load("Bullet", typeof(GameObject));

        Debug.Log("Client is using ability");

       // bulletRig = GameObject.Find("/Prefabs/Bullet");
        GameObject clone = (GameObject)Instantiate(bulletRig, rig.position, Quaternion.identity);
        NetworkServer.Spawn(clone);
        
        shootingDirection = new Vector2(DIR, 0);
        

        switch (DIR)
        {
            case 3:
                shootingDirection = Vector2.down;
                break;
            case 2:
                shootingDirection = Vector2.left;
                break;
            case 1:
                shootingDirection = Vector2.up;
                break;
            case 0:
                shootingDirection = Vector2.right;
                break;
            default:
                shootingDirection = Vector2.right;
                break;

        }

        clone.transform.eulerAngles = new Vector3 (clone.transform.eulerAngles.x, clone.transform.eulerAngles.y, (DIR + 3) * 90);


        Physics2D.IgnoreCollision(clone.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        clone.GetComponent<Rigidbody2D>().velocity = shootingDirection * bulletSpeed;
        Debug.Log("Override");


        //Destroy(clone, 5.0f);
    }

    public override void CmdGhostAbility()
    {

        Debug.Log("No Ability for gunner on this key");
        //throw new NotImplementedException();
    }
}
