using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class QueueSystem : MonoBehaviour
{
    /// This script handles the repairQueue, adding spawnedNPCs,
    /// Items and data into the repairQueue.

    /// The script also sort the repairUrgency from highest to
    /// lowest.

    // pulling external scripts ==============================

    public SpawnManager spawnManager;
    public DayNightScript dayNightScript;

    // info trackng ==========================================

    public List<EntityData> repairQueue = new List<EntityData>();

    // boolean check =========================================

    private bool hasCheckedDayStart = false;

    // ====================================================================================

    public void Update()
    {
        if (!hasCheckedDayStart)
        {
            if (dayNightScript.dayCycle == DayNightScript.states.dayStart)
            {
                float timeOfDay = dayNightScript.TimeOfDay;
                if (timeOfDay >= 9f && timeOfDay < 19f)
                {
                    //Debug.Log("DayStart detected.");
                    hasCheckedDayStart = true;
                }
                else
                {
                    //Debug.LogWarning("Not in the specified time range.");
                }
            }
        }
        //repairQueue.RemoveAt(0);
        //repairQueue.Insert();
        if (spawnManager.allowSpawning)
        {
            UpdateRepairQueue();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            UpdateRepairQueue(); // for testing
        }
    }

    public void UpdateRepairQueue()
    {
        List<EntityData> allEntityData = spawnManager.entityDataList; // get relevant data from spawnManager

        List<EntityData> newEntities = allEntityData.Except(repairQueue).ToList();

        newEntities.Sort((a, b) => b.repairUrgency.CompareTo(a.repairUrgency)); // sort based on repair urgency, highest to lowest

        repairQueue.AddRange(newEntities);

        //Debug.LogWarning($"UpdateRepairQueue called. Current repairQueue count: {repairQueue.Count}");

        foreach (var entity in newEntities)
        {
            //Debug.LogWarning($"RepairQueueLog - NewEntity: {entity.npcName}, Repair Urgency: {entity.repairUrgency}");
        }
        /// FOR ANU: QueueSystem sorted here, debugLog will spit out Entity + RepairUrgency in descending order
        /// FOR KATHI: Pull this DebugLog's info out for the sorted RepairQueue UI list
    }

    public List<EntityData> GetRepairQueue()
    {
        return repairQueue;
    }
}
