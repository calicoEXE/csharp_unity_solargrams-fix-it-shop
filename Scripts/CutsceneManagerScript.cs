using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutsceneManagerScript : MonoBehaviour
{

    public List<VideoClip> cutsceneList = new List<VideoClip>();
    NarrativeManagerScript refToNarrManScript;
    GameManager refToGM;
    public VideoPlayer refToVideoPlayer;
    int currentVideoIndex;
    void Start()
    {
        refToGM = FindObjectOfType<GameManager>();
        refToNarrManScript = FindObjectOfType<NarrativeManagerScript>();
    }

    void Update()
    {
        if (refToGM.oSCall == GameManager.overallState.start)//intro scene triggered by GameState Start
        {
            currentVideoIndex = 0;
        }
        else if(refToNarrManScript.narrativeState == NarrativeManagerScript.state.prologue)//triggered by narrative state
        {
            currentVideoIndex = 1;
        }
        else if (refToGM.oSCall == GameManager.overallState.cutscene)
        {
            onVideoPlay();
        }
        
    }

    void onVideoPlay()
    {
        refToVideoPlayer.clip = cutsceneList[currentVideoIndex]; //enters the updated 
        refToVideoPlayer.Play();
    }
}
