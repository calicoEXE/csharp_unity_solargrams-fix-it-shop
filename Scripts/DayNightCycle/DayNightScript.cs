#region old code
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayNightScript : MonoBehaviour
{
    /// <summary>
    /// Game time: 10 hours total from 9:00 - 19:00
    /// 
    /// Real Time: 10 min total
    ///     8 minutes day
    ///     2 minutes night
    ///     
    /// 
    ///  1 min RT = 60 min GT
    /// </summary>
    /// 


    public float secondsInRealMinute = 60f; //1sec GT = 60 sec RT
    public float minutesInGameHour = 60f; // 1 hour GT = 1 min RT
    public float hoursInGameDay = 10f; // 24f
    public Light sun;
    public TMP_Text timeText;

    private float timeOfDay = 9f; //0f
    private int currentDay = 1;

    public float TimeOfDay
    {
        get { return timeOfDay; } 
    }

    float gameDaystart = 9;
    float gameDayend = 19;
    

    public enum states { dayStart, dayWork, dayEnd, nightSummary};
    public states dayCycle;
    private void Update()
    {
       TimeCycle();

        if (timeOfDay >= 9f && timeOfDay < 19f)
        {
            dayCycle = states.dayWork;
        }
        else if (timeOfDay >= 0f && timeOfDay < 9f)
        {
            dayCycle = states.dayStart;
        }
        else if (timeOfDay >= 19f && timeOfDay < 24f)
        {
            dayCycle = states.dayEnd;
        }
        else
        {
            dayCycle = states.nightSummary;
        }

        // QueueSystem and SpawnManager checks for dayStart before initiating spawn + repairQ
    }
    void TimeCycle()
    {
        // Calculate the time that has passed in real-time
        float timePassed = Time.deltaTime;

        // Calculate how much in-game time should pass based on real-time
        float inGameMinutesPassed = timePassed / secondsInRealMinute * minutesInGameHour;

        // Update the time of day based on the in-game time passed
        timeOfDay += inGameMinutesPassed / 60f;

        // Ensure timeOfDay stays within a 24-hour cycle
        if (timeOfDay >= hoursInGameDay)
        {
            timeOfDay -= hoursInGameDay;
            currentDay++;
        }

        // Update the directional light's rotation to simulate the sun/moon movement
        float angle = (timeOfDay / hoursInGameDay) * 360f;
        sun.transform.rotation = Quaternion.Euler(angle, 0, 0);

        // Convert timeOfDay to hours and minutes
        int currentHour = Mathf.FloorToInt(timeOfDay);
        int currentMinute = Mathf.FloorToInt((timeOfDay - currentHour) * 60);

        // Update the TMP_Text to display the day count and time in atomic clock format
        timeText.text = string.Format("Day {0:D}, {1:D2}:{2:D2}", currentDay, currentHour, currentMinute);
    }
}*/

#endregion 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DayNightScript : MonoBehaviour
{
    /// <summary>
    /// Game time: 10 hours total from 9:00 - 19:00
    /// 
    /// Real Time: 10 min total
    ///     8 minutes day
    ///     2 minutes night
    ///     
    /// 
    ///  1 min RT = 60 min GT
    /// </summary>
    /// 

    const float hoursPerDay = 24f;

    public float secondsInRealMinute = 60f; //1sec GT = 60 sec RT
    public float minutesInGameHour = 60f; // 1 hour GT = 1 min RT
    public float hoursInGameDay = 24f;//10f; // 24f
    public Light sun;
    public TMP_Text timeText;

    public float timeOfDay = 9f; //0f
    public int currentDay = 1;

    public enum states { dayStart, dayWork, dayEnd, nightSummary };
    public states dayCycle;

    public GameObject refToTownUI;
    public RepairMechanicManagerClass refToRepairMechManagerClass;

    public GameManager refToGM;
    public QuestUIList refToQuestUIListScript;
    public MainMenuActionScript mainmenuScript;


    public GameObject controls;

    public Image fadeImage;
    public Image fadeHoneyImage;
    public float fadeDuration = 10f;
    public float blinkDuration = 1.0f;


    public float TimeOfDay
    {
        get { return timeOfDay; }
    }
    private void Start()
    {
        refToGM = FindObjectOfType<GameManager>();
        //mainmenuScript = FindObjectOfType<MainMenuActionScript>();
        refToRepairMechManagerClass = FindObjectOfType<RepairMechanicManagerClass>();
    }
    public void Update()
    {
        if (currentDay==2)
        {
            controls.SetActive(false);
        }

        if (currentDay==3)
        {
            mainmenuScript.GameEnd();   
        }

        if (timeOfDay >= 18.85f)
        {
            fadeImage.gameObject.SetActive(true);
            StartCoroutine(FadeOutAndLoadMainMenu());
        }

        if (timeOfDay >=18.6f)
        {
            StartCoroutine(BlinkText());
        }


        if (refToGM.oSCall == GameManager.overallState.freePlay || refToGM.oSCall == GameManager.overallState.storyPlay || refToGM.oSCall == GameManager.overallState.onboarding)
        {
            if(refToGM.pause == false)
            {
                if (dayCycle != states.nightSummary)//time doesn't continue if it reaches nightsummary
                {
                    TimeCycle();
                    refToTownUI.SetActive(false);

                }
            }
            
            #region changes dayStates
            if (timeOfDay == 9.000f)
            {
                dayCycle = states.dayStart;
            }
            if (dayCycle == states.dayStart)
            {
                
                if (timeOfDay > 9)// && hoursInGameDay < 19)
                {
                    dayCycle = states.dayWork;
                }
            }
            else if (dayCycle == states.dayWork)
            {
                if (timeOfDay >= 19)
                {
                    dayCycle = states.dayEnd;
                }
            }
            else if (dayCycle == states.dayEnd)
            {
                if (timeOfDay >= 22)
                {
                    dayCycle = states.nightSummary;
                }
            }
            if (dayCycle == states.nightSummary)
            {
                refToTownUI.SetActive(true);

                //refToQuestUIListScript.LogUnselectedNPCs();  ////no longer using

                if (Input.GetKeyDown(KeyCode.R))//resets time on the player in put
                {
                    Debug.Log("R pressed");
                    timeOfDay = 9;
                    //resets both repairs counts
                    refToRepairMechManagerClass.successfulRepairs = 0;
                    refToRepairMechManagerClass.failedRepairs = 0;
                    refToRepairMechManagerClass.incompleteRepairs = 0;
                    dayCycle = states.dayStart;
                    currentDay++;
                }
            }
            #endregion
        }


    }
    void TimeCycle()
    {
        // Calculate the time that has passed in real-time
        float timePassed = Time.deltaTime;

        // Calculate how much in-game time should pass based on real-time
        float inGameMinutesPassed = timePassed / secondsInRealMinute * minutesInGameHour;

        // Update the time of day based on the in-game time passed
        timeOfDay += inGameMinutesPassed / 60f;

        // Ensure timeOfDay stays within a 24-hour cycle
        if (timeOfDay >= hoursInGameDay)
        {
            timeOfDay -= hoursInGameDay;
        }

        // Update the directional light's rotation to simulate the sun/moon movement
        float angle = (timeOfDay / hoursInGameDay) * 360f;
        sun.transform.rotation = Quaternion.Euler(angle, 0, 0);

        // Convert timeOfDay to hours and minutes
        int currentHour = Mathf.FloorToInt(timeOfDay);
        int currentMinute = Mathf.FloorToInt((timeOfDay - currentHour) * 60);

        // Update the TMP_Text to display the day count and time in atomic clock format
        timeText.text = string.Format("Day {0:D} {1:D2}:{2:D2}", currentDay, currentHour, currentMinute);
    }
    public void onResetTownUIButton()
    {
        
        timeOfDay = 9;
        //resets both repairs counts
        refToRepairMechManagerClass.successfulRepairs = 0;
        refToRepairMechManagerClass.failedRepairs = 0;
        refToRepairMechManagerClass.incompleteRepairs = 0;
        dayCycle = states.dayStart;
        currentDay++;
        
    }

    IEnumerator FadeOutAndLoadMainMenu()
    {
       // isFading = true;

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

       // SceneManager.LoadScene(0);

       // isFading = false;
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

    IEnumerator BlinkText()
    {
        while (true) // Continue blinking indefinitely
        {
            float t = 0;

            // Increase t from 0 to 1 over the specified duration
            while (t < 1.0f)
            {
                t += Time.deltaTime / blinkDuration;

                // Use Mathf.PingPong to smoothly interpolate between Color.clear and Color.red
                timeText.color = Color.Lerp(Color.clear, Color.red, Mathf.PingPong(t, 1.0f));

                yield return null;
            }

            yield return null;
        }
    }


}
