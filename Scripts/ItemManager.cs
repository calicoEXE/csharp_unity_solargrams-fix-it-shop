
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{





}

#region old code
/*using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public float minDurability = 0f;
    public float maxDurability = 1.0f;
    private int decimalPlaces = 1;
    public float selectedDurability;
    public float repairUrgency;

    void Start()
    {
        RandomItems();
    }

    void RandomItems()
    {
        Item[] refToItemScript = FindObjectsOfType<Item>(); // find all Itemss ONLY in scnene
        NPCManager[] npcs = FindObjectsOfType<NPCManager>();

        foreach (NPCManager npc in npcs)
        {
            Item selectedItem = SelectRandomItem(refToItemScript);
            if (selectedItem != null)
            {
                if (AssignItem(selectedItem)) // ONLY randomise durability for selectedItem
                {
                    selectedItem.durability = Random.Range(minDurability, maxDurability);
                    selectedItem.durability = Mathf.Round(selectedItem.durability * Mathf.Pow(10, decimalPlaces)) / Mathf.Pow(10, decimalPlaces);

                    selectedItem.CalculateRepairUrgency();
                    npc.ReceiveRepairUrgency(selectedItem.repairUrgency, selectedItem);

                    selectedDurability = selectedItem.durability;
                    repairUrgency = selectedItem.repairUrgency;
                }
            }
        }
    }

    Item SelectRandomItem(Item[] refToItemScript)
    {
        if (refToItemScript.Length > 0)
        {
            int randomIndex = Random.Range(0, refToItemScript.Length);
            return refToItemScript[randomIndex];
        }
        else
        {
            return null;
        }
    }

    bool AssignItem(Item assignItem) // ONLY pick gameobject marked "Item" for randomisation
    {
        if (assignItem.gameObject.layer == LayerMask.NameToLayer("Item")) // check if item is layered "Item"
        {
            return true;
        }

        return false;
    }
}
*/
#endregion