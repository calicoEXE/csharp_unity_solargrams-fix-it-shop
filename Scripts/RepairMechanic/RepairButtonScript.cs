using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class RepairButtonScript : MonoBehaviour
{
    public WorkshopManager refToWorkshopManagerScript;
    public RepairMechanicManagerClass refToRepairMechManagerScript;
    public WorkBenchClass reftoWorkBench;
    public DialogueManager dialogueManagerScript;
    public DialogueVariables dialogueVariables;
    public AudioManagerScript refToAudioMan;

    public GameObject storageCam;
    public GameObject repairCompleteButton;
    public GameObject storageButton;
    public GameObject workshopButton;
    public GameObject refurbishCam;
  //  public GameObject refurbishButton;
  //  public GameObject storageBackButton;

    
    void Start()
    {
        refToWorkshopManagerScript = FindObjectOfType<WorkshopManager>();
        refToRepairMechManagerScript = FindObjectOfType<RepairMechanicManagerClass>();
        reftoWorkBench = FindObjectOfType<WorkBenchClass>();
        dialogueManagerScript = FindObjectOfType<DialogueManager>();
        refToAudioMan = FindObjectOfType<AudioManagerScript>();
    

        //Cannot find Inactive GameObjects :/
        //  storageCam = GameObject.Find("StorageCam");
        //  repairCompleteButton = GameObject.Find("RepairCompleteButton");
        //  storageButton = GameObject.Find("StorageAccess");
        //  workshopButton = GameObject.Find("WorkshopAccess");
        //  refurbishCam = GameObject.Find("RefurbishCam");
        //  refurbishButton = GameObject.Find("RefurbishAccess");
        //  storageBackButton = GameObject.Find("StorageBack");



    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnRepairDone() 
    {
       // DialogueManager.GetInstance().UpdateRepairDoneVariable("repair_done", "true");
        Debug.Log("button pressed");
        reftoWorkBench.InteractionOff();
        if (refToRepairMechManagerScript.repairMechanicState == RepairMechanicManagerClass.state.repairInProgress)
        {
            refToWorkshopManagerScript.workshop = WorkshopManager.state.givingItemBack;
            refToRepairMechManagerScript.repairMechanicState = RepairMechanicManagerClass.state.noRepair;
            if(refToRepairMechManagerScript.failedRepairBool == true)
            {
                refToRepairMechManagerScript.RepairFail();
                refToRepairMechManagerScript.failedRepairBool = false;
            }
            else if(refToRepairMechManagerScript.successfulRepairBool == true)
            {
                refToRepairMechManagerScript.RepairSuccess();
                refToRepairMechManagerScript.successfulRepairBool = false;
            }
        }
        //refToAudioMan.onButtonDownAudio();
    }
public void StorageOn()
    {
        storageCam.SetActive(true);
        repairCompleteButton.SetActive(false);
        storageButton.SetActive(false);
        workshopButton.SetActive(true);
       // refurbishButton.SetActive(true);    
        refurbishCam.SetActive(false);
      //  storageBackButton.SetActive(false); 

    }

    public void RefurbishOn()
    {
        refurbishCam.SetActive(true);
      //  refurbishButton.SetActive(false);
        workshopButton.SetActive(false);
      //  storageBackButton.SetActive(true); 
    }

    public void StorageOff()
    { 
        storageCam.SetActive(false);
        repairCompleteButton.SetActive(true);
        storageButton.SetActive(true);
        workshopButton.SetActive(false);
       // refurbishButton.SetActive(false );
    }


}
