using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClickInteractionScript : MonoBehaviour
{
    public bool acceptedRequest = false;
    public QuestUIList refToQuestUIList;
    //public QueueSystem refToQueueSystem;
    public int uiID;
    public Image bg;
    public TMP_Text titelText;
    public TMP_Text descText;
    public TMP_Text diffText;
    public enum state { normal, alert};
    public state uiType;

    public Color norBGNeutral;
    public Color norBGHover;
    public Color norBGSelected;

    public Color norTextNeutral;
    public Color norTextSelected;


    public Color alertBGNeutral;
    public Color alertBGHover;
    public Color alertBGSelected;

    public Color alertTextNeutral;
    public Color alertTextSelected;

    /// <summary>
    /// gets the mousedown to sent to QuestUiLIst Script so it can be used
    /// NOTE: if the mouseDown isn't working try moving the camera closer to the GO to see whether it will work
    /// </summary>


    private void Start()
    {  
        //find the script
        refToQuestUIList = GameObject.Find("RepairList").GetComponent<QuestUIList>();
        

    }
    private void Update()
    {
        
        //#region visual feedback
        ////this is just for visual feedback

        //if (uiType == state.normal)
        //{
        //    bg.color = norBGNeutral;
        //    titelText.color = norBGNeutral;

        //}
        //else if (uiType == state.alert)
        //{
        //    bg.color = alertTextNeutral;
        //    titelText.color = alertTextNeutral;
        //}
        //#endregion
    }

    void OnMouseDown()
    {
        
        if (refToQuestUIList.repairState == QuestUIList.repairStates.choose) //checks whether a request has been accepted yet
        {           
            if (acceptedRequest == true)
            {
                
            }
            else
            {
                acceptedRequest = true;
            }
        }
        if (uiType == state.normal) //selected
        {
            bg.color = norBGSelected;
            //titelText.color = norBGSelected;

        }
        else if (uiType == state.alert)
        {
            bg.color = alertBGSelected;
            //titelText.color = alertTextSelected;
        }
    }
    private void OnMouseOver()
    {
        #region visual feedback hover
        //this is just for visual feedback

        if (uiType == state.normal)
        {
            bg.color = norBGHover;
            //titelText.color = norBGNeutral;

        }
        else if (uiType == state.alert)
        {
            bg.color = alertBGHover;
            //titelText.color = alertTextNeutral;
        }
        #endregion 
    }
    private void OnMouseExit()
    {
        #region visual feedback neutral
        //this is just for visual feedback

        if (uiType == state.normal)
        {
            bg.color = norBGNeutral;
            //titelText.color = norBGNeutral;

        }
        else if (uiType == state.alert)
        {
            bg.color = alertTextNeutral;
            //titelText.color = alertTextNeutral;
        }
        #endregion
    }

}
