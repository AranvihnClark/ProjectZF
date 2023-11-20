using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

// This script will be mainly, if not only, used for combat.
public class CameraController : MonoBehaviour
{
    // Grabs the player's position.
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float rotationSpeed;
    private bool isTurning;
    private int turnDirection;
    private float previousRotationAngle;
    private float changeInRotation;
    private float totalRotationCompleted;

    private void Start()
    {
        isTurning = false;
        
        // Used to dictate layers that should be culled.
        float[] distanceLayers = new float[32];
        distanceLayers[3] = 40;
        distanceLayers[7] = 40;
        GetComponent<Camera>().layerCullDistances = distanceLayers;

        // Possibly removing the SerializeField above and setting it to the below as a static.
        // For now, until I'm done testing, this will be here as a comment.
        // rotationSpeed = 300f;
        previousRotationAngle = transform.eulerAngles.y;
        changeInRotation = 0;
        totalRotationCompleted = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isTurning)
        {
            changeInRotation = 0;
            totalRotationCompleted = 0;
            if (Input.GetKeyDown(KeyCode.E))
            {
                isTurning = true;
                PlayerMovement.canMove = false;
                turnDirection = 1;
            } else if (Input.GetKeyDown(KeyCode.Q))
            {
                isTurning = true;
                PlayerMovement.canMove = false;
                turnDirection = -1;
            }
        }

        if (isTurning)
        {
            CheckRotation();
        }

        // Update's the camera's position to mirror the player's location.
        transform.position = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
    }

    private void CheckRotation()
    {
        changeInRotation = turnDirection * rotationSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + changeInRotation, transform.eulerAngles.z);

        totalRotationCompleted += changeInRotation;
        if (Mathf.Abs(totalRotationCompleted) >= 90)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, previousRotationAngle + (turnDirection * 90), transform.eulerAngles.z);
            isTurning = false;
            PlayerMovement.canMove = true;
            previousRotationAngle = transform.eulerAngles.y;
        }

    }
}
