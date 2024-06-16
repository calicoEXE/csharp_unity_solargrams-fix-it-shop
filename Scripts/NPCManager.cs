using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public float minCompetency = 0f;
    public float maxCompetency = 1.0f;
    private int decimalPlaces = 1;
    public float competencyLevel;

    public Item selectedItem;

    public static NPCManager instance;

    private void Awake()
    {
        instance = this;
    }

    public float GetRandomCompetency()
    {
        competencyLevel = Random.Range(minCompetency, maxCompetency); // generate random competency level for this NPC
        competencyLevel = Mathf.Round(competencyLevel * Mathf.Pow(10, decimalPlaces)) / Mathf.Pow(10, decimalPlaces); // round competency level to the specified decimal places

        Debug.Log("NPC Competency Level: " + competencyLevel);

        return competencyLevel;
    }

    public void ReceiveRepairUrgency(float urgency, Item assignItem)
    {
        selectedItem = assignItem;
        PutNPCInQueue();
    }

    void PutNPCInQueue()
    {

    }
}