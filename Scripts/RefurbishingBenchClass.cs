using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefurbishingBenchClass : MonoBehaviour
{
    public GameObject refurbishingCMCamera;
    public GameObject mainPlayerCamera;
    public GameObject refurbPanel;

    public bool interactionActive;
    public InteractionTriggerClass refToIntClass;
   

    void Start()
    {
        var outline = gameObject.AddComponent<Outline>();
        outline.enabled = false;


        refToIntClass = GetComponentInParent<InteractionTriggerClass>();
        if (refurbPanel.activeSelf == true)
        {
            refurbPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (refToIntClass.playerInteractionActiveCheck == true)
        {
            RefurbInteractionON();
            //Debug.Log("Interactionclass check last is active");
        }
        else
        {
            RefurbInteractionOff();
            //Debug.Log("Interactionclass check last is deactdive");
        }
        //Player can exit the mechanic with ESC
        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    RefurbInteractionOff();
        //    refToIntClass.playerInteractionActiveCheck = false;
        //    if (FindObjectOfType<WorkshopManager>().workshop != WorkshopManager.state.givingItemBack)//makes sure that the item is not being returned
        //    {
        //        if (FindObjectOfType<WorkshopManager>().workshop != WorkshopManager.state.playerHasItem)
        //        {
        //            Debug.Log("refurbish");
        //            FindObjectOfType<WorkshopManager>().WorkingStateChange(WorkshopManager.state.idle); //not here Here
        //        }
        //        //FindObjectOfType<WorkshopManager>().WorkingStateChange(WorkshopManager.state.idle);
        //    }
            
        //}
    }

    void RefurbInteractionON()
    {
      

        refurbPanel.SetActive(true);
        refurbishingCMCamera.SetActive(true);
        refToIntClass.interactionPrompt.SetActive(false);
    }

    void RefurbInteractionOff()
    {
        refurbishingCMCamera.SetActive(false);
        refurbPanel.SetActive(false);

        
        
    }
}
