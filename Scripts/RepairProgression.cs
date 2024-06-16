/*using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RepairProgression : MonoBehaviour

    /// This script checks on the progression of all repairs 
    /// in the repairQueue.

    /// This script pulls into DaySummary for final evaluation.
{
    // pulling external scripts ==============================

    public CarrierElementClass carrierElementClass;
    public QuestUIList questUIList;
    public QueueSystem queueSystem;
    public EntityData entityData;
    public SpawnManager spawnManager;

    // =======================================================

    private int missedUrgentNPC = 0;
    
    /// dictionary track times for each NPC
    private Dictionary<int, int> missedUrgentNPCTimes = new Dictionary<int, int>();

    private void Update()
    {
        CheckMissedUrgentNPCs();
    }

    private void CheckMissedUrgentNPCs()
    {
        if (missedUrgentNPC >= 3)
        {
            var urgentMissedNPCs = new List<int>();

            foreach (var npc in missedUrgentNPCTimes)
            {
                if (npc.Value >= 3)
                {
                    EntityData repairData = GetRepairByID(npc.Key);
                    if (repairData != null)
                    {
                        if (repairData.repairUrgency > 0.8f)
                        {
                            urgentMissedNPCs.Add(npc.Key);
                        }
                    }
                }

                foreach (var urgentNPC in urgentMissedNPCs)
                {
                    Debug.Log($"Missed urgent NPC: {urgentNPC} - Missed {missedUrgentNPCTimes[urgentNPC]} times.");
                }
            }
        }
    }

    private EntityData GetRepairByID(int uniqueID)
    {
        var repairQueue = queueSystem.GetRepairQueue();
        foreach (var repair in repairQueue)
        {
            if (repair.uniqueID == uniqueID)
            {
                return repair;
            }
        }
        return null;
    }

    public void CompletedRepair(EntityData repair)
    {
        if (!questUIList)
        {
            if (questUIList.repairState == QuestUIList.repairStates.accepted)
            {
                if (carrierElementClass.repaired)
                {
                    repair.isRepairCompleted = true;
                }
            }
            else if (questUIList.repairState == QuestUIList.repairStates.choose)
            {
                RepairPending(repair);
            }
        }
    }

    public void RepairPending(EntityData repair)
    {
        /// check how many repairs left in repairQ
        
        if (!questUIList)
        {
            List<EntityData> repairQueue = queueSystem.GetRepairQueue();

            if (repairQueue.Count == 0)
            {
                repair.repairList = true;
            }
            else
            {
                repair.repairList = false;

                /// Checking if repairUrgency is higher than 0.8 and specific NPC is not selected
                
                if (repair.repairUrgency > 0.8f)
                {
                    if (!IsNPCSelected(repair))
                    {
                        missedUrgentNPC++;

                        if (missedUrgentNPCTimes.ContainsKey(repair.uniqueID))
                        {
                            missedUrgentNPCTimes[repair.uniqueID]++;
                        }
                        else
                        {
                            missedUrgentNPCTimes.Add(repair.uniqueID, 1);
                        }

                        Debug.LogError($"{repair.npcName} with repair urgency {repair.repairUrgency} not selected. Count: {missedUrgentNPC}. Times missed: {missedUrgentNPCTimes[repair.uniqueID]}");
                    }
                }
            }
        }
    }

    private bool IsNPCSelected(EntityData repair)
    {
        return false;
    }

}*/
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RepairProgression : MonoBehaviour
{
    public CarrierElementClass carrierElementClass;
    public QuestUIList questUIList;
    public QueueSystem queueSystem;
    public EntityData selectedNPC;
    public SpawnManager spawnManager;

    public bool processedSkippedUrgentRepairs = false;

    private int missedUrgentNPC = 0;
    private Dictionary<int, int> missedUrgentNPCTimes = new Dictionary<int, int>();

    private void Update()
    {
        //CheckMissedUrgentNPCs();
    }

    //private void CheckMissedUrgentNPCs()
    //{
    //    foreach (var npc in missedUrgentNPCTimes)
    //    {
    //        if (npc.Value >= 1)  // Check if NPC was missed once
    //        {
    //            EntityData repairData = GetRepairByID(npc.Key);
    //            if (repairData != null && repairData.repairUrgency > 0.8f)
    //            {
    //                Debug.Log($"Missed urgent NPC: {npc.Key} - Missed 1 time.");
    //            }
    //        }
    //    }
    //}

    public void SkippedNPCs(List<int> skippedNPCs)
    {
        foreach (var npcID in skippedNPCs)
        {
            //if (!processedSkippedUrgentRepairs)
            //{

                if (missedUrgentNPCTimes.ContainsKey(npcID))
                {
                    missedUrgentNPCTimes[npcID] += 1;
                    Debug.Log($"Missed urgent NPC: {npcID} ({missedUrgentNPCTimes[npcID]} times)");
                }
                else
                {
                    missedUrgentNPCTimes.Add(npcID, 1);
                    Debug.Log($"Missed urgent NPC: {npcID} (1 time)");
                }

                //processedSkippedUrgentRepairs = true;

            //}

        }
    }

    private EntityData GetRepairByID(int uniqueID)
    {
        var repairQueue = queueSystem.GetRepairQueue();
        foreach (var repair in repairQueue)
        {
            if (repair.uniqueID == uniqueID)
            {
                return repair;
            }
        }
        return null;
    }

    public void CompletedRepair(EntityData repair)
    {
        if (!questUIList)
        {
            if (questUIList.repairState == QuestUIList.repairStates.accepted)
            {
                if (carrierElementClass.repaired)
                {
                    repair.isRepairCompleted = true;

                }
            }
            else if (questUIList.repairState == QuestUIList.repairStates.choose)
            {
                RepairPending(repair);
            }
        }
    }

    public void RepairPending(EntityData repair)
    {
        if (!questUIList)
        {
            List<EntityData> repairQueue = queueSystem.GetRepairQueue();

            if (repairQueue.Count == 0)
            {
                repair.repairList = true;
            }
            else
            {
                repair.repairList = false;

                if (repair.repairUrgency > 0.8f)
                {
                    if (!IsNPCSelected(repair))
                    {
                        if (!missedUrgentNPCTimes.ContainsKey(repair.uniqueID))
                        {
                            missedUrgentNPCTimes.Add(repair.uniqueID, 1);
                            missedUrgentNPC++;
                            Debug.LogError($"{repair.npcName} with repair urgency {repair.repairUrgency} not selected. Count: {missedUrgentNPC}. Times missed: {missedUrgentNPCTimes[repair.uniqueID]}");
                        }
                    }
                }
            }
        }
    }


    private bool IsNPCSelected(EntityData repair)
    {
        return repair == selectedNPC;
    }

    public void SetSelectedNPC(EntityData selectedNPCData)
    {
        selectedNPC = selectedNPCData;
    }
}
