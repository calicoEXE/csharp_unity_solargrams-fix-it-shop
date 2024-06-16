using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 5f;

    private Rigidbody rb;
    public Animator animator;
    private Vector3 lastMovementDirection = Vector3.forward; // Default forward direction
    public DayNightScript refToDayNightScript;
    public WorkshopManager refToWorkShopManager;

    //public GameObject WorkPanel;


    

    public float speed = 120f; // Speed variable
    public Vector3 movement; // Set the variable 'movement' as a Vector3 (x,y,z)
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        refToDayNightScript = FindObjectOfType<DayNightScript>();
        refToWorkShopManager = FindObjectOfType<WorkshopManager>();
    }

    void Update()
    {
        if(refToDayNightScript.dayCycle != DayNightScript.states.nightSummary)
        {
            if(refToWorkShopManager.workshop == WorkshopManager.state.working)
            {

            }
            else
            {
                if (DialogueManager.GetInstance().dialogueisPlaying)
                {
                    return;
                } 
            }   
        } 
        if(refToDayNightScript.dayCycle == DayNightScript.states.nightSummary)
        {
            float ySleepValue = 0.628f;//0.628421f;
            this.transform.position = new Vector3(-7.25f, ySleepValue, 5.5f);//resets player to bed
            this.transform.eulerAngles = new Vector3(0, 85, 0);
        }
    }
    void FixedUpdate()
    {
        if (refToDayNightScript.dayCycle != DayNightScript.states.nightSummary)
        {
            if (refToWorkShopManager.workshop == WorkshopManager.state.working)
            {

            }
            else
            {
                

                PlayerMovement();
                moveCharacter(movement); // We call the function 'moveCharacter' in FixedUpdate for Physics movement

                
            }
        }
        
    }



    // 'moveCharacter' Function for moving the game object
    void moveCharacter(Vector3 direction)
    {
        // We multiply the 'speed' variable to the Rigidbody's velocity...
        // and also multiply 'Time.fixedDeltaTime' to keep the movement consistant on all devices
        rb.velocity = direction * speed * Time.fixedDeltaTime;
    }
    void PlayerMovement()
    {
        
        //// Get input from the horizontal and vertical axes (WASD or arrow keys)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //// Calculate the movement direction based on the input
        movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized;
      
        //new code
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        //


       // transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
        ////changes rotation to the player direction of movement
        if (movement != Vector3.zero)
        {
            transform.forward = movement;
            animator.SetBool("Walking", true);
        }
        else
        {

            animator.SetBool("Walking", false);
        }
    }
}
