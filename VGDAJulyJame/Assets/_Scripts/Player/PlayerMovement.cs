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
        playerRB = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
        MakeMovement();
	}

    // Detect player input and apply respective movement
    // makes use of unity input
    void MakeMovement()
    {
        float horizontalAxis = Input.GetAxis("Horizontal") * speed / 10;
        float verticalAxis = Input.GetAxis("Vertical") * speed / 10;

        if (horizontalAxis != 0 || verticalAxis != 0)
        {
            Vector2 movement = new Vector2(horizontalAxis, verticalAxis);

            playerRB.MovePosition(playerRB.position + movement);
        }
    }
}
