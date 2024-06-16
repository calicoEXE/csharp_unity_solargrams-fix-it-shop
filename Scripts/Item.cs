using UnityEngine;

public class Item : MonoBehaviour
{
    public float durability;
    public float repairUrgency;

    public float minDurability = 0f;
    public float maxDurability = 1.0f;
    private int decimalPlaces = 1;

    //public int itemUniqueID;
    public enum difficultyState { lvl1, lvl2 };
    public difficultyState itemDifficulty;

    public void GetRandomDurability()
    {
        durability = Random.Range(minDurability, maxDurability);
        durability = Mathf.Round(durability * Mathf.Pow(10, decimalPlaces)) / Mathf.Pow(10, decimalPlaces);

        CalculateRepairUrgency();

        //Debug.Log("Repair Urgency for Tool: " + repairUrgency);
    }

    public void CalculateRepairUrgency()
    {
        repairUrgency = 1 - (Random.Range(minDurability, maxDurability) * durability);
    }
}
