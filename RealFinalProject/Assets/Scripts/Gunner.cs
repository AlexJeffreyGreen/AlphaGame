using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Gunner : PlayableChar {

   

    public float bulletSpeed = 5.0f;
    public Transform bulletSpawn;
    public GameObject bulletRig;
    // public float bulletTimer = 4.0f;
    public Vector2 shootingDirection;


    public override void Update()
    {
        base.Update();
    }

    [Command]
    public override void CmdHandleSpecialAbility()
    {
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


}
