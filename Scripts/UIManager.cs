using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    public TextMeshProUGUI currentEntityDataText;
    public TextMeshProUGUI existingEntityDataText;
    public TextMeshProUGUI repairQueueText;
    public QueueSystem queueSystem;

    public void Update()
    {
        UpdateCurrentEntityDataUI();
        UpdateExistingEntityDataUI();
        //UpdateRepairQueueUI();
    }

    public void UpdateCurrentEntityDataUI()
    {
        currentEntityDataText.text = "Current Data: \n";

        for (int i = 0; i < spawnManager.currentlySpawnedEntities.Count; i++)
        {
            EntityData entityData = spawnManager.currentlySpawnedEntities[i];
            currentEntityDataText.text += $"Name: {entityData.npcName}, Tool: {entityData.selectedItem}\n";
        }
    }

    public void UpdateExistingEntityDataUI()
    {
        existingEntityDataText.text = "Existing Data: \n";

        for (int i = 0; i < spawnManager.existingSpawnedEntities.Count; i++)
        {
            EntityData entityData = spawnManager.existingSpawnedEntities[i];
            existingEntityDataText.text += $"Name: {entityData.npcName}, Tool: {entityData.selectedItem}\n";
        }
    }

    /*public void UpdateRepairQueueUI()
    {
        repairQueueText.text = "Repair Queue: \n";

        for (int i = 0; i < queueSystem.repairQueue.Count; i++)
        {
            EntityData entityData = queueSystem.repairQueue[i];
            repairQueueText.text += $"Name: {entityData.npcName}, Tool: {entityData.selectedItem}, Urgency: {entityData.repairUrgency}\n";
        }
    }*/

}
