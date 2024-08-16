using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;       // The player or object the camera should follow
    [SerializeField]
    public Vector3 targetOffset;   // The fixed offset from the target
    public Vector3 lookOffset;
    [SerializeField]
    private float movementSpeed = 5f; // The speed at which the camera follows the target (if smoothing is desired)

    void LateUpdate()
    {
        FollowTarget();
    }
    
    void FollowTarget()
    {
        // Directly set the camera's position to the target's position plus the offset
        transform.position = target.position + targetOffset;

        // Optionally, if you want to add a smoothing effect, uncomment the following line
        // transform.position = Vector3.Lerp(transform.position, target.position + targetOffset, movementSpeed * Time.deltaTime);

        // Make the camera look at the target (optional, if you want the camera to always face the target)
        Vector3 lookTarget = target.position + lookOffset;
        transform.LookAt(lookTarget);
    }
}
