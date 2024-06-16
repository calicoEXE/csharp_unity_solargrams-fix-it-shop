using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NPC : MonoBehaviour
{
    public float displayCompetency;

    //walking
    bool shouldStartWalking;
    bool shouldWalkAway;

    public GameObject goNPC; //this gameobject
    public GameObject associatedItem;

    public Transform pointA;  // Point A spawning
    public Transform pointB;  // Point B kiosk
    public Transform pointC;  // Point B endpoint
    float moveSpeed = 3.5f;


    float distanceToNPCAndPoint;
    public enum state { getsCalledRepair, idle, walksAway};
    public state repairState;
    public bool repairDone;

    public enum pstate { pointASpawn, pointBKisok, pointCEnd};
    public pstate pointStates;

    public DialogueTrigger refToDiaTrig;
    public WorkshopManager refToWorkshopManagerScript;
    public DialogueManager refToDiaManager;
    public DialogueManager dialogueManagerScript;
    public DialogueVariables dialogueVariables;
    public NarrativeManagerScript refToNarrManager;
    public GameManager refToGM;
    public AudioManagerScript refToAudioManager;
    public bool interactionComplete;

    public void UpdateCompetency(float competency)
    {
        displayCompetency = competency;
    }
    private void Start()
    {
        refToGM = FindObjectOfType<GameManager>();
        repairDone = false;
        pointA = GameObject.Find("NPCSpawnPoint").transform;
        pointB = GameObject.Find("KioskPoint").transform;
        pointC = GameObject.Find("EndPoint").transform;
        refToDiaTrig = this.GetComponent<DialogueTrigger>();
        refToDiaManager = FindObjectOfType<DialogueManager>();
        refToAudioManager = FindObjectOfType<AudioManagerScript>();
        refToWorkshopManagerScript = FindObjectOfType<WorkshopManager>();
        refToNarrManager = FindObjectOfType<NarrativeManagerScript>();
        shouldStartWalking = true;// starts the walking when npc is made active
        pointStates = pstate.pointASpawn;

    }
    private void Update()
    {
        distanceToNPCAndPoint = Vector3.Distance(transform.position, pointB.position);
        //decided on the states of the npc
        if (pointStates == pstate.pointASpawn)
        {
            repairState = state.getsCalledRepair;
        }
        //else if(this.transform.position == pointB.position)
        //else if(distanceToNPCAndPoint <= 0.1f)
        else if(pointStates == pstate.pointBKisok)
        {
            
            #region main workshop gameloop
            //below are the states need and connect to the workshop main gameloops
            if (refToDiaTrig.playerInteract == true)//checks whether player is infron of Kiosk and whether the player is interacting with it so the NPC can give the item
            {
                if (refToWorkshopManagerScript.workshop != WorkshopManager.state.givingItemBack)
                {
                    refToAudioManager.npcArrivedBellSE();
                    refToWorkshopManagerScript.workshop = WorkshopManager.state.takingItem;
                }
                else if(refToWorkshopManagerScript.workshop == WorkshopManager.state.givingItemBack)
                {
                    //triggers dialogue depending on whether a good or bad repair was done, check with repairmechanicsmanagerscript
                    refToWorkshopManagerScript.workshop = WorkshopManager.state.itemReturned;
                    shouldWalkAway = true;
                    repairDone = true;
                    repairState = state.idle;
                }
                
            }
            #endregion
        }
        
        if (repairDone == true)//checks whether repair has been completed
        {
            if (refToDiaManager.dialogueisPlaying == false)//check whether player has finished the interaction
            {
                StartCoroutine(WalkFromDestination()); // starts that the play is walking away
                Debug.Log("NPCGO");
                repairState = state.walksAway;
                if (refToGM.oSCall == GameManager.overallState.storyPlay)
                {
                    interactionComplete = true;
                    refToNarrManager.npcInteraction = interactionComplete;  
                }
                repairDone = false;
                interactionComplete = false;
            }
        } 
        //triggers the walks towards the kiosk
        if (shouldStartWalking == true)
        {
            StartCoroutine(WalkToDestination());//start the couritine function to make the NPC walk
            shouldStartWalking = false; // Make sure it doesn't start the Coroutine repeatedly 
        }
        if (pointStates == pstate.pointCEnd)
        {
            StopCoroutine(WalkFromDestination());//stops the coroutine since the player reached the desination
        }
    }

    IEnumerator WalkToDestination()
    {
        // Calculate the distance between point A and point B
        float distance = Vector3.Distance(transform.position, pointB.position);
        while (distance > 0.1f)
        {
            // While the NPC hasn't reached point B yet
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, moveSpeed * Time.deltaTime);
            // Move the NPC towards point B
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, moveSpeed * Time.deltaTime);

            // Update the distance
            distance = Vector3.Distance(transform.position, pointB.position);
            // Check if the NPC has reached point B
            if (distance <= 0.1f)
            {
                pointStates = pstate.pointBKisok;
                break; // Exit the loop
            }
            // Yield for one frame
            yield return null;
        }
        // NPC has reached point B
    }

    
    IEnumerator WalkFromDestination()
    {
        // Calculate the distance between point B and point C
        float distance = Vector3.Distance(transform.position, pointC.position);
        while (distance > 0.1f)
        {
            // Move the NPC towards point C
            transform.position = Vector3.MoveTowards(transform.position, pointC.position, moveSpeed * Time.deltaTime);
            // Update the distance
            distance = Vector3.Distance(transform.position, pointC.position);
            // Yield for one frame
            if (distance <= 0.1f)
            {
                pointStates = pstate.pointCEnd;
                break; // Exit the loop
            }
            yield return null;
        }
        
    }

    public void SetAssociatedItem(GameObject item)
    {
        associatedItem = item;
    }
}
