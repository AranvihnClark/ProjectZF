using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] private float playerSpeed = 5f;

    private float moveX;
    private float moveZ;
    private bool onGround;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (onGround)
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveZ = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector3(moveX * playerSpeed, rb.velocity.y, moveZ * playerSpeed);
        }
    }
}
