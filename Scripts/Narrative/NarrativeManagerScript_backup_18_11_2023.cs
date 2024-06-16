using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeManagerScript_backup_18_11_2023 : MonoBehaviour
{

    public GameManager refToGM;
    public DayNightScript refToDayNightScript;

    public Dictionary<GameObject, GameObject> coreItemNPC = new Dictionary<GameObject, GameObject>();

    public Transform narrativeSpawnPoint;
    public Transform kioskSpawnPoint;
    public enum state { prologue, act1, act2};
    public state narrativeState;
    void Start()
    {
        refToDayNightScript = FindObjectOfType<DayNightScript>();
        refToGM = FindObjectOfType<GameManager>();
        narrativeState = state.prologue;
    }

    // Update is called once per frame
    void Update()
    {
        if(refToGM.oSCall == GameManager.overallState.storyPlay)
        {
            if(narrativeState == state.prologue)
            {
                if (refToDayNightScript.currentDay == 1)
                {

                    if (refToDayNightScript.dayCycle == DayNightScript.states.dayStart)
                    {

                    }
                }
                else if (refToDayNightScript.currentDay == 2)
                {

                }
            }
            
            
        }
    }

    void TriggerNarrativeEvent()
    {
        //add in the NPCs that get spawn and the items, create new
        //Instantiate(coreItemNPC[i], narrativeSpawnPoint, Quaternion.identity);
    }
}
