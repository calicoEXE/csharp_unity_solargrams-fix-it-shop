using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using Ink.Runtime;

public class QuestUIList_backup_17_11_2023 : MonoBehaviour
{
    
    public DayNightScript refToDayNightScript;

    public QueueSystem refToQueueSystem;
    public SpawnManager refToSpawnManager;
    public DialogueManager refToDiaManager;
    public RepairProgression repairProgression;
    public GameManager refToGM;
    /// <summary>
    /// refToQueueSystem.repairQueue and UI Slots indexes will always match
    /// </summary>

    public ClickInteractionScript[] refToUiSlots;
    public TMP_Text[] titleText;
    public TMP_Text[] descText;
    public RawImage[] reftoVisualIcon;
    public TMP_Text[] diffText;

    public enum repairStates { accepted, choose, npcActivated};
    public repairStates repairState;

    public List <int> acceptedQuestIDList = new List<int> ();

    public int acceptedQuestID;
    public int acceptedNPCID;
    public int accptedItemID;

    public Transform pointA;  // Point A GameObject
    public Transform pointB;  // Point B GameObject
    public float moveSpeed;

    //walking
    bool shouldStartWalking;

    public GameObject goNPC;
    public GameObject goItem;
    private void Start()
    {
        refToGM = FindObjectOfType<GameManager>();
        refToDiaManager = FindObjectOfType<DialogueManager>();
        //gives each UiSlot a unique ID based on their index value in refToUiSlots Lists
        for (int i = 0; i< refToUiSlots.Length; i++)
        {
            refToUiSlots[i].uiID = i;
        }
        
        //conect ClickInteractionScript connect to the 
        repairState = repairStates.choose;
    }

    private void Update()
    {

  //     //triggers the walks
  //     if (shouldStartWalking)
  //     {
  //         StartCoroutine(WalkToDestination());//start the couritine function to make the NPC walk
  //         shouldStartWalking = false; // Make sure it doesn't start the Coroutine repeatedly
  //     }

        if(refToGM.oSCall == GameManager.overallState.freePlay)
        {

        }
        if (repairState == repairStates.npcActivated)
        {
            //gets the uniqueID from the NPC itself and the Item (origins: items -> spawnmanager)
            if (acceptedNPCID >= 0 && acceptedQuestID < refToQueueSystem.repairQueue.Count)
                // old script: without if check
            {
                acceptedNPCID = refToQueueSystem.repairQueue[acceptedQuestID].uniqueID;
            }
            //find the items if from the dictionary for the npc and the items          
            goNPC = refToSpawnManager.spawnedNPCsDictionary[acceptedNPCID];
            //sets NPC and Items are set active 
            goNPC.SetActive(true);
            repairState = repairStates.accepted;
        }
        else if(repairState == repairStates.accepted)
        {
            //put couritine here to make it work
            //shouldStartWalking = true;         
        }
        else if(repairState == repairStates.choose)
        {
            if(refToDayNightScript.dayCycle == DayNightScript.states.dayStart)//daystarts UI gets updated
            {
                UpdateUIData();
                acceptedQuestIDList.Clear();//clears list of previously accpedt Repair requests
            }
            int i = 0;
            List<int> skippedUrgentRepairs = new List<int>();
            bool hasAcceptedRequest = false;
            foreach (var intClick in refToUiSlots) //check whether any slot is accepted aka if the player has accepted any requests
            {
                        
                if (intClick.acceptedRequest == true)
                {
                    hasAcceptedRequest = true;
                    acceptedQuestID = intClick.uiID; // gets the ID
                    goItem = refToSpawnManager.spawnedItems[acceptedQuestID];//give sthe variable to goItem so it can be called and setactive in the WorkshopManager
                    if (acceptedQuestIDList.Contains(acceptedQuestID)) //checks whether this quest has been accpted before
                    {

                    }
                    else //if quest has not been accepted then go and trigger next state
                    {
                        acceptedQuestIDList.Add(acceptedQuestID);
                        repairState = repairStates.npcActivated;//changes the state
                    }
                    
                }
                else if (i < refToQueueSystem.repairQueue.Count && refToQueueSystem.repairQueue[i].repairUrgency >= 0.8f)
                //else if (refToQueueSystem.repairQueue[i].repairUrgency >= 0.8f)
                {
                    skippedUrgentRepairs.Add(refToQueueSystem.repairQueue[i].uniqueID);
                }

                i++;
            }

            if (hasAcceptedRequest && skippedUrgentRepairs.Count > 0)
            {
                Debug.Log("Skipped number of urgent repairs: " + skippedUrgentRepairs.Count);
                repairProgression.SkippedNPCs(skippedUrgentRepairs);
            }
        }     
    }
    private void UpdateUIData() //here all of the info is inputed
    {
        ////index of text and NPC will always match
        ////add info for UI here
        for(int i = 0; i < refToQueueSystem.repairQueue.Count; i++)
        {
            titleText[i].text = refToQueueSystem.repairQueue[i].npcName;
            // titleText[i].text = refToDiaManager.tagValue.ToString();
            //displays challenge of the item repair from he spawnmanager
            if (refToSpawnManager.spawnedItems[i].GetComponent<Item>().itemDifficulty == Item.difficultyState.lvl1)
            {
                diffText[i].text = "easy";
            }     
            else if (refToSpawnManager.spawnedItems[i].GetComponent<Item>().itemDifficulty == Item.difficultyState.lvl2)
            {
                diffText[i].text = "hard";
            }
            //descText[i].text = 
        }
        
    }

    public void SelectedNPC(EntityData selectedNPCData)
    {
        repairProgression.SetSelectedNPC(selectedNPCData);
    }

    //  IEnumerator WalkToDestination()
    //  {
    //      // Calculate the distance between point A and point B
    //      float distance = Vector3.Distance(goNPC.transform.position, pointB.position);
    //      
    //      while (distance > 0.1f)
    //      {
    //          // While the NPC hasn't reached point B yet
    //          goNPC.transform.position = Vector3.MoveTowards(goNPC.transform.position, pointB.position, moveSpeed * Time.deltaTime);
    //          // Move the NPC towards point B
    //          goNPC.transform.position = Vector3.MoveTowards(goNPC.transform.position, pointB.position, moveSpeed * Time.deltaTime);
    //
    //          // Update the distance
    //          distance = Vector3.Distance(goNPC.transform.position, pointB.position);
    //
    //          // Yield for one frame
    //          yield return null;
    //      }
    //      // NPC has reached point B
    //      Debug.Log("NPC reached point B");
    //  }

}
