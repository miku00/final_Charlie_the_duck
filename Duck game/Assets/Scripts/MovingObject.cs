using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    public GameObject objectToMove;
    public Transform startPointOfMoving;
    public Transform endPointOfMoving;

    public float movingSpeed;

    private Vector3 currentTarget;

	// Use this for initialization
	void Start () {
        currentTarget = endPointOfMoving.position;
	}
	
	// Update is called once per frame
	void Update () {
        objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, currentTarget, movingSpeed * Time.deltaTime);


        if(objectToMove.transform.position == endPointOfMoving.position)
        {
            currentTarget = startPointOfMoving.position;
        } 

        if(objectToMove.transform.position == startPointOfMoving.position)
        {
            currentTarget = endPointOfMoving.position;
        }

		
	}
}
