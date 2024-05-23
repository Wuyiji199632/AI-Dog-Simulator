using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    //public Transform playerBody;
    public float speed = 12f;
    private Rigidbody rb;
    private float xRotation = 0f,yRotation=0f;
    public GameObject caressingHand;

    public DogFSM dogFSM;

    public List<GameObject> itemsPickedUp = new List<GameObject>();

    public string currentItemName=string.Empty;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        rb = GetComponent<Rigidbody>();
        InvokeRepeating(nameof(DetectObjectToPickup), 0.1f, 0.1f);
        InvokeRepeating(nameof(SelectPickupItems), 0.1f, 0.1f);
    }

    private void Update()
    {
        ToggleCaressingHand();DetectObjectToPickup(); SelectPickupItems();
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

    private void ToggleCaressingHand()
    {
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 1.5f))
        {
            if(hit.collider.gameObject.tag == "Dog")
            {
                caressingHand.SetActive(true);
                caressingHand.transform.position = hit.point;
                caressingHand.transform.rotation = Quaternion.LookRotation(hit.normal);
                dogFSM = hit.collider.gameObject.GetComponent<DogFSM>();
                DogFSM hitDogFSM = hit.collider.gameObject.GetComponent<DogFSM>();
                if (dogFSM != hitDogFSM || !(dogFSM.currentState is GreetState))
                {
                    dogFSM = hitDogFSM;
                    dogFSM.ChangeState(new GreetState(dogFSM));
                    
                }
                Debug.Log("Caressing dog!");
            }
            else
            {
                caressingHand.SetActive(false);
                Debug.Log("stop caressing dog!");
                if (!(dogFSM.currentState is IdleState))
                {
                    dogFSM.ChangeState(new IdleState(dogFSM));
                }
            }
        }
        else
        {
            caressingHand.SetActive(false);
            if (!(dogFSM.currentState is IdleState))
            {
                dogFSM.ChangeState(new IdleState(dogFSM));
            }
        }
    }

    private void DetectObjectToPickup()
    {
        bool isDetecting = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 5f);
       
            
        if (isDetecting)
        {

            bool detected = hit.collider.gameObject.GetComponent<PickupItem>() != null;
           
           

            if(detected && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Picked up " + hit.collider.gameObject.GetComponent<PickupItem>().itemName);
                hit.collider.gameObject.SetActive(false);                
                hit.collider.gameObject.transform.SetParent(this.transform);
                hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                hit.collider.gameObject.transform.localPosition = new Vector3(0, 0, 1);
                hit.collider.gameObject.GetComponent<PickupItem>().alreadyPickedUp = true;
                itemsPickedUp.Add(hit.collider.gameObject);

                UpdateItemIcons();

            }
            bool showReminder = detected && !hit.collider.gameObject.GetComponent<PickupItem>().alreadyPickedUp;
            WorldManager.instance.pickupReminderText.gameObject.SetActive(showReminder);
            WorldManager.instance.pickupReminderText.text = showReminder ? "Press E to pick up " + hit.collider.gameObject.GetComponent<PickupItem>().itemName : "";

        }
      
    }

    private void UpdateItemIcons()
    {
        foreach (var itemIcon in WorldManager.instance.iconsForPickUp)
        {
            foreach (var item in itemsPickedUp)
            {
                //itemsPickedUp[itemsPickedUp.Count - 1].gameObject.SetActive(true);
                if (itemIcon.name == item.name)
                {
                    itemIcon.color = new Color(1, 1, 1, 1); // Fully illuminate the icon
                }
            }
        }
    }

    private void SelectPickupItems()
    {

        Dictionary<KeyCode, string> keyItemMap = new Dictionary<KeyCode, string>
    {
        { KeyCode.Alpha1, "Bone" },
        { KeyCode.Alpha2, "Baseball" },
        { KeyCode.Alpha3, "Food" },
        { KeyCode.Alpha4, "Water" }
    };

        foreach (var item in itemsPickedUp)
        {
            if (item.GetComponent<PickupItem>().alreadyPickedUp)
            {
                foreach (var entry in keyItemMap)
                {
                    if (Input.GetKeyDown(entry.Key))
                    {
                        item.gameObject.SetActive(item.name == entry.Value && !item.gameObject.activeSelf);
                        currentItemName = item.name;
                    }
                }
            }
        }

    }

    private void UseItem()
    {
        switch (currentItemName)
        {
            case "Bone":
                Debug.Log("Throwing bone");
                break;
                case "Baseball":
                Debug.Log("Throwing baseball");
                break;
                case "Food":
                Debug.Log("Feeding dog");
                break;
                case "Water":
                Debug.Log("Giving water to dog");
                break;

        }
    }
}
