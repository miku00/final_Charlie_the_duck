using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnRespawn : MonoBehaviour {

    private Vector3 startPos;
    private Quaternion startRotation;
    private Vector3 startLocalScale;

    private Rigidbody2D myRB;

	// Use this for initialization
	void Start () {
        startPos = transform.position;
        startRotation = transform.rotation;
        startLocalScale = transform.localScale;

        if (GetComponent<Rigidbody2D>() != null)
        {
            myRB = GetComponent<Rigidbody2D>();
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResetObject()
    {
        transform.position = startPos;
        transform.rotation = startRotation;
        transform.localScale = startLocalScale;

        if(myRB != null)
        {
            myRB.velocity = Vector3.zero;
        }
    }

}
