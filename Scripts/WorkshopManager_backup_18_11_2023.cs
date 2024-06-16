using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkshopManager_backup_18_11_2023 : MonoBehaviour
{
    // Will Manage the Workshop and its states to give mroe structure to the dialogue


    public enum state {working, takingItem, givingItemBack, sleeping, idle, playerHasItem, itemReturned};
    public state workshop;

    public InteractionTriggerClass refToInteTrigRepairScript;
    public InteractionTriggerClass refToInteTrigPCScript;
    public InteractionTriggerClass refToInteTrigRefurbishScript;
    public QuestUIList refToQuestUIScript;
    public RepairMechanicManagerClass refToRepairMechanicManagerScript;

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
    }

    void Update()
    {
        //// sets the Workshop state to working if any of the big mechanics are activeated aka Repair, refurbish, pc
        //if (refToInteTrigPCScript.playerInteractionActiveCheck == true || refToInteTrigRefurbishScript.playerInteractionActiveCheck == true || refToInteTrigRepairScript.playerInteractionActiveCheck == true)
        //{
        //    workshop = state.working;
        //}
        //else
        //{
        //    workshop = state.idle;
        //}

        if (workshop == state.working)
        {

        }
        if(workshop == state.playerHasItem)
        {
            refToQuestUIScript.goItem.SetActive(true);
            refToRepairMechanicManagerScript.repairMechanicState = RepairMechanicManagerClass.state.repairInProgress;
        }
        else if (workshop == state.takingItem)//Gets triggered in NPC script
        {
            workshop = state.playerHasItem;
        }
        else if (workshop == state.givingItemBack)//should be changed through RepairButtonScript on the RepairCamera
        {
            refToQuestUIScript.goItem.SetActive(false);
        }
        else if (workshop == state.itemReturned)//will be triggered in NPC script
        {
            refToQuestUIScript.repairState = QuestUIList.repairStates.choose;
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
        if(workshop == state.givingItemBack)//checks whether the player finished a repair and needs to return the item to the npc, if yes do not change state
        {

        }
        else
        {
            workshop = newState;
        }
       
  
    }
}
