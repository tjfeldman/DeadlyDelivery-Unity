using UnityEngine;
using System.Collections;

public class PlayerPhysics : MonoBehaviour {

    public LayerMask collisionMask;

    // used for movement
    private float deltaX;   // how far do we have to go in the x direction
    private float deltaY;   // how far do we have to go in the y direction
   
    // used in collisions
    private BoxCollider collider;   // the box collider we have
    private Vector2 p;              // our current position
    private Vector3 s;              // the size of our collider
    private Vector3 c;              // the center of our collider

    // used if we have scaled the collider
    private Vector3 originalSize;   // holds the original size of the collider
    private Vector3 originalCenter; // holds the original center of the collider
    private float colliderScale;    // how much have we scaled by

    // how many rays should we cast
    private const int k_collisionsDivisionX = 3;    // number of rays cast for top/bottom collisions
    private const int k_collisionsDivisionY = 3;    // number of rays cast for left/right collisions

    private float grace = .005f; // Tiny bit of space between a ray and a possible collision

    [HideInInspector]
    public bool isGrounded = false;
    [HideInInspector]
    public bool movementStopped = false;

    /*
    // NOTE #1: doesn't work for vertical. need to do some automatic correction
    // used to correct movement if standing on a horizontal moving platform
    private Transform platform;                             // the platform we are standing on
    private Vector3 platformPosPrev;                        // the platform's previous position
    private Vector3 deltaPlatformPos;                       // the change in distance between the platform's positions
    */

    // used for all collisions
    Ray ray;
    RaycastHit hit;

    void Start()
    {
        collider = GetComponent<BoxCollider>();
        colliderScale = transform.localScale.x; // assumes the scale for the collider is equal in all dimensions
        originalSize = collider.size;
        originalCenter = collider.center;
        SetCollider(originalSize, originalCenter);
    }

    // handles movement
    // @param moveAmount - a Vector2 for how much to move from input and gravity
	public void Move(Vector2 moveAmount)
    {
        deltaX = moveAmount.x;
        deltaY = moveAmount.y;
        
        /*
        // SEE NOTE #1
        // update how far the platform has moved if standing on one
        if (platform)
            deltaPlatformPos = platform.position - platformPosPrev;
        else
            deltaPlatformPos = Vector3.zero;
        */

        collisions();

        /*
        // SEE NOTE #1
        // adjust movement for platforms
        if (platform)
        {
            deltaX += deltaPlatformPos.x;
        }
        */

        // holds the total movement that has occurred.
        Vector2 totalMovement = new Vector2(deltaX, deltaY);

        // move the character after all calculations are done
        transform.Translate(totalMovement);
    }

    // handles collisions
    public void collisions()
    {
        p = transform.position;

        isGrounded = false; // reset isGrounded
        // Check collisions above and below
        for (int i = 0; i < k_collisionsDivisionX; i++)
        {
            // what direction are we checking for collisions
            float dirY = Mathf.Sign(deltaY);
            // split apart the rays staring from the left of the box to the right of the box
            float x = (p.x + c.x - s.x / 2) + s.x / (k_collisionsDivisionX - 1) * i;
            float y = p.y + c.y + s.y / 2 * dirY; // top or bottom edge of collider

            ray = new Ray(new Vector2(x, y), new Vector2(0, dirY));
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
            // did we generate a hit
            if (Physics.Raycast(ray, out hit, Mathf.Abs(deltaY), collisionMask))
            {
                // assign the moving platform that we're on if we're above one
                if (hit.collider.tag == "MovingPlatform" && dirY == -1)
                {
                    // SEE NOTE #1
                    //platform = hit.transform;
                    //platformPosPrev = platform.position;
                    transform.parent = hit.transform;
                }
                else
                    transform.parent = null;

                // the distance between the raycast and the hit
                float dst = Vector3.Distance(ray.origin, hit.point);

                // Stop vertical movement after coming within a grace distance with the collider
                if (dst > grace)
                    deltaY = dst * dirY - grace * dirY;
                else
                    deltaY = 0;

                isGrounded = true;  // we are on the ground now
                break;
            }
            // if there's no collision, we're not on a platform
            else
                // SEE NOTE #1
                // platform = null;
                transform.parent = null;

        }

        movementStopped = false;    // reset movementStopped
        // Check collisions left and right
        for (int i = 0; i < k_collisionsDivisionY; i++)
        {
            // what direction are we checking for collisions
            float dirX = Mathf.Sign(deltaX);
            float x = p.x + c.x + s.x / 2 * dirX; // left or right edge of collider
            // split apart the rays staring from the top of the box to the bottom of the box
            float y = p.y + c.y - s.y / 2 + s.y / (k_collisionsDivisionY - 1) * i;

            ray = new Ray(new Vector2(x, y), new Vector2(dirX, 0));
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
            // did we generate a hit
            if (Physics.Raycast(ray, out hit, Mathf.Abs(deltaX), collisionMask))
            {
                // the distance between the raycast and the hit
                float dst = Vector3.Distance(ray.origin, hit.point);

                // Stop horizontal movement after coming within a grace distance with the collider
                if (dst > grace)
                    deltaX = dst * dirX - grace * dirX;
                else
                    deltaX = 0;

                movementStopped = true; // we stopped moving because we hit something
                break;
            }
        }

        // Check collisions diagonally (to land on edges)
        if (!isGrounded && !movementStopped)
        {
            Vector3 playerDir = new Vector3(deltaX, deltaY);    // what direction are we going at the moment
            // find the corner that we're using
            Vector3 o = new Vector3(p.x + c.x + s.x / 2 * Mathf.Sign(deltaX), p.y + c.y + s.y / 2 * Mathf.Sign(deltaY));
            Debug.DrawRay(o, playerDir.normalized, Color.yellow);

            // normalize because we're using a direction
            ray = new Ray(o, playerDir.normalized);
            // use pythagorean theorem to find the distance of the ray
            if (Physics.Raycast(ray, Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY), collisionMask))
            {
                isGrounded = true;
                deltaY = 0;
            }
        }
    }

    // sets up our collider
    public void SetCollider(Vector3 size, Vector3 center)
    {
        collider.size = size;
        collider.center = center;

        s = size * colliderScale;
        c = center * colliderScale;
    }
}
