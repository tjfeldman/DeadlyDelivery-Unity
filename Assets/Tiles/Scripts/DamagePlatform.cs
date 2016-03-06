using UnityEngine;
using System.Collections;

public class DamagePlatform : MonoBehaviour {

    private const int k_collisionsDivisionX = 10;
    private BoxCollider2D collider;
    private Vector2 p;              // our current position
    private Vector2 s;              // the size of our collider
    private Vector2 o;              // the center of our collider
    private const float k_distCheck = .2f;

    // used if we have scaled the collider
    private Vector2 originalSize;   // holds the original size of the collider
    private Vector2 originalOffset; // holds the original offset of the collider
    private Vector2 colliderScale;    // how much have we scaled by

	// Use this for initialization
	void Start () {
        collider = GetComponent<BoxCollider2D>();
        p = transform.position;
        colliderScale = transform.localScale;
        originalSize = collider.size;
        originalOffset = collider.offset;
        SetCollider(originalSize, originalOffset);
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < k_collisionsDivisionX; i++)
        {
            // split apart the rays staring from the left of the box to the right of the box
            float x = (p.x + o.x - s.x / 2) + s.x / (k_collisionsDivisionX - 1) * i;
            float y = p.y + o.y + s.y / 2; // top edge of collider

            Vector2 rayOrigin = new Vector2(x, y);  // set the ray's origin point

            // THE FOLLOWING IS USED FOR DEBUGGING
            Ray2D ray = new Ray2D(rayOrigin, new Vector2(0, 1));
            Debug.DrawRay(ray.origin, ray.direction, Color.cyan);

            // set up our RaycastHit2d
            //@params: the origin we set up earlier
            //         the direction determined by our calculation earlier
            //         only check as far as how far the player would move
            //         the collisionLayerMask
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, new Vector2(0, 1), k_distCheck, LayerMask.NameToLayer("Player"));
            //did we generate a hit
            if (hit.fraction > 0)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<PlayerHealth>().takeDamage(10);
            }
        }
	
	}

    // sets up our collider
    public void SetCollider(Vector2 size, Vector2 offset)
    {
        collider.size = size;
        collider.offset = offset;

        // scale the values used to set up raycasting
        s = Vector2.Scale(size, colliderScale) + new Vector2(PlayerPhysics.skin, PlayerPhysics.skin);
        o = Vector2.Scale(offset, colliderScale);
    }
}
