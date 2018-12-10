using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject target; //Player lesz, őt kell kövesse a kamera
    public float followAhead; // mennyivel kövesse a Playert, hisz nem akarjuk, hogy a Player legyen a közepe

    private Vector3 targetPosition; //Ezt kövesse a kamera, followahead beleszámolva

    public float smoothing;


	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {


        //transform.position - kameráé

                targetPosition = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);

                if(target.transform.localScale.x > 0f)
                {
                    targetPosition = new Vector3(targetPosition.x + followAhead, transform.position.y, transform.position.z);
                }
                else
                {
                    targetPosition = new Vector3(targetPosition.x - followAhead, transform.position.y, transform.position.z);
                }

                //transform.position = targetPosition;

                //Lerp - interpoláció from és to között, t arányban
                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);

        
	}
}
