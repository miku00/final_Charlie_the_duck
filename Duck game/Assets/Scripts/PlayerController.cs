using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    /*
     * Mozgáshoz tartozik
     * */
    public float moveSpeed;
    public Rigidbody2D myRigidbody;

    public float jumpSpeed;


    public Transform groundCheck;
    public float groundCheckedRadius;
    public LayerMask whatIsGround; //mi is a föld

    public bool isGrounded;

    public Vector3 respawnPosition;
    public LevelManager levelManager;

    public GameObject stompBox;

    public float knockBackForce;
    public float knockBackLength;

    private float knockBackCounter;


    //animation
    private Animator myAnimator;

    //Sound
    public AudioSource jumpSound;
    public AudioSource hurtSound;


    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        respawnPosition = transform.position;

        levelManager = FindObjectOfType<LevelManager>();

	}
	
	// Update is called once per frame
	void Update () {
           
        //ellenőrzi, hogy a Ground-on van-e, ugrásnál kell!
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckedRadius, whatIsGround);

        if (knockBackCounter <= 0)
        {
            //jobbra
            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(3f, 3f, 3f);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0f) //balra
            {
                myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(-3f, 3f, 3f);
            }
            else
            {
                myRigidbody.velocity = new Vector3(0f, myRigidbody.velocity.y, 0f);
            }

            //ugrás
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpSpeed, 0f);
                jumpSound.Play();
            }

            levelManager.invincible = false;
        }

        if(knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;

            if (transform.localScale.x > 0)
            {
                myRigidbody.velocity = new Vector3(-knockBackForce, knockBackForce, 0f);
            }
            else
            {
                myRigidbody.velocity = new Vector3(knockBackForce, knockBackForce, 0f);
            }
        }

        //ugrás animációhoz, átadjuk mikor ér a földhöz
        myAnimator.SetBool("Grounded", isGrounded);

        if(myRigidbody.velocity.y < 0)
        {
            stompBox.SetActive(true);
        }
        else
        {
            stompBox.SetActive(false);
        }
    }

    public void KnockBack()
    {
        knockBackCounter = knockBackLength;

        levelManager.invincible = true;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "KillArea")
        {
            levelManager.Respawn();
        }
        if(other.tag == "Checkpoint")
        {
            respawnPosition = other.transform.position;
        }
        if(other.tag == "LadyDuck")
        {
            levelManager.congratulationScreen.SetActive(true);
            levelManager.StartCoroutine("OnRestartGame");
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = collision.transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }
}
