using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DialogueTrigger : MonoBehaviour
{

    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON; 

    private bool playerinRange;
    public bool playerInteract;

    public enum state {  fluffTalk, giveItem};
    public state NPCState;

    public NPC refToNPC;
    private void Awake()
    {
        playerinRange = false;
        refToNPC = this.GetComponent<NPC>();

        var outline = gameObject.AddComponent<Outline>();
        outline.enabled = false;

    }

    private void Update()
    {
        if (playerinRange && !DialogueManager.GetInstance().dialogueisPlaying)  //check if player is in range to trigger dialogue
        {
            visualCue.SetActive(true);
            if (Input.GetButtonDown("Fire1"))                                   //Initiate dialogue when pressed on fire1 button
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                playerInteract = true;
                //refToNPC.dialogueStates = NPC.diaState.diaStart;///////////////////////////////////////////triggers start of dialogue
            }
        }
        else 
        {
            visualCue.SetActive(false);
            playerInteract = false;
        }    
    }

    private void OnTriggerEnter(Collider other)                             
    {
        if (other.gameObject.tag == "Player")
        {
            var outline = gameObject.GetComponent<Outline>();
            outline.OutlineMode = Outline.Mode.OutlineAll;
            outline.OutlineColor = Color.yellow;
            outline.OutlineWidth = 5f;
            outline.enabled = true;
            
            playerinRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var outline = gameObject.GetComponent<Outline>();
            outline.enabled = false;
            playerinRange = false;
        }
    }

}
