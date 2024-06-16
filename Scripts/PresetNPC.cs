using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetNPC : MonoBehaviour
{
    [Header("NPC Properties")]
    public float npcCompetency = 0f;
    public float itemDurability = 0f;
    public GameObject assignedItem;

    [Header("Dialogue Properties")]
    public TextAsset dialogueData;
    public GameObject visualCue;

    private GameObject currentItem;
    private float repairUrgency;

    public float RepairUrgency
    {
        get { return repairUrgency; }
    }

    private void Start()
    {
        AssignItem();
        CalculateCoreRepairUrgency();
    }

    private void Update()
    {
        // only recalculate urgency if npcCompetency or itemDurability changes.
        if (Mathf.Approximately(repairUrgency, CalculateCoreRepairUrgency()))
        {
            CalculateCoreRepairUrgency();
        }


        if (!DialogueManager.GetInstance().dialogueisPlaying)
        {
            visualCue.SetActive(true);
            if (Input.GetButtonDown("Fire1"))
            {
                DialogueManager.GetInstance().EnterDialogueMode(dialogueData);
            }
            else
            {
                visualCue.SetActive(false);
            }
        }
    }

    private void Interact()
    {
        if (dialogueData != null)
        {
            DialogueManager.GetInstance().EnterDialogueMode(dialogueData);
        }
    }

    private float CalculateCoreRepairUrgency()
    {
        repairUrgency = 1 - (npcCompetency * itemDurability);
        return repairUrgency;
    }

    private void AssignItem()
    {
        if (assignedItem != null)
        {
            currentItem = Instantiate(assignedItem);
            currentItem.SetActive(true);
            //currentItem = Instantiate(assignedItem, transform.position, Quaternion.identity, transform);
            //currentItem.transform.localPosition = Vector3.zero;
        }
        else
        {
            Debug.LogError("Item is not assigned to the NPC.");
        }
    }
}
