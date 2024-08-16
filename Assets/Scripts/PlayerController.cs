using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float movementSpeed = 5.0f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovementInput();
        HandleRotationInput();
        HandleShootInput();
        UpdateAnimation();
    }

    void HandleMovementInput()
    {
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");

        Vector3 _movement = new Vector3(_horizontal, 0, _vertical).normalized;
        transform.Translate(_movement * movementSpeed * Time.deltaTime, Space.World);
    }

    void HandleRotationInput()
    {
        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit))
        {
            transform.LookAt(new Vector3(_hit.point.x, transform.position.y, _hit.point.z));
        }
    }

    void HandleShootInput ()
    {
        if (Input.GetButton("Fire1"))
        {
            //Shoot
            animator.SetTrigger("shoot");
            // PlayerGun.Instance.Shoot();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            // Reset the shooting trigger to stop the shooting animation
            animator.ResetTrigger("shoot");
        }
    }

    void UpdateAnimation()
    {
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");

        // Calculate the movement direction relative to the player's forward direction
        Vector3 movement = new Vector3(_horizontal, 0, _vertical).normalized;
        Vector3 localMovement = transform.InverseTransformDirection(movement);

        // Update Animator parameters
        Debug.Log(localMovement.x);
        animator.SetFloat("MoveX", localMovement.x);
        animator.SetFloat("MoveZ", localMovement.z);
    }
}
