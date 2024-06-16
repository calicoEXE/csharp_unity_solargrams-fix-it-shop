using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireSupportScript : MonoBehaviour
{
    public WireScript refToWireClass;

    public string destinationTag = "DropArea";


    #region mouse drag and drop
    void OnMouseDown()
    {
        // transform.GetComponent<Collider>().enabled = false;
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

                transform.position = hitInfo.transform.position;

                //hitInfo.transform.gameObject.GetComponent<DropAreaManager>().DesiredElement = element;

            }
        }

        //transform.GetComponent<Collider>().enabled = true;
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
    #endregion
    //adds in the objects that the support wire element is colliding with
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.gameObject.name);
        refToWireClass.wireCollision.Add(collision.collider.gameObject.GetComponent<ElementDataScript>());
       // refToWireClass.wireName.Add(collision.collider.gameObject.GetComponent<ElementDataScript>().elementType.ToString());
    }
    public void OnCollisionExit(Collision collision)
    {
        //adds element to list of carrier
        refToWireClass.wireCollision.Remove(collision.collider.gameObject.GetComponent<ElementDataScript>());
      //  refToWireClass.wireName.Remove(collision.collider.gameObject.GetComponent<ElementDataScript>().elementType.ToString());

    }

}
