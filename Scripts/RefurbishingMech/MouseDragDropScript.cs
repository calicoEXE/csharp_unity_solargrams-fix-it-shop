using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDragDropScript : MonoBehaviour
{
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
