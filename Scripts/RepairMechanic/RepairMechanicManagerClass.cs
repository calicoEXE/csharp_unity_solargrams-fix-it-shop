using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;
using UnityEngine.UIElements;

public class RepairMechanicManagerClass : MonoBehaviour
{
    public enum state { repairInProgress, noRepair };
    public state repairMechanicState;
    public int successfulRepairs;
    public int failedRepairs;
    public int incompleteRepairs;
    public QuestUIList refToQuestUILIst;
    public GameObject currentRepairItem;
    public bool itemRepaired; //= true;
    public bool successfulRepairBool;
    public bool failedRepairBool;
    public bool incompleteRepairBool;
    public int itemRepairedCounter;
    public GameManager refToGameManager;

    void Start()
    {
        //refToQuestUILIst = FindObjectOfType<QuestUIList>();
        repairMechanicState = state.noRepair;
        //itemRepairedCounter = -1;
    }

    void Update()
    {
        //Debug.Log(repairMechanicState);
        if(repairMechanicState == state.noRepair)
        {
            if (itemRepaired == false)
            {
                //RepairFail();
                itemRepaired = true; //need to be true because otherwise it would add before a repair could be accepted to if there is no repair and the itemrepair is false (decide in carrierscript of repaireditem) then the repair is failed

                //itemRepairedCounter++;

                //Debug.Log($"Item Repaired Counter: {itemRepairedCounter}");  // Log the counter value

                //Debug.Log($"Successful Repairs: {successfulRepairs}");
                //Debug.Log($"Failed Repairs: {failedRepairs}");
                int maxIncompleteRepairs;
                if (refToGameManager.oSCall == overallState.onboarding)
                {
                    maxIncompleteRepairs = 0;
                }
                else
                {
                    maxIncompleteRepairs = 5;
                    incompleteRepairs = Mathf.Max(0, maxIncompleteRepairs - (failedRepairs + successfulRepairs));
                    //Debug.Log($"Incomplete Repairs: {incompleteRepairs}");
                }
            }
        }
        else if(repairMechanicState == state.repairInProgress)
        {
            
            currentRepairItem = refToQuestUILIst.goItem;

        }

    }

    public void RepairSuccess() // gets called in the carrierclass of the the acceptec repairItem when the player has successfully repaired everything in the item
    {
        successfulRepairs++;
        successfulRepairBool = false;

        //refToQuestUILIst.processedSkippedUrgentRepairs = false;

        if (successfulRepairBool)
        {
            
        }
    }

    public void RepairFail()
    {
        failedRepairs++;
        failedRepairBool = false;

        //refToQuestUILIst.processedSkippedUrgentRepairs = false;

        if (failedRepairBool)
        {
            
        }
    }


    public void RepairIncomplete()
    {
        if(incompleteRepairBool)
        {
            incompleteRepairs++;
            incompleteRepairBool = false;
        }
    }
}
