using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using Ink.Runtime;

public class QuestUIList : MonoBehaviour
{

    public DayNightScript refToDayNightScript;

    public QueueSystem refToQueueSystem;
    public SpawnManager refToSpawnManager;
    public DialogueManager refToDiaManager;
    public RepairProgression repairProgression;
    public GameManager refToGM;
    public NarrativeManagerScript refToNarrativeManager;
    public WorkshopManager workshopManager;

    /// <summary>
    /// refToQueueSystem.repairQueue and UI Slots indexes will always match
    /// </summary>

    public ClickInteractionScript[] refToUiSlots;
    public TMP_Text[] titleText;
    public TMP_Text[] descText;
    public RawImage[] reftoVisualIcon;
    public TMP_Text[] diffText;

    //alert popup
    public TMP_Text alertText;
    public TMP_Text alertDescText;
    public TMP_Text alertDiffText;
    public RawImage alertVisualIcon;
    public GameObject refToAlertGO;

    public enum repairStates { accepted, choose, npcActivated };
    public repairStates repairState;

    public List<int> acceptedQuestIDList = new List<int>();

    public int acceptedQuestID;
    public int acceptedNPCID;
    public int accptedItemID;

    //public Transform pointA;  // Point A GameObject
    //public Transform pointB;  // Point B GameObject
    //public float moveSpeed;

    //walking
    //bool shouldStartWalking;

    public bool processedSkippedUrgentRepairs = false;

    //freeplay
    public GameObject goNPC;
    public GameObject goItem;
    //narrative
    public GameObject coreNPCs;
    public GameObject coreItems;

    private List<int> skippedUrgentRepairs = new List<int>();

    private void Start()
    {
        workshopManager = FindObjectOfType<WorkshopManager>();
        refToGM = FindObjectOfType<GameManager>();
        refToDiaManager = FindObjectOfType<DialogueManager>();
        refToNarrativeManager = FindObjectOfType<NarrativeManagerScript>();
        //gives each UiSlot a unique ID based on their index value in refToUiSlots Lists
        for (int i = 0; i < refToUiSlots.Length; i++)
        {
            refToUiSlots[i].uiID = i;
        }

        //conect ClickInteractionScript connect to the 
        repairState = repairStates.choose;
    }

    private void Update()
    {
        /// STORY MODE
        if (refToGM.oSCall == GameManager.overallState.storyPlay)
        {
            if (repairState == repairStates.choose)
            {
                //lines below gets the one core npc which is supposed to spawn and allows the prefab to display the information
                //coreNPCs = refToNarrativeManager.coreNPCsDictionary[refToNarrativeManager.narrIndex]; // obtain coreNPCDictionary and check accept
                coreNPCs = refToNarrativeManager.coreNPCs[refToNarrativeManager.narrIndex]; // obtain coreNPCDictionary and check accept
                coreItems = refToNarrativeManager.coreItems[refToNarrativeManager.narrIndex];

                narrPlayUpdate();
                //bool alertRequest = false;
                //CLICK INTERACTION
                if (refToAlertGO.GetComponent<ClickInteractionScript>().acceptedRequest == true) //get the script from only the popup
                {
                    Debug.Log("alerted NPC");
                    //alertRequest = true;
                    refToAlertGO.SetActive(false);
                    repairState = repairStates.npcActivated;//changes the state                  
                }
            }
            else if (repairState == repairStates.npcActivated)
            {
                descText[acceptedQuestID].text = "Customer Incoming";
                //coreNPCs = refToNarrativeManager.coreNPCs[refToNarrativeManager.narrIndex]; // obtain coreNPCDictionary and check accept
                coreNPCs = refToNarrativeManager.coreNPCsDictionary[refToNarrativeManager.narrIndex];///////////////////////////////////////////////////Sky HERE SEE ME PLEASE
                //coreItems = refToNarrativeManager.coreItems[refToNarrativeManager.narrIndex];//still needs to be changed
                coreItems = refToNarrativeManager.coreItemDictionary[refToNarrativeManager.narrIndex];//////////////////////////////////////////////////SKY HERE
                //coreItems.SetActive(true);
                coreNPCs.SetActive(true);

                repairState = repairStates.accepted;


            }
            else if (repairState == repairStates.accepted)
            {
                
                //changes desc text to gove the player an update on the repair progress
                if(workshopManager.workshop == WorkshopManager.state.takingItem)
                {
                    descText[acceptedQuestID].text = "Repair in Progress";
                }
                if(workshopManager.workshop == WorkshopManager.state.itemReturned)
                {
                    descText[acceptedQuestID].text = "Repair Completed";
                }
            }
        }
        /// FREEPLAY MODE


        else if (refToGM.oSCall == GameManager.overallState.freePlay)
        {           
            if (repairState == repairStates.choose)
            {
                processedSkippedUrgentRepairs = false;
                Debug.Log("Accepted.");
                int i = 0;
                List<int> skippedUrgentRepairs = new List<int>();
                bool hasStartedNewQuest = false;

                foreach (var intClick in refToUiSlots) //check whether any slot is accepted aka if the player has accepted any requests
                {
                    if (intClick.acceptedRequest == true)
                    {
                        if (acceptedQuestIDList.Contains(intClick.uiID)) //checks whether this quest has been accpted before
                        {

                        }
                        else //if quest has not been accepted then go and trigger next state
                        {
                            acceptedQuestID = intClick.uiID; // gets the ID
                            goItem = refToSpawnManager.spawnedItems[acceptedQuestID];//give sthe variable to goItem so it can be called and setactive in the WorkshopManager
                            acceptedQuestIDList.Add(acceptedQuestID);
                            repairState = repairStates.npcActivated;//changes the state
                            hasStartedNewQuest = true;
                        }

                    }
                    else if (i < refToQueueSystem.repairQueue.Count && refToQueueSystem.repairQueue[i].repairUrgency >= 0.8f)
                    //else if (refToQueueSystem.repairQueue[i].repairUrgency >= 0.8f)
                    {
                        if (acceptedQuestIDList.Contains(intClick.uiID)) //checks whether this quest has been accpted before
                        {

                        }
                        else
                        {
                            skippedUrgentRepairs.Add(refToQueueSystem.repairQueue[i].uniqueID);
                        }
                    }
                    i++;
                }

                if (hasStartedNewQuest && skippedUrgentRepairs.Count > 0)
                {
                    if (!processedSkippedUrgentRepairs)
                    {
                        processedSkippedUrgentRepairs = true;

                        Debug.Log("Skipped number of urgent repairs: " + skippedUrgentRepairs.Count);
                        repairProgression.SkippedNPCs(skippedUrgentRepairs);
                    }
                }
            }
            else if (repairState == repairStates.npcActivated)
            {
                skippedUrgentRepairs.Clear();

                //gets the uniqueID from the NPC itself and the Item(origins: items->spawnmanager)
                if (acceptedNPCID >= 0 && acceptedQuestID < refToQueueSystem.repairQueue.Count)
                // old script: without if check
                {
                    acceptedNPCID = refToQueueSystem.repairQueue[acceptedQuestID].uniqueID;
                }
                acceptedNPCID = refToQueueSystem.repairQueue[acceptedQuestID].uniqueID;
                //find the items if from the dictionary for the npc and the items          
                goNPC = refToSpawnManager.spawnedNPCsDictionary[acceptedNPCID];
                //sets NPC and Items are set active 
                goNPC.SetActive(true);

                repairState = repairStates.accepted;  

                LogSelectedNPC();

            }
            else if (repairState == repairStates.accepted)
            {
                //put couritine here to make it work
                //shouldStartWalking = true;         
            }

        }
    }
    public void UpdateUIData() //here all of the info is inputed
    {
        ////index of text and NPC will always match
        ////add info for UI here
        for (int i = 0; i < refToQueueSystem.repairQueue.Count; i++)
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
            
            if (refToQueueSystem.repairQueue[i].repairUrgency > 0.8f)
            {
                descText[i].text = "Urgent!";
            }
        }

    }

    public void SelectedNPC(EntityData selectedNPCData)
    {
        repairProgression.SetSelectedNPC(selectedNPCData);
    }

    public void freePlayUpdate() //gets called in GM
    {
        //sets alert popup as inactive
        refToAlertGO.SetActive(false);
        foreach (ClickInteractionScript var in refToUiSlots) //check whether the script are in active and set them active
        {
            if (var.enabled == false)
            {
                var.enabled = true;
            }

        }
        UpdateUIData();
        acceptedQuestIDList.Clear();//clears list of previously accpedt Repair requests
    }
    public void narrPlayUpdate() //gets called at the top of storyPlay
    {
        refToAlertGO.SetActive(true);
        //alertDescText.text = ;
        alertText.text = coreNPCs.name.ToString();
        alertDiffText.text = "EMERGENCY";
        foreach (ClickInteractionScript var in refToUiSlots) //check whether the script are in active and set them active
        {
            var.enabled = false;
        }
    }

    public void LogSelectedNPC()
    {
        foreach (var repair in refToQueueSystem.GetRepairQueue())
        {

            Debug.Log($"NPC {refToQueueSystem.repairQueue[acceptedQuestID].npcName} (ID: {acceptedNPCID}) has been selected.");
        }
    }

    //public void LogUnselectedNPCs() ///no longer using
    //{
    //    foreach (var repair in refToQueueSystem.GetRepairQueue())
    //    {
    //        if (repairState != repairStates.npcActivated && !acceptedQuestIDList.Contains(repair.uniqueID))
    //        {
    //            Debug.LogWarning($"NPC {repair.npcName} (ID: {repair.uniqueID}) was not selected.");
    //        }
    //    }

    //}
}
