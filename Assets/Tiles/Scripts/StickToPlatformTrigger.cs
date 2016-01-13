using UnityEngine;
using System.Collections;

public class StickToPlatformTrigger : MonoBehaviour
{ 
    void OnTriggerEnter2D(Collider2D col)
    {
        // If a Player walks on the platform, make the player a child of the platform
        if (col.tag == "Player")
            col.transform.parent = transform.parent;
    }


    void OnTriggerExit2D(Collider2D col)
    {
        // If a Player exits the platform, unparent the platform from it
        if (col.tag == "Player")
           col.transform.parent = null;
    }
}