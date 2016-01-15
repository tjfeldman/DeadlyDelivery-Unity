using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerPhysics))]
public class PlayerController : MonoBehaviour {

    private PlayerPhysics playerPhysics;    // our PlayerPhysics variable
    private bool jump;                      // check if the player has jumped

	// Use this for initialization
	void Start () {
        playerPhysics = GetComponent<PlayerPhysics>();
	}
	

    void Update ()
    {
        // set jump in update so we don't miss the inpu
        if(!jump)
        {
            jump = Input.GetButtonDown("Jump");
        }
    }

	void FixedUpdate () {
        float hDir = Input.GetAxisRaw("Horizontal");    // get's input for the horizontal direction
        playerPhysics.Move(hDir, jump);                 // move the player
        jump = false;                                   // reset jump
	}
}
