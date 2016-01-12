using UnityEngine;
using System.Collections;


namespace Tiles
{
    public class MovingPlatform : MonoBehaviour
    {

        public float xSpeed;             // The speed at which the platform will move on the x-axis
        private float xUseSpeed;         // The speed to use in the update for horizontal movement
        float xOrig;                     // The original x-value that the platform will start at
        public float xDistance;          // Half the distance the platform will travel on the x-axis
        public bool xStartRight;         // Which direction should the platform start, right or left
        int xDirection;                  // Modify the starting direction (-1 or 1 / left or right)
        public float ySpeed;             // The speed at which the platform will move on the y-axis
        private float yUseSpeed;         // The speed to use in the update for vertical movement
        float yOrig;                     // The original y-value that the platform will start at
        public float yDistance;          // Half distance the platform will travel on the y-axis
        public bool yStartUp;            // Which direction should the platform start, up or down
        int yDirection;                  // Modify the starting direction (-1 or 1 / down or up)

        // Use this for initialization
        void Start()
        {
            xDirection = (xStartRight) ? 1 : -1;
            yDirection = (yStartUp) ? 1 : -1;
            xUseSpeed = xSpeed * xDirection;
            yUseSpeed = ySpeed * yDirection;
            xOrig = transform.position.x;
            yOrig = transform.position.y;
            
        }

        // Update is called once per frame
        void Update()
        {
            if (xOrig - transform.position.x < -xDistance)
            {
                xUseSpeed = -xSpeed; // flip direction and go left
            }
            else if (xOrig - transform.position.x > xDistance)
            {
                xUseSpeed = xSpeed; // flip direction and go right
            }

            if (yOrig - transform.position.y < -yDistance)
            {
                yUseSpeed = -ySpeed; // flip direction and go up
            }
            else if (yOrig - transform.position.y > yDistance)
            {
                yUseSpeed = ySpeed; // flip direction and go down
            }

            transform.Translate(xUseSpeed * Time.deltaTime, yUseSpeed * Time.deltaTime, 0);
        }
    }
}
