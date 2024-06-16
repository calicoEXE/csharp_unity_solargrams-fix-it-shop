using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.MaterialProperty;

public class WireScript : MonoBehaviour
{
    public GameObject object1;  // The first GameObject to connect.
    public GameObject object2;  // The second GameObject to connect.
    public LineRenderer lineRenderer;  // Reference to the LineRenderer component.

    //public List<GameObject> wireCollision = new List<GameObject>();
    public List<ElementDataScript> wireCollision = new List<ElementDataScript>();
    public List<string> wireName = new List<string>(); //gets the enum of the object
    public bool wireCheck;
    bool wireCheckA;
    bool wireCheckB;

    public string destinationTag = "DropArea";


    //public ElementDataScript refToElementDataScript;
    private void Start()
    {
        // Make sure the LineRenderer component is attached.
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer component is missing!");
            return;
        }

        // Set the positions of the LineRenderer to the initial positions of the GameObjects.
        UpdateLineRenderer();
    }

    void Update()
    {

        // Update the LineRenderer positions.
        UpdateLineRenderer();

        //looks whether the enum is in it and whether the go are usable
        //the checks are broken into to so that in the end wirecheck can be done

        #region wirecollisioncheck
       // if (wireCollision != null)
       // {
       //     if (wireCheckA == false)
       //     {
       //         foreach (ElementDataScript go in wireCollision)
       //         {
       //             if (go.elementType == droptype.work && go.usable == true)
       //             {
       //                 wireCheckA = true;
       //             }
       //             else
       //             {
       //                 wireCheckA = false;
       //             }
       //
       //         }
       //     }
       //     if (wireCheckB == false)
       //     {
       //         foreach (ElementDataScript go in wireCollision)
       //         {
       //             if (go.elementType == droptype.drive && go.usable == true)
       //             {
       //                 wireCheckB = true;
       //             }
       //             else
       //             {
       //                 wireCheckB = false;
       //             }
       //
       //         }
       //     }
       //
       //
       //     if (wireCheckA == true && wireCheckB == true)
       //     {
       //         wireCheck = true;
       //     }
       //     else
       //     {
       //         wireCheck = false;
       //     }
       // }
       // else
       // {
       //
       // }

        #endregion wirecollisioncheck


    }
    private void UpdateLineRenderer()
    {
        // Update the positions of the LineRenderer.
        lineRenderer.SetPosition(0, object1.transform.position);
        lineRenderer.SetPosition(1, object2.transform.position);
    }
    void OnCollisionEnter(Collision collision)
    {
      //  wireCollision.Add(collision.collider.gameObject.GetComponent<ElementDataScript>());
      //  wireName.Add(collision.collider.gameObject.GetComponent<ElementDataScript>().elementType.ToString());
    }
    void OnCollisionExit(Collision collision)
    {
       // wireCollision.Remove(collision.collider.gameObject.GetComponent<ElementDataScript>());
        // wireName.Remove(collision.collider.gameObject.GetComponent<ElementDataScript>().elementType.ToString());
    }

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
}
