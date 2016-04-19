using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {

    Collider2D touch;
    int count = 0;
    bool killme = false;
    float bulletTimer = 5.0f;
    public Rigidbody2D bulletRig;
	// Use this for initialization
	void Start () {
       
        bulletRig = GetComponent<Rigidbody2D>();
        
       // Destroy(this, 5.0f);//This is wicked important
    }
	
	// Update is called once per frame
	void Update () {

        //stuff
        bulletTimer -= Time.deltaTime;

        if (bulletTimer <= 0)
        {
            NetworkServer.UnSpawn(this.gameObject);
            Destroy(this.gameObject);
        }
        
       
       
    }
        
    void OnTriggerEnter2D(Collider2D C)
    {
        //Have to stop bullet here...
    }

/*
    void OnCollision(Collider2D c)
    {
        if (isServer)
        {
            touch = c;
            if (c.tag == "Wall" || c.tag == "Player")
            {
                Destroy(gameObject);
            }

            if (c.tag == "Enemy")
            {
                Destroy(gameObject);
                //must do something special
                //extra special ;)
            }
        }
    }*/
}
