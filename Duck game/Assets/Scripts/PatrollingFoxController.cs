using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingFoxController : MonoBehaviour {

    public Transform leftPoint;
    public Transform rightPoint;

    public float moveSpeed;
    private Rigidbody2D myRigidbody;
    private SpriteRenderer mySR;

    public bool rightFaced;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
        if(rightFaced && transform.position.x > rightPoint.position.x)
        {
            rightFaced = false;
            mySR.flipX = true;
        }
        if(!rightFaced && transform.position.x < leftPoint.position.x)
        {
            rightFaced = true;
            mySR.flipX = false;
        }

        if (rightFaced)
        {
            myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0f);
        }
        else
        {
            myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
        }

	}
}
