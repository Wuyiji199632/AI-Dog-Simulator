using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    //public Transform playerBody;
    public float speed = 12f;
    private Rigidbody rb;
    private float xRotation = 0f,yRotation=0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        rb = GetComponent<Rigidbody>();
    }

    
    private void FixedUpdate()
    {
        if(WorldManager.instance.paused) return; // If the game is paused, don't update the player's movement
        UpdateMouseRotationMovement(); UpdateTranslationMovement();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag==("Dog"))
        {
            Debug.Log("Touch Dog");
        }
    }

    private void UpdateMouseRotationMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation += mouseX;
        //xRotation= Mathf.Clamp(xRotation, -360f, 360f);


        yRotation += mouseY;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);


        // Applying both pitch and yaw rotation in one operation to the camera
        Quaternion cameraRotation = Quaternion.Euler(-yRotation, xRotation, 0f);
        Camera.main.transform.localRotation = cameraRotation;
       // transform.localRotation = Quaternion.Euler(0, xRotation, 0);
    }

    private void UpdateTranslationMovement()
    {
       

        float horizontalMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float verticalMovement = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        // Moving the camera based on the current rotation
        Vector3 movement = Camera.main.transform.right * horizontalMovement + Camera.main.transform.forward * verticalMovement;
        rb.velocity = movement;
        transform.Translate(rb.velocity, Space.World);
    }
}
