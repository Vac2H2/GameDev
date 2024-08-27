using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public Transform targetObj;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Check if the Animator component exists
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
    }

    void Update()
    {
        float speed = 3;
        if (Vector3.Distance(targetObj.position, this.transform.position) < 2.5f)
        {
            animator.SetTrigger("Fire");
            speed = 2;
        }
        transform.position = Vector3.MoveTowards(this.transform.position, targetObj.position, speed * Time.deltaTime);
        transform.LookAt(targetObj);
    }
}
