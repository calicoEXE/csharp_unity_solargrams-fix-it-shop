using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
//using UnityEditor.PackageManager;
using UnityEngine;

public class RepairElementsCollisionClass : MonoBehaviour
{
    /// <summary>
    /// this script will be on all of the elemnts except for the CARRIER element
    /// </summary>


    public string destinationTag = "DropArea";
    
    public 

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
       // transform.GetComponent<Collider>().enabled = true;
        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseWorldPosition() - Camera.main.transform.position;
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo))
        {
            if (hitInfo.transform.tag == destinationTag)
            {
                Debug.Log("destination found");
                transform.position = hitInfo.transform.position;
                
                //hitInfo.transform.gameObject.GetComponent<DropAreaManager>().DesiredElement = element;

            }
        }

        transform.GetComponent<Collider>().enabled = true;
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
    #endregion
    


}
