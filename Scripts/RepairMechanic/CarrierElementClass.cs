using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CarrierElementClass : MonoBehaviour
{

    //public GameManager refToGM;

    public enum elementparts { Driver1, Wire0, Wire1, Driver2, Transmission };
    public elementparts DesiredElement;
    public DropAreaManager dropAreaManager;
    public GameObject dropManager;




    //public List<string> carrierList = new List<string>();
    public List<ElementDataScript> carrierList = new List<ElementDataScript>();
    
    public List<string> carrierNameList = new List<string>();
    public WireScript refToWireScript;

    public enum repairDiff{lvl1, lvl2};
    public repairDiff repairMech;

    public bool batteryCheck;
    public bool motorCheck;
    public bool transmissionCheck;
    public bool allwiresCheck;
    //public bool check4;

    public bool repaired = false;
    //public bool narrRepaired = false;

    public ElementDataScript refToElementDataScript;
    //string typeElement;
    //bool usability;

    public List<GameObject> objectsWithRotateScript = new List<GameObject>();
    // public List<GameObject> objectCollisionstoRemove= new List<GameObject>();
    public GameObject freezeCollision;

    //DropArea
    public GameObject targetObject;
    public List<DropAreaManager> dropAreaslots = new List<DropAreaManager>();


    public RepairMechanicManagerClass refToRepairMechManagerScript;
    public AudioManagerScript audioManagerScript;


    // Visual feedback for work element 
    public GameObject workElement;
    public Material fixedState;
    public Material brokenState;


    public GameObject blueprint;



    void Start()
    {
         

        refToRepairMechManagerScript = FindObjectOfType<RepairMechanicManagerClass>();
        refToWireScript = FindObjectOfType<WireScript>();
        audioManagerScript = FindObjectOfType<AudioManagerScript>();


        if (targetObject == null)       
        {
            //Debug.LogWarning("Target GameObject is not assigned. Please assign it in the Inspector.");
            return;
        }
        DropAreaManager[] childComponents = targetObject.GetComponentsInChildren<DropAreaManager>();

        foreach (DropAreaManager childComponent in childComponents)     // Find all children of the targetObject with the DropAreaManager component
        {
           
            dropAreaslots.Add(childComponent);                          // Add each child component to the list
        }   

    }

    void Update()
    {
        // foreach item of the target list (dropslots), Find same element in the carrierList 

        // carrierList.Find(dropAreaslots[0]);
        bool listsMatch = true;

        if (dropAreaslots.Count == carrierList.Count)
        {
            for (int i = 0; i < dropAreaslots.Count; i++)
            {
                bool foundItemType = false;
                for (int j = 0; j < carrierList.Count; j++)
                {
                    if (carrierList[i].elementType == dropAreaslots[j].dropType)
                    {
                        foundItemType = true;
                    }
                }
                listsMatch = foundItemType;
            }
        }
        else
        {
            refToRepairMechManagerScript.itemRepaired = repaired;
            
            listsMatch = false;
            MeshRenderer carrierMat = workElement.GetComponent<MeshRenderer>();
            carrierMat.material = brokenState;
        }


        if (listsMatch)
        {
            audioManagerScript.MachineSolved();
            //Debug.Log("Lists match!");
            repaired = true;
            MeshRenderer carrierMat = workElement.GetComponent<MeshRenderer>();
            carrierMat.material = fixedState;
            refToRepairMechManagerScript.itemRepaired = repaired;
            refToRepairMechManagerScript.successfulRepairBool = true;
            refToRepairMechManagerScript.failedRepairBool = false;
            //refToRepairMechManagerScript.RepairSuccess();//adds up the integer to keep count of how many repairs the player has done
            EnableRotationForList();
            freezeCollision.GetComponent<BoxCollider>().enabled = true;



        }
        else
        {
            refToRepairMechManagerScript.failedRepairBool = true;
            //refToRepairMechManagerScript.RepairFail();
        }





        #region oldrepairlogic
        if (repairMech == repairDiff.lvl1)
        {

            foreach(ElementDataScript elementDataScriptLocal in carrierList)
            {
                if(elementDataScriptLocal.elementType == droptype.battery && elementDataScriptLocal.usable == true)
                {
                    batteryCheck = true;               
                }
                else
                {
                    //check1 = false;
                }
                if (elementDataScriptLocal.elementType == droptype.motor && elementDataScriptLocal.usable == true)
                {
                    motorCheck = true;                  
                }
                else
                {
                    //check2 = false;
                }
                if (elementDataScriptLocal.elementType == droptype.transmission && elementDataScriptLocal.usable == true)
                {
                    transmissionCheck = true;                  
                }
                else
                {
                    //check2 = false;
                }
            }
            if (refToWireScript.wireCheck == true)
            {
                allwiresCheck = true;
                Debug.Log("allwirescheck");
                repaired = true;
            }
            else if(batteryCheck == true &&  motorCheck==true && transmissionCheck == true &&  allwiresCheck == true)
            {
                repaired = true;
            }

            



            #region old code
            //     if (carrierNameList.Contains("driver") )//|| carrierList.Contains("WorkElement"))
            //     {
            //
            //         check1 = true;
            //         Debug.Log("check1");
            //
            //         if (carrierNameList.Contains("work"))//|| carrierList.Contains("WorkElement"))
            //         {
            //             check2 = true;
            //             Debug.Log("check2");
            //
            //             if (refToWireScript.wireCheck == true)
            //             {
            //                 check3 = true;
            //                 Debug.Log("check3");
            //                 repaired = true;
            //             }
            //         }
            //     }
            #endregion old code
        }
        #endregion oldrepairlogic

        #region lvl2

        //  else if (repairMech == repairDiff.lvl2)
        //  {
        //      #region old code
        /*
        foreach (GameObject carrier in carrierList)
        {
            var refToElementScriptLocal = GetComponent<ElementDataScript>();
            if (refToElementScriptLocal.usable == true && refToElementScriptLocal.elementType == ElementDataScript.type.drive)
            {
                Debug.Log("check1");
                check1 = true;
            }
            else if (refToElementScriptLocal.usable == true && refToElementScriptLocal.elementType == ElementDataScript.type.work)
            {
                Debug.Log("check2");

                check2 = true;
            }
            else if (refToElementScriptLocal.usable == true && refToElementScriptLocal.elementType == ElementDataScript.type.control)
            {
                Debug.Log("check3");

                check3 = true;
            }

        }
        */
        //     #endregion old code
        //      foreach (ElementDataScript elementDataScriptLocal in carrierList)
        //      {
        //          if (elementDataScriptLocal.elementType == ElementDataScript.type.battery && elementDataScriptLocal.usable == true)
        //          {
        //              Debug.Log("hadlkfjhsdflkjh");
        //              batteryCheck = true;
        //          }
        //          else
        //          {
        //              batteryCheck = false;
        //          }
        //          if (elementDataScriptLocal.elementType == ElementDataScript.type.work && elementDataScriptLocal.usable == true)
        //          {
        //              transmissionCheck = true;
        //          }
        //          else
        //          {
        //              transmissionCheck = false;
        //          }
        //          if (elementDataScriptLocal.elementType == ElementDataScript.type.control && elementDataScriptLocal.usable == true)
        //          {
        //              check3 = true;
        //          }
        //          else
        //          {
        //              check3 = false;
        //          }
        //      }
        //      if (refToWireScript.wireCheck == true)
        //      {
        //          check4 = true;
        //          repaired = true;
        //      }
        //      else if (driveCheck == true && transmissionCheck == true && check3 == true && check4 == true)
        //      {
        //          repaired = true;
        //      }
        //  }
        #endregion lvl2
    }


    #region mouse drag and drop
    // void OnMouseDown()
    // {
    //     transform.GetComponent<Collider>().enabled = false;
    // }
    //
    // void OnMouseDrag()
    // {
    //     transform.position = MouseWorldPosition(); //+ offset;
    // }
    //
    // void OnMouseUp()
    // {
    //     var rayOrigin = Camera.main.transform.position;
    //     var rayDirection = MouseWorldPosition() - Camera.main.transform.position;
    //
    //     transform.GetComponent<Collider>().enabled = true;
    // }
    //
    // Vector3 MouseWorldPosition()
    // {
    //     var mouseScreenPos = Input.mousePosition;
    //     mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
    //     return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    // }
    #endregion


    public void OnCollisionEnter(Collision collision)
    {
        
      // carrierList.Add(collision.collider.gameObject.GetComponent<ElementDataScript>());
      // carrierNameList.Add(collision.collider.GetComponent<ElementDataScript>().elementType.ToString());
    }
    
    public void OnCollisionExit(Collision collision)
    {
        //adds element to list of carrier
      //  carrierList.Remove(collision.collider.gameObject.GetComponent<ElementDataScript>());
      //  carrierNameList.Remove(collision.collider.GetComponent<ElementDataScript>().elementType.ToString());
    }

    void EnableRotation(List<GameObject> objects)
    {
        foreach (GameObject obj in objects)
        {
            // Enable RotateObject script for each GameObject in the list
            RotateObject rotateScript = obj.GetComponent<RotateObject>();
            if (rotateScript != null)
            {
                rotateScript.enabled = true;
            }
        }
    }

    // ... (other code)

    // Call this function when you want to enable RotateObject scripts for GameObjects in the list
    public void EnableRotationForList()
    {
        EnableRotation(objectsWithRotateScript);
    }

    public void DisableAllCollisions()
    {

       

    }


}
