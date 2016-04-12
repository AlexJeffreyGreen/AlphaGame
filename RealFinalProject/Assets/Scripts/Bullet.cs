using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {

    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollision(Collider2D c)
    {
        if (isServer)
        {
            if (c.tag == "Wall" || c.tag == "Player")
            {
                Destroy(gameObject);
            }

            if (c.tag == "Enemy")
            {
                Destroy(gameObject);
                //must do something special
            }
        }
    }
}
