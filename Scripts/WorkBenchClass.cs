using Cinemachine;
using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkBenchClass : MonoBehaviour
{
    public GameObject repairCMCamera;
    public GameObject storageCam;
    public GameObject refurbishCam;

    public CinemachineVirtualCamera mainPlayerCamera;
    //public GameObject workPanel;

    public bool interactionActive;
    public InteractionTriggerClass refToIntClass;
    public GameManager refToGM;

    private void Start()
    {
        refToIntClass = GetComponentInParent<InteractionTriggerClass>();
        refToGM = GetComponentInParent<GameManager>();
        //if(workPanel.activeSelf == true)
        //{
        //    workPanel.SetActive(false);
        //}
    }
    void Update()
    {
        if(refToIntClass.playerInteractionActiveCheck == true)
        {
            InteractionON();
            //Debug.Log("Interactionclass check last is active");
        }
        else
        {
            //InteractionOff();
            //Debug.Log("Interactionclass check last is deactdive");

        }
        // Player can exit the mechanic with ESC
      // if (Input.GetKeyDown(KeyCode.Escape))
      // {
      //     InteractionOff();
      //     refToIntClass.playerInteractionActiveCheck = false;
      //     if (FindObjectOfType<WorkshopManager>().workshop != WorkshopManager.state.givingItemBack)//makes sure that the item is not being returned
      //     {
      //         FindObjectOfType<WorkshopManager>().WorkingStateChange(WorkshopManager.state.idle);
      //     }
      //     
      //
      // }

    }

    public void InteractionON()
    {
        interactionActive = true;
        refToIntClass.PlayerModelGone();

       //workPanel.SetActive(true);
       repairCMCamera.SetActive(true);
        refToIntClass.interactionPrompt.SetActive(false);
        refToIntClass.VisualTriggerEnd();
        refToIntClass.outlineOff();
        //camera is switching cause repair cam is getting active so we can ignore playercam
        //need to disable player movement when in this state
    }

    public void InteractionOff()
    {
        //workPanel.SetActive(false);
        interactionActive = false;
        refToIntClass.PlayerModelBack();

        refToIntClass.playerInteractionActiveCheck = false;
        if (FindObjectOfType<WorkshopManager>().workshop != WorkshopManager.state.givingItemBack)//makes sure that the item is not being returned
        {
            if (FindObjectOfType<WorkshopManager>().workshop != WorkshopManager.state.playerHasItem)
            {
                //Debug.Log("Workbench");
                FindObjectOfType<WorkshopManager>().WorkingStateChange(WorkshopManager.state.idle); //not here
            }
        }
        repairCMCamera.SetActive(false);
        storageCam.SetActive(false);
        refurbishCam.SetActive(false);   
    }
}
