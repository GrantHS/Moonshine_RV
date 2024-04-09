using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float speed = 5.0f; 
    public float rotationSpeed = 100.0f; 
    public float boundaryRadius = 200.0f; 
    public float directionChangeInterval = 5.0f; 
    public float turnSmoothTime = 2.0f; 
    public float minPitchAngle = -30.0f; 
    public float maxPitchAngle = 30.0f; 

    private Vector3 startPosition; 
    private Vector3 targetDirection; 
    private float directionChangeTimer; 

    void Start()
    {
        startPosition = transform.position; // store the start position
        pickRandomDirection(); // set an initial random direction
        directionChangeTimer = directionChangeInterval; // reset timer
    }

    void Update()
    {
        // decrease the timer and change direction
        directionChangeTimer -= Time.deltaTime;
        if (directionChangeTimer <= 0)
        {
            pickRandomDirection(); // change to a new random direction
            directionChangeTimer = directionChangeInterval; // reset timer
        }

        movePlane(); // move the plane
        smoothRotatePlane(); // rotate the plane
        checkBoundary(); // keep the plane within the boundary
    }

    void pickRandomDirection()
    {
        // get a new random direction within pitch limits
        targetDirection = Random.insideUnitSphere.normalized;
        targetDirection.y = Mathf.Clamp(targetDirection.y, Mathf.Sin(minPitchAngle * Mathf.Deg2Rad), Mathf.Sin(maxPitchAngle * Mathf.Deg2Rad));
    }

    void movePlane()
    {
        // move the plane forward in the current direction
        transform.position += targetDirection * speed * Time.deltaTime;
    }

    void smoothRotatePlane()
    {
        // calculate the plane's rotation based on current direction of where it's going/moving
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        targetRotation = clampRotationAroundXAxis(targetRotation); // clamp x-axis limits

        // smoothly interpolate to the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSmoothTime * Time.deltaTime);
    }

    void checkBoundary()
    {
        // if the plane is outside the boundary, reset to edge of boundary
        if ((transform.position - startPosition).magnitude > boundaryRadius)
        {
            transform.position = startPosition + (transform.position - startPosition).normalized * boundaryRadius;
            pickRandomDirection(); // and pick a new direction if it's outside the boundary
        }
    }

    Quaternion clampRotationAroundXAxis(Quaternion q)
    {
        // clamp rotation around the x-axis to remain within pitch angle limits
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
        angleX = Mathf.Clamp(angleX, minPitchAngle, maxPitchAngle);
        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }
}
