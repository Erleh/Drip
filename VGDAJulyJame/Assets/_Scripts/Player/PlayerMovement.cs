using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerState playerState;

    [SerializeField]
    private Animator pAnim;

    [SerializeField]
    private Rigidbody2D playerRB;
    [SerializeField]
    private bool findPlayerRB = true;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private Vector2 axes;

    //Direction Determination Variables
    public Direction PlayerDir;
    //Idle Determination Variables
    [SerializeField]
    private float idleTimer;
    [SerializeField]
    private float idleLimit;

    private bool idle, setRandomIdle, pfu, pfd;

    public enum Direction{  Up, Down, Left, Right   }
    [SerializeField]
    private bool pushing;
    private bool moving;
    private void Awake()
    {
        pAnim = GetComponent<Animator>();
    }

    void Start ()
    {
        PlayerDir = Direction.Down;
        if(findPlayerRB)
            playerRB = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate ()
    {
        idleTimer += Time.deltaTime;
        idle = CheckIdle();
        pAnim.SetBool("isIdle", idle);
        pAnim.SetBool("colliding", pushing);
        MakeMovement();

        playerRB.velocity = new Vector2(0, 0); //comment out this line for non-physics movement
	}

    //Determines direction of the player using movement axes
    void DetermineDirection()
    {
        if (axes.y > 0 && axes.x == 0)
            PlayerDir = Direction.Up;
        if (axes.y < 0 && axes.x == 0)
            PlayerDir = Direction.Down;
        if (axes.y == 0 && axes.x < 0)
            PlayerDir = Direction.Left;
        if (axes.y == 0 && axes.x > 0)
            PlayerDir = Direction.Right;

        //Movement Prioritization
        if (axes.y > 0 && axes.x > 0)
        {
            PlayerDir = Direction.Right;
            if (pfd)
                PlayerDir = Direction.Up;
        }
        if (axes.y > 0 && axes.x < 0)
        {
            PlayerDir = Direction.Left;
            if (pfd)
                PlayerDir = Direction.Up;
        }
        if (axes.y < 0 && axes.x < 0)
        {
            PlayerDir = Direction.Left;
            if (pfu)
                PlayerDir = Direction.Down;
        }
        if (axes.y < 0 && axes.x > 0)
        {
            PlayerDir = Direction.Right;
            if (pfu)
                PlayerDir = Direction.Down;
        }
        //If player does not input, change nothing.
    }

    //physics-based controls
    // Detect player input and apply respective movement
    // makes use of unity input
    void MakeMovement()
    {
        if (CheckIsAlive())
        {
            //Important to get raw values of axes for more precise movement
            axes = DetermineProperMovement();
            //If movement is made, move the player and determine the direction they are facing...
            if (!axes.Equals(Vector2.zero))
            {
                setRandomIdle = false;
                moving = true;
                pAnim.SetBool("moving", moving);
                idleTimer = 0;
                Vector2 movForce = axes * speed;
                playerRB.AddForce(movForce);
                DetermineDirection();
                pAnim.SetInteger("PlayerDirection", (int)PlayerDir);
            }
            else
            {
                moving = false;
                if (!setRandomIdle && idle)
                {
                    pAnim.SetInteger("randIdle", Random.Range(0, 3));
                    setRandomIdle = true;
                }
                pAnim.SetBool("moving", moving);
            }
        }
    }
    //Because we get raw input, values are not smoothed nor processed. 
    //To get the proper unit vector, we divide the input (at 1) by 1.4144 to get .707
    private Vector2 DetermineProperMovement()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) >= 1 && Mathf.Abs(Input.GetAxisRaw("Vertical")) >= 1)
            return new Vector2(Input.GetAxisRaw("Horizontal") / 1.4144f, Input.GetAxisRaw("Vertical") / 1.4144f);
        else
            return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    bool CheckIdle()
    {
        if (idleTimer >= idleLimit)
            return true;
        else
            return false;
    }

    bool CheckIsAlive()
    {
        return playerState.IsAlive();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Pushable"))
        {
            CheckPushingDirection(col);
            pushing = true;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Pushable"))
        {
            pfu = pfd = false;
            pushing = false;
        }
    }
    //Better directional pushing animations
    private void CheckPushingDirection(Collision2D col)
    {
        float myY = transform.position.y;
        float otherUpY = col.collider.bounds.center.y + col.collider.bounds.extents.y;
        float otherDownY = col.collider.bounds.center.y - col.collider.bounds.extents.y;
        if(myY > otherUpY)
            pfu = true;
        else if(myY < otherDownY)
            pfd = true;
        
    }
    public bool GetMoving(){    return moving;  }
    public bool GetPushing(){   return pushing;  }
    //Non-physics controls
    /*void MakeMovement()
    {
        float horizontalAxis = Input.GetAxis("Horizontal") * speed / 10;
        float verticalAxis = Input.GetAxis("Vertical") * speed / 10;

        if (horizontalAxis != 0 || verticalAxis != 0)
        {
            Vector2 movement = new Vector2(horizontalAxis, verticalAxis);

            playerRB.MovePosition(playerRB.position + movement);
        }
    }*/
}
