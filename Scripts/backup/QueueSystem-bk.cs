/*using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QueueSystem : MonoBehaviour
{
    // pull info from spawnManager

    // organise the repair urgency from highest to lowest order

    // add list into visual UI\
    public SpawnManager spawnManager;

    private List<EntityData> repairQueue = new List<EntityData>();
    private bool dayStart = true;

    public void Update()
    {
        if (dayStart)
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
    }

    public List<EntityData> GetRepairQueue()
    {
        return repairQueue;
    }
}
*/