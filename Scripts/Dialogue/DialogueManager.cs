using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System.Linq;

public class DialogueManager : MonoBehaviour
{


    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;

    [SerializeField] private TextMeshProUGUI dialogue;

    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private TextMeshProUGUI dialogueNameText;

    [SerializeField] private Animator portraitAnimator;

    private Animator layoutAnimator;

    [Header("Globals_Load JSON")]
    [SerializeField] private TextAsset Globals_LoadJSON;


    [Header("Dialogue Choices")]
    [SerializeField] private GameObject[] choices;

    private TextMeshProUGUI[] choicesText;

    public bool dialogueisPlaying  { get;private set; }    

    public Story currentStory;

    private static DialogueManager instance;

    public const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";

    private DialogueVariables dialogueVariables;

    public RepairButtonScript repairButton;

    public string tagValue;

    public enum VariableTypes
    {
        BOOL,
        INT,
        STRING,
    }

    private void Awake()
    {

        if (instance != null)
        {
            //Debug.LogWarning("Found more than one dialogue manager in the scene");
        }


        instance = this; 

        dialogueVariables = new DialogueVariables(Globals_LoadJSON);
    }

    public static DialogueManager GetInstance() 
    {
        return instance; 
    }   

    private void Start()
    {
        dialogueisPlaying = false;  
        dialoguePanel.SetActive(false); 

        // get the layout animator
        layoutAnimator = dialoguePanel.GetComponent<Animator>();

        //get the choices text

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)      
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (!dialogueisPlaying)                     //return if dialogue isn't playing
        {
            return;
        }
        if (currentStory.currentChoices.Count == 0
            && Input.GetMouseButtonDown(0))          //pressing submit button continues the dialouge
        {
            ContinueStory();
        }
    }


    public void EnterDialogueMode(TextAsset inkJSON)            //Get story from the NPCs ink file and enable dialogueUI
    {
        currentStory = new Story(inkJSON.text);
        dialogueisPlaying = true;
        dialoguePanel.SetActive(true);

        dialogueVariables.StartListening(currentStory);

        currentStory.variablesState.variableChangedEvent += VariablesState_variableChangedEvent;

        ContinueStory();

    }

    private void VariablesState_variableChangedEvent(string variableName, Ink.Runtime.Object newValue)
    {
        Debug.Log(variableName);
    }

   public IEnumerator ExitDialogueMode()                   //Disable dialogue UI when done with dialogue
    {
        yield return new WaitForSeconds(0.8f);

        dialogueVariables.StopListening(currentStory);

        dialogueisPlaying = false;
        dialoguePanel.SetActive(false);
        dialogue.text = "";

    }

    public void ContinueStory()                             // conintue story to the next line, if there is any
    {
        if (currentStory.canContinue)
        {
            //set text for current dialogue line
            dialogue.text = currentStory.Continue();

            //display choices, if any for this dialogue line
            DisplayChoices();
            // handle tags
            HandleTags(currentStory.currentTags);
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }
    private void HandleTags(List<string> currentTags)
    {
        foreach(string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be parsed" + tag);
            }
            string tagKey = splitTag[0].Trim();
            tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    dialogueNameText.text = tagValue;
                    break;
                    case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;
                    case LAYOUT_TAG:
                    layoutAnimator.Play(tagValue);
                    break;
                    default:
                    Debug.LogWarning("Tage came in but is not being handled:" + tag);
                    break;
            }
        }
    }
    private void DisplayChoices()                       //Display choice buttons, if there are any
    {

        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)                          //check if choices are more than UI can handle
        {
            Debug.LogError("More choices were given than UI can support." +
                " Number of choices give,:" + currentChoices.Count);
        }


        int index = 0;

        foreach (Choice choice in currentChoices)                           //enable and initialize the choices upto the amount of choices for this line of dialogue
        {
            choices[index].gameObject.SetActive (true);
            choicesText[index].text = choice.text;
            index++;
        }

        //go through the remaining choices the UI supports and hide them

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        StartCoroutine(SelectFirstChoice()); 
    }    


    private IEnumerator SelectFirstChoice()  //Force unity to select the first choice in a dialogue
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);

    }

    public void MakeChoice (int choiceIndex)    //Make the choice in the ink story
    {

        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null:" + variableName);
        }
        return variableValue;
    }
    //public void UpdateRepairDoneVariable(string variableName, string newValue)
    //{
    //    // Check if the variable exists in the dictionary
    //    if (dialogueVariables.variables.ContainsKey(variableName))
    //    {
    //        currentStory.variablesState[variableName] = newValue;

    //        // Update the variable in the dictionary
    //        // ...

    //        // Set dialogueisPlaying to false
    //        dialogueisPlaying = false;
    //        Debug.Log($"Variable '{variableName}' updated to: {newValue}");
    //    }
    //    else
    //    {
    //        Debug.LogError($"Dictionary Variable '{variableName}' not found.");
    //    }
        //// Update the variable in the dictionary
        //object newValueTemp = newValue;
        //switch (type)
        //{
        //    case VariableTypes.BOOL:
        //        {
        //            dialogueVariables.variables[variableName] = (Ink.Runtime.BoolValue)newValueTemp;
        //            break;
        //        }
        //    case VariableTypes.INT:
        //        {
        //            dialogueVariables.variables[variableName] = (Ink.Runtime.IntValue)newValueTemp;
        //            break;
        //        }
        //    case VariableTypes.STRING:
        //        {
        //            dialogueVariables.variables[variableName] = (Ink.Runtime.StringValue)newValueTemp;
        //            break;
        //        }
        //    default:
        //        Debug.Log("TYpe not handled!");
        //        break;
        //}

        //Debug.Log($"Variable '{variableName}' updated to: {newValue}");
        //    }
        //    else
        //    {
        //        Debug.LogError($"Dictionary Variable '{variableName}' not found.");
        //    }
        //}
   
}
