using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InteractionTriggerClass : MonoBehaviour
{
   
    public List<string> collisionTriggerGO = new List<string>();
    public bool playerInteractionAreaCheck = false;
    public bool playerInteractionActiveCheck = false;
    public GameObject interactionPrompt;
    public GameObject refToVisualCue;
    public GameObject playerModel;

    public GameManager refToGM;

    public bool onbardingCheckPlayerAction;

    

    private void Start()
    {
        playerModel = GameObject.Find("Player");
        var outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = 5f;
        outline.enabled = false;
        refToGM = FindObjectOfType<GameManager>();

        //check for whetehr visual cue is active on start
        if (refToVisualCue.activeSelf == true)
        {
            refToVisualCue.SetActive(false);

        }
    }

    void Update()
    {
        
        if (playerInteractionAreaCheck == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerInteractionActiveCheck = true;
                if(this.gameObject.name == "Bed_Parent")//check for Bed so it doesnt freeze player
                {
                    
                }
                else
                {
                    FindObjectOfType<WorkshopManager>().WorkingStateChange(WorkshopManager.state.working);
                }
                
                
                onbardingCheckPlayerAction = true;
            }
        }       
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            outlineOn();
            VisualTriggerStart();
            interactionPrompt.SetActive(true);
            collisionTriggerGO.Add(other.name);
            //Debug.Log(other.name + "in");
            playerInteractionAreaCheck = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (collisionTriggerGO.Contains("Player"))
        {
            outlineOff();
            VisualTriggerEnd();
            interactionPrompt.SetActive(false);
            playerInteractionAreaCheck = false;
            //Debug.Log(other.name + "out");
            collisionTriggerGO.Remove("Player");
        }
    }

    public void VisualTriggerStart()
    {
        refToVisualCue.SetActive(true);

    }
    public void VisualTriggerEnd()
    {
        refToVisualCue.SetActive(false);

    }

    public void outlineOn()
    {
        var outline = gameObject.GetComponent<Outline>();
        outline.enabled = true;
    }

    public void outlineOff()
    {
        var outline = gameObject.GetComponent<Outline>();
        outline.enabled = false;
    }

    public void PlayerModelGone()
    {
        playerModel.SetActive(false);
        Debug.Log("PayerModelGone");
    }
    public void PlayerModelBack()
    {
        playerModel.SetActive(true);
        Debug.Log("PayerModelHere");

    }






}
