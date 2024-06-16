using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnboardingManagerScript : MonoBehaviour
{
    [TextArea]
    public List <string> grandmaCommentsList = new List<string>(); //has all of the comments
    public GameManager refToGM;
    public DayNightScript refToDayNightScript;
    public List <Sprite> UISprites = new List<Sprite>(); //has all of the ui icons


    public GameObject refToOnboardingParentGO;

    public Sprite GrandmaIcon;
    public TMP_Text tipText;

    public GameObject refurbishedElementPC;
    public CarrierElementClass pcMachineCarrier;
    public GameObject machinePC;
    public GameObject[] PC;

    public Button continueButton;
    
    //used to type the text out
    public float typingSpeed = 0.05f; // Adjust the typing speed as needed
    public string textToType;
    //private TMP_Text textMeshPro;
    public int currentIndex;
    public enum state { shopIntro, pcBroken, workbenchIntro, refurbishingProcess, refurbFinished, pcFixed, bedTime, blueprint};
    public state onboarding;

    //VisualPrompt
    public InteractionTriggerClass refToRepairVis;
    public InteractionTriggerClass refToPCVis;
    public InteractionTriggerClass refToRefurbishVis;
    public InteractionTriggerClass refToBedVis;
  //  public InteractionTriggerClass refToStorageVis;
    public WorkBenchClass refToworkBenchClass;
    public PCQueueTriggerScript refToPCScript;

    public AudioSource typingAudioSource;
    public AudioClip typingCLip;
    void Start()
    {
        refToGM = FindObjectOfType<GameManager>();
        refToDayNightScript = FindObjectOfType<DayNightScript>();
        refToworkBenchClass = FindObjectOfType<WorkBenchClass>();
        refToPCScript = FindObjectOfType<PCQueueTriggerScript>();
        //StartCoroutine(TypeText());
        if(machinePC.activeSelf == true)
        {
            machinePC.SetActive(false);
        }
        
    }
    public void onStartText() //if it needs to be started in another script comment same line in start out and call this function in script
    {
        StartCoroutine(TypeText());
    }

    void Update()
    {
        
        if (refToGM.oSCall == GameManager.overallState.onboarding)
        {
            if (refToPCScript.interactionActive == true)/////////checks whether workbench is active and continues the dialogue
            {
                if (FindObjectOfType<WorkshopManager>().workshop == WorkshopManager.state.working)
                {
                    Debug.Log("idleing");
                    FindObjectOfType<WorkshopManager>().WorkingStateChange(WorkshopManager.state.idle);
                }
            }
            skipTextEnd();
            #region currentIndex onboarding switch
            if(currentIndex == 0)
            {
                
                currentIndex = 1;
                StartCoroutine(TypeText());
            }
            else if (currentIndex == 1) //0
            {
                onboarding = state.shopIntro;
                //Introduce the workshop
                //introduce them to pc
            } 
            else if (currentIndex == 2) //1
            {
                onboarding = state.pcBroken;
                //Introduce the workshop
                //introduce them to pc
            }
            else if (currentIndex == 4) //1
            {
                onboarding = state.workbenchIntro;
                //Introduce the workshop
                //introduce them to pc
            }
            else if (currentIndex == 5) //4
            {
                onboarding = state.refurbishingProcess;
            }
            else if (currentIndex == 7)//7
            {
                onboarding = state.refurbFinished;
            }
            else if( currentIndex == 8)//blueprints 8
            {
                onboarding = state.blueprint;
            }
            else if (currentIndex == 9)//computer is fixed 9
            {
                onboarding = state.pcFixed;
            }
            else if (currentIndex == 10) //10
            {
                onboarding = state.bedTime;
                //bed trigger
            }
            
            else if (currentIndex == 12)//makes sure it doesn't loop 13
            {
                refToBedVis.VisualTriggerEnd();
            }
            
            #endregion
         
            if (onboarding == state.shopIntro)
            {
                if (FindObjectOfType<WorkshopManager>().workshop == WorkshopManager.state.working)
                {
                    FindObjectOfType<WorkshopManager>().WorkingStateChange(WorkshopManager.state.idle);
                }
            }
            else if(onboarding == state.pcBroken)
            {
                // introduce them to workbench
                //take pc item to workbench 
                
                refToPCVis.VisualTriggerStart();
                if (currentIndex == 2)
                {
                    continueButton.gameObject.SetActive(false);
                    if (FindObjectOfType<WorkshopManager>().workshop == WorkshopManager.state.working)
                    {
                        FindObjectOfType<WorkshopManager>().WorkingStateChange(WorkshopManager.state.idle);
                    }
                    bool triggerLocal = false; //stops button from constantly goign between them
                    if(triggerLocal == false)
                    {
                        if (refToPCVis.onbardingCheckPlayerAction == true)
                        {
                            for (int i = 0; i < PC.Length; i++)//sets pc visual parts false
                            {
                                PC[i].SetActive(false);
                            }
                            continueButton.gameObject.SetActive(true);
                            refToworkBenchClass.InteractionOff(); // to put workshop state back to idle so player can move
                            NextOnBoarding();                           
                        }
                        else
                        {

                        }
                        triggerLocal = true;
                    } 
                }
                if (currentIndex == 3)//pc on the workbench
                {
                    machinePC.SetActive(true);
                    continueButton.gameObject.SetActive(false);
                    if (FindObjectOfType<WorkshopManager>().workshop == WorkshopManager.state.working)
                    {
                        FindObjectOfType<WorkshopManager>().WorkingStateChange(WorkshopManager.state.idle);
                    }                   
                    refToPCVis.VisualTriggerEnd();
                    refToRepairVis.VisualTriggerStart();
                    continueButton.gameObject.SetActive(true);
                }                              
            }
            else if(onboarding == state.workbenchIntro)//never called
            {
                if(refToPCVis.playerInteractionAreaCheck == true)
                {
                    if (FindObjectOfType<WorkshopManager>().workshop == WorkshopManager.state.working)
                    {
                        FindObjectOfType<WorkshopManager>().WorkingStateChange(WorkshopManager.state.idle);
                    }
                }
                continueButton.gameObject.SetActive(false);
                if (refToworkBenchClass.interactionActive == true)/////////checks whether workbench is active and continues the dialogue
                {
                    if (FindObjectOfType<WorkshopManager>().workshop == WorkshopManager.state.idle)
                    {
                        FindObjectOfType<WorkshopManager>().WorkingStateChange(WorkshopManager.state.working);
                    }
                }
                if (refToworkBenchClass.interactionActive == true)/////////checks whether workbench is active and continues the dialogue
                {
                    continueButton.gameObject.SetActive(true);
                }
                
            }
            else if(onboarding == state.refurbishingProcess)
            {
                if (refToPCVis.playerInteractionAreaCheck == true)
                {
                    if (FindObjectOfType<WorkshopManager>().workshop == WorkshopManager.state.working)
                    {
                        FindObjectOfType<WorkshopManager>().WorkingStateChange(WorkshopManager.state.idle);
                    }
                }
                //introduction to refurbishing
                refToPCVis.VisualTriggerEnd();
                if (currentIndex == 7)
                {
                    continueButton.gameObject.SetActive(false);
                }
                if(refurbishedElementPC.GetComponent<ElementDataScript>().usable == true)
                {
                    continueButton.gameObject.SetActive(true);
                    NextOnBoarding();
                }
            }
            else if(onboarding == state.refurbFinished)
            {
                if (refToPCVis.playerInteractionAreaCheck == true)
                {
                    if (FindObjectOfType<WorkshopManager>().workshop == WorkshopManager.state.working)
                    {
                        FindObjectOfType<WorkshopManager>().WorkingStateChange(WorkshopManager.state.idle);
                    }
                }
                refToPCVis.onbardingCheckPlayerAction = false;
            }
            else if (onboarding == state.blueprint)
            {
                if (refToPCVis.playerInteractionAreaCheck == true)
                {
                    if (FindObjectOfType<WorkshopManager>().workshop == WorkshopManager.state.working)
                    {
                        FindObjectOfType<WorkshopManager>().WorkingStateChange(WorkshopManager.state.idle);
                    }
                }
                continueButton.gameObject.SetActive(false);
                if (pcMachineCarrier.repaired ==true)//checks whether machine is fixed
                {
                    continueButton.gameObject.SetActive(true);
                    NextOnBoarding();
                }
            }
            else if (onboarding == state.pcFixed)
            {
                if (refToPCVis.playerInteractionAreaCheck == true)
                {
                    if (FindObjectOfType<WorkshopManager>().workshop == WorkshopManager.state.working)
                    {
                        FindObjectOfType<WorkshopManager>().WorkingStateChange(WorkshopManager.state.idle);
                    }
                }
                for (int i = 0; i < PC.Length; i++)
                {
                    PC[i].SetActive(true);
                }
                
                refToRepairVis.VisualTriggerEnd();
                refToPCVis.VisualTriggerStart();
            }
            else if (onboarding == state.bedTime)
            {

                if (currentIndex == 11)
                {
                    continueButton.gameObject.SetActive(false);
                }


                if (refToPCVis.playerInteractionAreaCheck == true)
                {
                    if (FindObjectOfType<WorkshopManager>().workshop == WorkshopManager.state.working)
                    {
                        FindObjectOfType<WorkshopManager>().WorkingStateChange(WorkshopManager.state.idle);
                    }
                }
                machinePC.SetActive(false);
                refToBedVis.VisualTriggerStart();
                if(refToDayNightScript.dayCycle == DayNightScript.states.nightSummary)
                {
                    this.GetComponent<OnboardingManagerScript>().SetFalse();
                }
            }
        }
        else
        {
            refToOnboardingParentGO.SetActive(false);
        }
    }

    IEnumerator TypeText()
    {
        PlayTypingSound();
        foreach (char c in grandmaCommentsList[currentIndex])
        {
            tipText.text += c;
            
            yield return new WaitForSeconds(typingSpeed);
        }
        typingAudioSource.Stop();
    }
    
    public void NextOnBoarding()
    {
        if (refToGM.oSCall == GameManager.overallState.onboarding)
        {
            // Check if currentIndex is within the bounds of grandmaCommentsList
            if (currentIndex < grandmaCommentsList.Count)
            {             
                if (currentIndex + 1 < grandmaCommentsList.Count)// Move to the next index in the array if available
                {
                    currentIndex++;
                    // Clear the tipText
                    tipText.text = "";
                    // Stop any existing text typing coroutine and start a new one
                    
                    StopAllCoroutines();
                    StartCoroutine(TypeText());
                }
            }
        }

    }
    void skipTextEnd()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Skip typing coroutine if text is still being typed
            if (tipText.text.Length < grandmaCommentsList[currentIndex].Length)
            {
                StopAllCoroutines();
                tipText.text = grandmaCommentsList[currentIndex]; // Set text to fully typed
                typingAudioSource.Stop();
            }
        }
    }

    public void PCRepairComplete()
    {
        //refToworkBenchClass.InteractionOff();
        NextOnBoarding();
        continueButton.gameObject.SetActive(true);

    }
    void PlayTypingSound()
    {
        if (typingAudioSource != null && typingCLip != null)
        {
            typingAudioSource.PlayOneShot(typingCLip);
        }
    }

    //IEnumerator PlayAudioWithDelay()
    //{
    //    yield return null; // Wait for one frame to avoid synchronization issues
        
    //}
    public void SetTrue()
    {
        refToOnboardingParentGO.SetActive(true);
    }

    public void SetFalse()
    {
        refToOnboardingParentGO.SetActive(false);
    }
}
