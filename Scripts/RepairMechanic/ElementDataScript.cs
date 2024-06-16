using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElementDataScript : MonoBehaviour
{
    public bool quality; //true = good quality
    public bool usable;
    public enum type { battery, motor, transmission, carrier, control, droparea, work, drive};
    public droptype elementType;

    public WorkshopManager refWorkshopMang;   

    public void Start()
    {
        refWorkshopMang = FindObjectOfType<WorkshopManager>();
        var outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.red;
        outline.OutlineWidth = 6f;
        outline.enabled = false;
    }

    public void Update()
    {

        if (refWorkshopMang.workshop == WorkshopManager.state.working)
        {
            //Debug.Log("workshop enabled");
            if (!usable) 
            {
                var outline = gameObject.GetComponent<Outline>();
                outline.OutlineColor = Color.red;
                outline.enabled = true;



            }

            if (usable)
            {
                var outline = gameObject.GetComponent<Outline>();
                outline.enabled = false;

               //if (refWorkshopMang.workshop == WorkshopManager.state.working)
               //{
               //    
               //    //outline.OutlineColor = Color.white;
               //}

            }


        }

        if (refWorkshopMang.workshop == WorkshopManager.state.idle)
        {
            var outline = gameObject.GetComponent<Outline>();
            outline.enabled = false;
        }



        

    }   
    
}

