using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedScript : MonoBehaviour
{
    public DayNightScript refToDayNightScript;
    public WorkshopManager refToWorkShopManagerScript;
    public InteractionTriggerClass refToBedTriggerClass;
    public GameManager refToGM;

    void Start()
    {
     refToGM = FindObjectOfType<GameManager>();   
    }

    void Update()
    {
        if(refToGM.oSCall != GameManager.overallState.onboarding)
        {
            if (refToDayNightScript.dayCycle == DayNightScript.states.dayEnd)
            {
                if (refToBedTriggerClass.playerInteractionActiveCheck == true)
                {
                    refToWorkShopManagerScript.workshop = WorkshopManager.state.sleeping;
                    refToDayNightScript.dayCycle = DayNightScript.states.nightSummary;
                }
            }
        }
        else //needed for the onboarding that the player can go to sleep whenever
        {
            if (refToBedTriggerClass.playerInteractionActiveCheck == true)
            {

                refToWorkShopManagerScript.workshop = WorkshopManager.state.sleeping;
                refToDayNightScript.dayCycle = DayNightScript.states.nightSummary;
            }
        }
        
        
    }
}
