using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Grabs the player's position.
    [SerializeField] Transform playerTransform;    
    

    // Update is called once per frame
    private void Update()
    {
        // Update's the camera's position to mirror the player's location.
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
    }
}
