using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadioInteractionScript : MonoBehaviour
{
   
    public List<string> collisionTriggerGO = new List<string>();
    public bool playerInteractionAreaCheck = false;
    public bool playerInteractionActiveCheck = false;
    public GameObject interactionPrompt;
    //public GameObject refToVisualCue;
    public AudioManagerScript refToAudioManager;

    private void Start()
    {
        refToAudioManager = FindObjectOfType<AudioManagerScript>();
        var outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = 5f;
        outline.enabled = false;
        //check for whetehr visual cue is active on start
        //if (refToVisualCue.activeSelf == true)
        //{
        //    refToVisualCue.SetActive(false);

        //}
    }

    void Update()
    {

        if (playerInteractionAreaCheck == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerInteractionActiveCheck = true;
                refToAudioManager.SwitchBGAudio();
                //FindObjectOfType<WorkshopManager>().WorkingStateChange(WorkshopManager.state.working);

            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var outline = gameObject.GetComponent<Outline>();
            outline.enabled = true;
            //VisualTriggerStart();
            interactionPrompt.SetActive(true);
            //collisionTriggerGO.Add(other.name);
            //Debug.Log(other.name + "in");
            playerInteractionAreaCheck = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            var outline = gameObject.GetComponent<Outline>();
            outline.enabled = false;
            //VisualTriggerEnd();
            interactionPrompt.SetActive(false);
            playerInteractionAreaCheck = false;
            //Debug.Log(other.name + "out");
            //collisionTriggerGO.Remove("Player");
        }
    }

    //public void VisualTriggerStart()
    //{
    //    refToVisualCue.SetActive(true);

    //}
    //public void VisualTriggerEnd()
    //{
    //    refToVisualCue.SetActive(false);

    //}


}
