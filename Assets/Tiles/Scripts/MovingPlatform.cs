using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{

    public GameObject platform;         // The platform in the game world that will be moving
    public float moveSpeed;             // The speed of the platform's movement
    private Transform targetPoint;      // The point that the platform is currently moving to
    public Transform[] points;          // All the points the platform will move through
    public int currPoint;          // What point in the array are we looking at

    // Use this for initialization
    void Start()
    {
        // Set the first point that we'll be moving towards
        targetPoint = points[currPoint];
    }

    // Update is called once per frame
    void Update()
    {
        // Moving the platform from its position to the targetPoint at moveSpeed.
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, targetPoint.position, Time.deltaTime * moveSpeed);

        // Switch to the next targetPoint if we've reached one
        if (platform.transform.position == targetPoint.position)
        {
            currPoint++;

            // If we've reached the endPoint, continue forward to the first point.
            if (currPoint == points.Length)
                currPoint = 0;

            targetPoint = points[currPoint];
        }
    }
}

