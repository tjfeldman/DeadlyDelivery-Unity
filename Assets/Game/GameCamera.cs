using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {
    private Transform target;
    private const float k_trackSpeed = 20;  // how fast the camera moves

	public void SetTarget(Transform t)
    {
        target = t;
    }

    // update late to calculate for input
    void LateUpdate()
    {
        // update the camera's position
        if(target)
        {
            float x = IncrementTowards(transform.position.x, target.position.x, k_trackSpeed);
            float y = IncrementTowards(transform.position.y, target.position.y, k_trackSpeed);

            transform.position = new Vector3(x, y, transform.position.z);
        }
    }

    // accelerate to the speed we want
    private float IncrementTowards(float current, float target, float accel)
    {
        if (current == target)
            return current;
        else
        {
            float dir = Mathf.Sign(target - current);
            current += accel * Time.deltaTime * dir;
            return (dir == Mathf.Sign(target - current)) ? current : target;
        }
    }
}
