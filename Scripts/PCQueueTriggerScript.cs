using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCQueueTriggerScript : MonoBehaviour
{
    public GameObject pcCMCamera;
    public GameObject mainPlayerCamera;
    public GameObject queueRepairPanel;
    public InteractionTriggerClass refToIntClass;
    public GameManager refToGM;
    public bool interactionActive;
    public AudioManagerScript refToAudioScript;
    void Start()
    {
        refToIntClass = GetComponentInParent<InteractionTriggerClass>();
        refToGM = FindObjectOfType<GameManager>();
        refToAudioScript = FindObjectOfType<AudioManagerScript>();
        if (queueRepairPanel.activeSelf == true)
        {
            queueRepairPanel.SetActive(false);
        }
        
    }

    void Update()
    {
        if (refToGM.oSCall == GameManager.overallState.onboarding) //so ui doesnt show up during onboarding
        {
            if (refToIntClass.playerInteractionActiveCheck==true)
            {
                refToIntClass.playerInteractionActiveCheck =false;
            }

            //FindObjectOfType<WorkshopManager>().WorkingStateChange(WorkshopManager.state.idle);
        }
        else
        {
            if (refToIntClass.playerInteractionActiveCheck == true)
            {
                RefurbInteractionON();
                refToIntClass.PlayerModelGone();
                refToAudioScript.onPCBootSE();
                //Debug.Log("Interactionclass check last is active");
            }
            else
            {
                refToIntClass.PlayerModelBack();
                //RefurbInteractionOff();
                //Debug.Log("Interactionclass check last is deactdive");
            }
            //Player can exit the mechanic with ESC
          //  if (Input.GetKeyDown(KeyCode.Escape))
          //  {
          //      RefurbInteractionOff();
          //      refToIntClass.playerInteractionActiveCheck = false;
          //      if (FindObjectOfType<WorkshopManager>().workshop != WorkshopManager.state.givingItemBack)//makes sure that the item is not being returned
          //      {
          //          FindObjectOfType<WorkshopManager>().WorkingStateChange(WorkshopManager.state.idle);
          //      }
          //  }

        }       
    }

    public void RefurbInteractionON()
    {
        queueRepairPanel.SetActive(true);
       
        pcCMCamera.SetActive(true);
        refToIntClass.interactionPrompt.SetActive(false);
        refToIntClass.VisualTriggerEnd();
        refToIntClass.outlineOff();

    }

   public void RefurbInteractionOff()
    {
       // refToIntClass.PlayerModelBack();
        pcCMCamera.SetActive(false);
        queueRepairPanel.SetActive(false);
        refToIntClass.playerInteractionActiveCheck = false;
        if (FindObjectOfType<WorkshopManager>().workshop != WorkshopManager.state.givingItemBack)//makes sure that the item is not being returned
        {
            FindObjectOfType<WorkshopManager>().WorkingStateChange(WorkshopManager.state.idle);
        }
    }
}
