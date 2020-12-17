using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed;
    private float initialMoveSpeed;
    public float speedMultiplier;
    public float speedIncreaseMilestone;
    private float initialSpeedIncreaseMilestone;
    private float nextSpeedMilestone;

    public float jumpForce;
    public float jumpDuration;
    public bool grounded;
    public LayerMask groundLayer;
    private bool stoppedJumping;
    private bool canDoubleJump;

    public Transform groundCheck;
    public float groundCheckRadius;

    public GameManager gameManager;

    public AudioSource jumpSound;
    public AudioSource deathSound;

    private Rigidbody2D myRigidBody;
    private Animator myAnimator;

    private float jumpTimeCounter;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        jumpTimeCounter = jumpDuration;
        nextSpeedMilestone = speedIncreaseMilestone;

        initialMoveSpeed = moveSpeed;
        initialSpeedIncreaseMilestone = speedIncreaseMilestone;
        stoppedJumping = true;
        canDoubleJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if(transform.position.x > nextSpeedMilestone)
        {
            speedIncreaseMilestone *= speedMultiplier;
            nextSpeedMilestone += speedIncreaseMilestone;
            moveSpeed *= speedMultiplier;
        }
        myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);

        myAnimator.SetFloat("Speed", myRigidBody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);

        if ((EventSystem.current.IsPointerOverGameObject() && !Input.GetKey(KeyCode.Space)) || moveSpeed == 0)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (grounded)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                stoppedJumping = false;
                jumpSound.Play();
            } else if (canDoubleJump)
            {
                canDoubleJump = false;
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                jumpTimeCounter = jumpDuration;
                stoppedJumping = false;
                jumpSound.Play();
            }
        }

        if((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && !stoppedJumping)
        {
            if(jumpTimeCounter > 0)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }

        if(grounded)
        {
            jumpTimeCounter = jumpDuration;
            canDoubleJump = true;
        }
    }

    public void Reset()
    {
        moveSpeed = initialMoveSpeed;
        myAnimator.SetBool("Dead", false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "killbox")
        {
            myAnimator.SetBool("Dead", true);
            deathSound.Play();
            moveSpeed = 0;
            speedIncreaseMilestone = initialSpeedIncreaseMilestone;
            nextSpeedMilestone = speedIncreaseMilestone;

            gameManager.RestartGame();
        }
    }
}
