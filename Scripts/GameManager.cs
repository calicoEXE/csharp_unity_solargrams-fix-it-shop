using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Cinemachine.CinemachineOrbitalTransposer;

public class GameManager : MonoBehaviour
{

    /// <summary>
    /// states - sets the functionality of the game
    /// 
    /// </summary>
    public enum overallState { start, freePlay, storyPlay, end, credits, onboarding, cutscene};
    public overallState oSCall;

    public DayNightScript refToSDCycle;
    public RefurbishingMechanicScript refToRefurbishScript;
    public OnboardingManagerScript refToOnboardingScript;
    public NarrativeManagerScript refToNarrativeManagerScript;
    public QuestUIList refToQuestUIScript;

    public GameObject repaircompleteButton;
    public bool pause;


    public Image fadeImage;
    public Image fadeHoneyImage;
    public float fadeDuration = 10f;

    private float timer;
    private bool isFading = false; // Added variable to track if fading is in progress

    void Start()
    {
        refToOnboardingScript = FindObjectOfType<OnboardingManagerScript>();
        refToNarrativeManagerScript = FindObjectOfType<NarrativeManagerScript>();
        refToQuestUIScript = FindObjectOfType<QuestUIList>();
        oSCall = overallState.start;
        //oSCall = overallState.storyPlay;

        //if (fadeImage == null)
        //{
        //    Debug.LogError("Fade Image is not assigned!");
        //    return;
        //}

        // Set the initial color of the Image to fully transparent
        Color initialColor = fadeImage.color;
        initialColor.a = 0f;
        fadeImage.color = initialColor;
        
        if (fadeImage.gameObject.activeSelf == true)
        {
            fadeImage.gameObject.SetActive(false);

        }

    }

    void Update()
    {
        #region autoFadeOut and Pause
        if (!isFading)
        {
            if (Input.anyKey == false)
            {
                timer += Time.deltaTime;

                if (timer >= 180.0f)
                {
                    fadeImage.gameObject.SetActive(true);
                    StartCoroutine(FadeOutAndLoadMainMenu());
                }
            }
            else
            {
                timer = 0;
            }
        }
        else if (Input.anyKey)
        {
            fadeImage.gameObject.SetActive(false);

            StopAllCoroutines();
            ResetFade();
        }

        //Pausing with P
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause == true)
            {
                PauseTriggered();
            }
            else
            {
                PauseUntriggered();
            }

        }
        #endregion




        if (oSCall == overallState.start)
        {
            oSCall = overallState.onboarding;
            //day and night script does not run
        }
        if(pause == false)
        {
            if (oSCall == overallState.onboarding)
            {
                repaircompleteButton.SetActive(false);  //Remove repair button so onboarding can playout
                refToOnboardingScript.SetTrue();
                if (refToSDCycle.dayCycle == DayNightScript.states.nightSummary)
                {
                    refToOnboardingScript.SetFalse();
                    if (refToNarrativeManagerScript.storyBeat == true) //triggered by narrativemanager
                    {
                        oSCall = overallState.storyPlay;
                    }
                    else
                    {
                        oSCall = overallState.freePlay;
                    }
                }

            }
            else if (oSCall == overallState.freePlay)
            {
                refToOnboardingScript.SetFalse();
                repaircompleteButton.SetActive(true); //main repair button active for repairs
                refToOnboardingScript.enabled = false;//makes sure onboarding isnt interffering
                if (refToSDCycle.dayCycle == DayNightScript.states.dayStart)
                {
                    refToQuestUIScript.freePlayUpdate();
                }
                else if (refToSDCycle.dayCycle == DayNightScript.states.dayWork)
                {
                    ///
                    /// list of things:
                    /// player selects repair
                    /// NPC spwans with repair requested item to drop it off
                    /// small narrative bits
                    ///
                }
                else if (refToSDCycle.dayCycle == DayNightScript.states.dayEnd)
                {
                    ///
                    /// list of things:
                    /// counts repaired items
                    /// lists of rewards from repairs
                    /// Repair Queue is updated and recalculated
                    /// Unfinished repair is dumped
                    /// Narrative update
                    /// Visual update
                }
            }
            else if (oSCall == overallState.storyPlay)//switch from story to freeplay happens in the narrative manager after checking workshopstate and whether dialogue is playing with the dialogue manager
            {
                refToOnboardingScript.SetFalse();
                repaircompleteButton.SetActive(true); //main repair button active for repairs
                refToOnboardingScript.enabled = false; //makes sure onboarding isnt interffering
                if (refToSDCycle.dayCycle == DayNightScript.states.dayStart)
                {

                    //refToQuestUIScript.narrPlayUpdate();
                    ///
                    /// List of things:
                    /// repairqueue
                    /// overlays are off
                    /// check for narrative scripted narrative events
                    /// NPC with already repaired items; their timeline for them returning will be decided
                    /// refurbished from previous night are all done

                }
                else if (refToSDCycle.dayCycle == DayNightScript.states.dayWork)
                {
                    ///
                    /// list of things:
                    /// player selects repair
                    /// NPC spwans with repair requested item to drop it off
                    /// small narrative bits
                    ///
                }
                else if (refToSDCycle.dayCycle == DayNightScript.states.dayEnd)
                {
                    ///
                    /// list of things:
                    /// counts repaired items
                    /// lists of rewards from repairs
                    /// Repair Queue is updated and recalculated
                    /// Unfinished repair is dumped
                    /// Narrative update
                    /// Visual update
                }
            }
        }
        else if(oSCall == overallState.cutscene)
        {
            refToOnboardingScript.enabled = false;//makes sure onboarding isnt interffering

        }

    }
    void PauseTriggered()
    {
        pause = true;
        
        
    }
    void PauseUntriggered()
    {
        pause = false;
    }
    

    public void Onboardinging()
    {
        oSCall = overallState.onboarding;
    }
    
    public void Freeplay()
    {
        oSCall = overallState.freePlay;
    }

    public void Narrative()
    {
        oSCall = overallState.storyPlay;
    }
    IEnumerator FadeOutAndLoadMainMenu()
    {
        isFading = true;

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            // Fade out the main UI image (fadeImage)
            Color lerpedColor = Color.Lerp(Color.clear, Color.black, elapsedTime / fadeDuration);
            fadeImage.color = lerpedColor;

            // Example: Increase alpha and saturation of image2
            Color image2Color = fadeHoneyImage.color;
            image2Color.a += Time.deltaTime * 0.1f; // Adjust the factor based on your preference
            image2Color = Color.Lerp(image2Color, Color.HSVToRGB((Time.time % 1f), 1f, 1f), Time.deltaTime * 0.1f);
            fadeHoneyImage.color = image2Color;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(0);

        isFading = false;
        //isFading = true;

        //float elapsedTime = 0f;
        //while (elapsedTime < fadeDuration)
        //{
        //    Color lerpedColor = Color.Lerp(Color.clear, Color.black, elapsedTime / fadeDuration);
        //    fadeImage.color = lerpedColor;
        //    elapsedTime += Time.deltaTime;
        //    yield return null;
        //}

        //SceneManager.LoadScene(0);

        //isFading = false;
    }

    void ResetFade()
    {
        StopAllCoroutines();
        isFading = false;

        // Reset the color of the main UI image to fully transparent
        Color initialColor = fadeImage.color;
        initialColor.a = 0f;
        fadeImage.color = initialColor;

        // Reset the color of image2 to fully transparent
        Color initialColorImage2 = fadeHoneyImage.color;
        initialColorImage2.a = 0f;
        fadeHoneyImage.color = initialColorImage2;

        timer = 0;
        //StopAllCoroutines();
        //isFading = false;

        //// Reset the color of the Image to fully transparent
        //Color initialColor = fadeImage.color;
        //initialColor.a = 0f;
        //fadeImage.color = initialColor;

        //timer = 0;
    }
}
