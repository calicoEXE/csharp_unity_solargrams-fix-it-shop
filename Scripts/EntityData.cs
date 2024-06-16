using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityData
{

    /// This script is a structure script for the data needed
    /// for EntityStorageSystem and QueueSystem.

    public string npcName;
    public float npcCompetency;
    public string selectedItem;
    public float itemDurability;
    public float repairUrgency;
    public TextAsset dialogueData;

    public bool isRepairCompleted;
    public bool repairList { get; set; }
    public bool wasSelected;
    public int uniqueID;

    public EntityData(string npcName, float npcCompetency, string selectedItem, float itemDurability, float repairUrgency, int uniqueID)
    {
        this.npcName = npcName;
        this.npcCompetency = npcCompetency;
        this.selectedItem = selectedItem;
        this.itemDurability = itemDurability;
        this.repairUrgency = repairUrgency;
        this.uniqueID = uniqueID;
        isRepairCompleted = false;
    }
}
