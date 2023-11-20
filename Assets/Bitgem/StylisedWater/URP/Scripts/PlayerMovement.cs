using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : MonoBehaviour
{
    // [SerializeField] Rigidbody rb;
    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private float rotationTime = 0.1f;
    float turnSmoothVelocity;

    // Basic player variables.
    private bool onGround = true;
    public static bool canMove = true;
    
    // To grab the player's input.
    private float xInput;
    private float zInput;
    
    // To create camera 'normalized' vectors.
    private UnityEngine.Vector3 forward;
    private UnityEngine.Vector3 right;

    // To store our direction-relative input.
    private UnityEngine.Vector3 relativeForwardInput;
    private UnityEngine.Vector3 relativeRightInput;

    // To create/confirm camera-relative movement.
    private UnityEngine.Vector3 cameraRelativePlayerMovement;

    private void Start()
    {
        // rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (canMove)
        {
            if (onGround)
            {
                MoveRelativeToCamera();
            }
            // rb.velocity = new UnityEngine.Vector3(0, rb.velocity.y, 0);
        }
    }

    private void MoveRelativeToCamera()
    {
        // Grabbing our player's input
        xInput = Input.GetAxis("Vertical");
        zInput = Input.GetAxis("Horizontal");

        // Below we normalize our character's movements based on the camera's direction
        forward = Camera.main.transform.forward;
        right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        // We then 'create' direction-relative vectors as 'inputs'
            relativeForwardInput = xInput * forward * (Time.deltaTime * playerSpeed);
            relativeRightInput = zInput * right * (Time.deltaTime * playerSpeed);

        // Then we apply the camera relative movements
        cameraRelativePlayerMovement = relativeForwardInput + relativeRightInput;
        
        // Character rotation.
        float targetAngle = (Mathf.Atan2(cameraRelativePlayerMovement.x, cameraRelativePlayerMovement.z) * Mathf.Rad2Deg) - 90f;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationTime);
        if (cameraRelativePlayerMovement.magnitude >= 0.01f)
        {
            transform.rotation = UnityEngine.Quaternion.Euler(0f, angle, 0f);
        }

        this.transform.Translate(cameraRelativePlayerMovement, Space.World);
    }
/*
    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnCollisionExit(Collision collision)
    {

    }
*/
}
