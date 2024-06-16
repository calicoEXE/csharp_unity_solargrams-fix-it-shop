using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager;
using UnityEngine;

public class RefurbishingMechanicScript : MonoBehaviour
{
    List<GameObject> slotsGOList = new List<GameObject>();
    float timer;

    
    void Update()
    {
        foreach(var slot in slotsGOList) //will go through all of the slots
        {
            if (slot.GetComponent<SlotCollisionScript>().collided == true) //if collision is true and mouse button is up
            {
                if (Input.GetKeyUp(KeyCode.Alpha0))
                {
                    timer++;
                    if(timer >= 1440f) //in 60fps  == 4 min in real time
                    {
                        //resets everything
                        /////////////////////////////////////////////Initiliase changes for items or elements here NOTE:SKY
                        timer = 0;
                        Debug.Log(slot.ToString());
                        slot.GetComponent<SlotCollisionScript>().elementRefurbCollided = null;
                        slot.GetComponent<SlotCollisionScript>().collided = false;
                    }
                }
            }
        }
    }
    

    #region mouse drag and drop
    void OnMouseDown()
    {
        transform.GetComponent<Collider>().enabled = false;
    }

    void OnMouseDrag()
    {
        transform.position = MouseWorldPosition(); //+ offset;
    }

    void OnMouseUp()
    {
        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseWorldPosition() - Camera.main.transform.position;
        RaycastHit hitInfo;
        //use raycast info to detect whether player dragged element over open 
        if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo))
        {
            if (hitInfo.transform.tag == "RefurbishingPlace")
            {
                transform.position = hitInfo.transform.position;
            }
            //transform.GetComponent<Collider>().enabled = true;
        }
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
    #endregion
}
