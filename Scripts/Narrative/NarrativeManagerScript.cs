using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeManagerScript : MonoBehaviour
{

    public GameManager refToGM;
    public DayNightScript refToDayNightScript;
    public DialogueManager refToDiaMan;
    public WorkshopManager refToWorkShopManager;
    public QuestUIList refToQuestUI;

    public List<GameObject> coreNPCs = new List<GameObject>();
    public List<GameObject> coreItems = new List<GameObject>();
    public Dictionary<int, GameObject> coreNPCsDictionary = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> coreItemDictionary = new Dictionary<int, GameObject>();

    public Transform narrativeSpawnPoint;
    public Transform kioskSpawnPoint;
    public enum state { prologue, act1, act2};
    public state narrativeState;

    public List<int> storyBeatDays = new List<int>(); ///enter the days when there is a story beat
    public int narrIndex;
    bool spawnTime;
    public bool storyBeat;

    public Transform workBenchPoint;
    public bool npcInteraction;

    void Start()
    {
        refToDiaMan = FindObjectOfType<DialogueManager>();
        refToWorkShopManager = FindObjectOfType<WorkshopManager>();
        refToDayNightScript = FindObjectOfType<DayNightScript>();
        refToGM = FindObjectOfType<GameManager>();
        refToQuestUI = FindObjectOfType<QuestUIList>();
        narrativeState = state.prologue;
    }

    // Update is called once per frame
    void Update()
    {
        if (narrativeState == state.prologue)//divides the story up
        {
            //check whether oSCall from GM should be storyPlay
            if (refToDayNightScript.currentDay == 2)
            {
                narrIndex = 0; // set the narrNPC here to communicate the storybeats by spawning the right NPC
            }
            else if (refToDayNightScript.currentDay == 3)
            {
                narrIndex = 1;
            }
        }
        
        if (refToGM.oSCall == GameManager.overallState.storyPlay)
        {
            if (refToDayNightScript.dayCycle == DayNightScript.states.dayStart)
            {
                //Debug.Log("narr Spawn done");
                SpawnCoreNPC(); //npc gets spawned from function below
            }
            if(npcInteraction == true)////////////////////////////switch from story to free happens HERE///////
            {

                refToGM.oSCall = GameManager.overallState.freePlay;
                refToQuestUI.repairState = QuestUIList.repairStates.choose;
                refToQuestUI.UpdateUIData();
                npcInteraction = false;
            }
        }
        if (refToDayNightScript.dayCycle == DayNightScript.states.nightSummary) //checks whether there is a storyBeat the next day
        {
            ////////ADD A NARRREPAIRDONE CONDITION
            if(storyBeatDays.Contains(refToDayNightScript.currentDay + 1))
            {
                storyBeat = true;
                refToGM.oSCall = GameManager.overallState.storyPlay;
            }
            else
            {
                storyBeat = false;
                refToGM.oSCall = GameManager.overallState.freePlay;

            }
        }
    }

    void SpawnCoreNPC()
    {
        //Debug.Log("SpawnedNPCSPAWNING");
        //defines where to get the coreNPC from and sets it inactive
        GameObject newNarrNPC = Instantiate(coreNPCs[narrIndex], narrativeSpawnPoint.position, Quaternion.identity);
        newNarrNPC.AddComponent<NPC>();
        coreNPCsDictionary.Add(narrIndex, newNarrNPC);//ads to dictionary

        GameObject newNarrItem = Instantiate(coreItems[narrIndex], workBenchPoint.position, Quaternion.identity);
        coreItemDictionary.Add(narrIndex, newNarrItem);
        //newNarrNPC.GetComponent<NPC>().enabled = false;
        newNarrNPC.SetActive(false);
        newNarrItem.SetActive(false);
        

    }
}
