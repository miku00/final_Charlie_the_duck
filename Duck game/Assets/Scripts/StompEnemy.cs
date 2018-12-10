using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompEnemy : MonoBehaviour {

    public GameObject deathAnim;

    private Rigidbody2D playerRB;
    public float bounceForce;

	// Use this for initialization
	void Start () {
        playerRB = transform.parent.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy"){
            //Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);

            Instantiate(deathAnim, collision.transform.position, collision.transform.rotation);

            playerRB.velocity = new Vector3(playerRB.velocity.x, bounceForce, 0f);
        }
    }
}
