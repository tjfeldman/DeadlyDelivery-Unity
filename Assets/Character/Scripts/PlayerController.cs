using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerPhysics))]
public class PlayerController : MonoBehaviour {

    // Player Movement
    public float gravity = 9.81f;
    public float maxSpeed = .3f;
    public float acceleration = 1;
    public float jumpHeight = 7;

    // used for input movement
    private float currentSpeed;
    private float targetSpeed;
    private Vector2 amountToMove;

    // holds the direction of gravity
    [HideInInspector]
    public Vector2 gravityDir; // (-1 = left || 0 = none || 1 = right, -1 = down || 0 = none || 1 = up)

    private PlayerPhysics playerPhysics; 

	// Use this for initialization
	void Start () {
        gravityDir = new Vector2(0, -1);  // set default gravity to down with no horizontal gravity
        playerPhysics = GetComponent<PlayerPhysics>();
	}
	

    void Update ()
    {
        targetSpeed = Input.GetAxisRaw("Horizontal") * maxSpeed;

        if (playerPhysics.isGrounded)
        {
            // remove gravity so that we can jump
            if (gravityDir.x != 0)
                amountToMove.x = 0;
            if (gravityDir.y != 0)
                amountToMove.y = 0;

            // Jump
            if (Input.GetButtonDown("Jump"))
            {
                if (gravityDir.x != 0)
                    amountToMove.x = jumpHeight * -gravityDir.x;
                if (gravityDir.y != 0)
                    amountToMove.y = jumpHeight * -gravityDir.y;
            }
        }
    }
	// Update is called once per frame
	void FixedUpdate () {
        // if we collide with an object horizontally, reset movement
        if(playerPhysics.movementStopped)
        {
            targetSpeed = 0;
            currentSpeed = 0;
        }

        // Input
        
        currentSpeed = IncrementTowards(currentSpeed, targetSpeed, acceleration);

        

        amountToMove.x = currentSpeed;  // apply movement in x-direction
        ApplyGravity();
        playerPhysics.Move(amountToMove);
	}

    void ApplyGravity()
    {
        amountToMove.x += gravity * gravityDir.x * Time.deltaTime;
        amountToMove.y += gravity * gravityDir.y * Time.deltaTime;
    }

    // accelerate to the speed we want
    private float IncrementTowards(float current, float target, float accel)
    {
        // if we're moving at the target speed, we've achieved the speed we wanted
        if (current == target)
            return current;
        else
        {
            // what direction to go to get closer to the target
            float dir = Mathf.Sign(target - current);
            current += accel * Time.deltaTime * dir;
            // if we haven't reached out target speed, return out current speed
            // but if we've gone to far, return the target speed
            return (dir == Mathf.Sign(target - current)) ? current : target;
        }
    }
}
