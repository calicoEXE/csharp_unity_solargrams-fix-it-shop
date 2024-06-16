using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkshopManager : MonoBehaviour
{
    // Will Manage the Workshop and its states to give mroe structure to the dialogue


    public enum state { working, takingItem, givingItemBack, sleeping, idle, playerHasItem, itemReturned };
    public state workshop;

    public InteractionTriggerClass refToInteTrigRepairScript;
    public InteractionTriggerClass refToInteTrigPCScript;
    public InteractionTriggerClass refToInteTrigRefurbishScript;
    public QuestUIList refToQuestUIScript;
    public RepairMechanicManagerClass refToRepairMechanicManagerScript;
    public NarrativeManagerScript refToNarrativeManager;
    public GameManager refToGM;

    //GameObjects===============================================================================
    public GameObject refToRepairItem;
    //===============================================================================

    //===============================================================================

    //===============================================================================

    void Start()
    {
        workshop = state.idle;
        refToInteTrigRepairScript = GameObject.Find("WorkBenchParent").GetComponent<InteractionTriggerClass>();
        refToInteTrigPCScript = GameObject.Find("PC_Repair_Parent").GetComponent<InteractionTriggerClass>();
        refToInteTrigRefurbishScript = GameObject.Find("RefurbishingParent").GetComponent<InteractionTriggerClass>();
        refToGM = FindObjectOfType<GameManager>();
        refToNarrativeManager = FindObjectOfType<NarrativeManagerScript>();
    }

    void Update()
    {
        //Debug.Log(workshop);
        if (workshop == state.working)
        {

        }
        if (workshop == state.takingItem)//Gets triggered in NPC script
        {
            workshop = state.playerHasItem;
        }
        else if (workshop == state.playerHasItem)
        {
            if (refToGM.oSCall == GameManager.overallState.freePlay)
            {
                 refToQuestUIScript.goItem.SetActive(true);
            }
            else if(refToGM.oSCall == GameManager.overallState.storyPlay)
            {
                Debug.Log("StoryPlay and item active");
                refToQuestUIScript.coreItems.SetActive(true);
            }
            refToRepairMechanicManagerScript.repairMechanicState = RepairMechanicManagerClass.state.repairInProgress;
        }
        
        else if (workshop == state.givingItemBack) //should be changed through RepairButtonScript on the RepairCamera
        {
            
            if (refToGM.oSCall == GameManager.overallState.freePlay)
            {
                refToQuestUIScript.goItem.SetActive(false);
                //DialogueManager.GetInstance().UpdateRepairDoneVariable("repair_done", "true");
            }
            else if (refToGM.oSCall == GameManager.overallState.storyPlay)
            {
                refToQuestUIScript.coreItems.SetActive(false);
                
            }  
        }
        else if (workshop == state.itemReturned)//will be triggered in NPC script
        {
            AudioManagerScript audioManagerScript = FindObjectOfType<AudioManagerScript>();
            audioManagerScript.onItemBackToNPCSE();
            refToQuestUIScript.repairState = QuestUIList.repairStates.choose;//only works for oSState.freeplay; storyyplay switch from accepted to choose happens in narrativemanager where story to free get switched
            workshop = state.idle;
        }
        else if (workshop == state.sleeping)
        {

        }
        else if (workshop == state.idle)
        {

        }
    }
    public void WorkingStateChange(state newState)// connected to INteractionTriggerClass to change the state to work
    {
        
        if (workshop == state.givingItemBack)//checks whether the player finished a repair and needs to return the item to the npc, if yes do not change state
        {

        }
        else
        {
            //Debug.Log("newState");
            workshop = newState;
        }

    }

    public void SetWorkshopItem(GameObject item)
    {
        refToRepairItem = item;
    }
}
