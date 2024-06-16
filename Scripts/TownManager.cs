using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TownManager : MonoBehaviour
{
    //stores the ui
    //public TMP_Text refToCarrierUI;
    //public TMP_Text refToWorkUI;
    //public TMP_Text refToControlUI;
    //public TMP_Text refToDriverUI;
    //public TMP_Text refToTransmissionUI;
    public TMP_Text refToSuccessRepairUI;
    public TMP_Text refToFailedRepairUI;
    public TMP_Text refToIncompleteRepairUI;

    /// <summary>
    /// lists below get the info to be stored and then displayed
    /// POSSIBLE ADDITION:
    /// how many repair have been done
    /// </summary>
    //public List<GameObject> rewardCarrierList = new List<GameObject>();
    //public List<GameObject> rewardWorkList = new List<GameObject>();
    //public List<GameObject> rewardControlList = new List<GameObject>();
    //public List<GameObject> rewardDriverList = new List<GameObject>();
    //public List<GameObject> rewardTransmissionList = new List<GameObject>();



    public DayNightScript refToDayNightScript;
    public RepairMechanicManagerClass refToRepairMechanicManagerScript;

    private void Start()
    {
        refToRepairMechanicManagerScript = FindObjectOfType<RepairMechanicManagerClass>();
    }
    void Update()
    {
        //checks with the day script whether the day has ended and then it will run the function TownUpdate
        if(refToDayNightScript.dayCycle == DayNightScript.states.nightSummary)
        {
            TownUpdate();
        }
        else
        {

        }
    }
    void TownUpdate()
    {
         //townupdate is a function that simple gets the info from different scripts and makes sure that it is displayed to the correct UI
        //refToControlUI.text = rewardControlList.Count.ToString();
        //refToWorkUI.text = rewardWorkList.Count.ToString();
        //refToDriverUI.text = rewardDriverList.Count.ToString();
        //refToTransmissionUI.text = rewardTransmissionList.Count.ToString();
        refToSuccessRepairUI.text = refToRepairMechanicManagerScript.successfulRepairs.ToString();
        refToFailedRepairUI.text = refToRepairMechanicManagerScript.failedRepairs.ToString();
        refToIncompleteRepairUI.text = refToRepairMechanicManagerScript.incompleteRepairs.ToString();
    }

    
}
