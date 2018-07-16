using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D playerRB;
    [SerializeField]
    private bool findPlayerRB = true;
    [SerializeField]
    private float speed = 1;
    void Start ()
    {
        if(findPlayerRB)
            playerRB = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate ()
    {
        MakeMovement();
        //Remove acceleration from physics equation ;D
        playerRB.velocity = new Vector2(0, 0); //comment out this line for non-physics movement
	}

    //physics-based controls
    // Detect player input and apply respective movement
    // makes use of unity input
    void MakeMovement()
    {
        //Important to get raw values of axes for more precise movement
        Vector2 axes = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 movForce = axes * speed;
        playerRB.AddForce(movForce);
    }

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
