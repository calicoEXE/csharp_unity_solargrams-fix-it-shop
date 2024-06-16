using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManagerScript : MonoBehaviour
{
    public MachineMasterScript refToMachineMasterScript;
    public RepairMechanicManagerClass refToRepairMechanicManagerScript;
    public TownManager refToTownManagerScript;

    void Start()
    {
        refToRepairMechanicManagerScript = FindObjectOfType<RepairMechanicManagerClass>();
        refToTownManagerScript = FindObjectOfType<TownManager>();
    }

    
    void Update()
    {
        //create function to give a random item
        
    }
}
