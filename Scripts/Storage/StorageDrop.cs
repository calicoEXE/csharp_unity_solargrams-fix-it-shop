using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageDrop : MonoBehaviour
{

    //  public AudioManagerScript audioManager;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {


        //   audioManager.WrongItemPlaced();

        // Check if the colliding object has a Rigidbody
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Get the Y position of the colliding object
            float originalY = collision.transform.position.y;

            // Snap the colliding object to the position of this GameObject while keeping the original Y position
            rb.position = new Vector3(transform.position.x, originalY, transform.position.z);

            rb.velocity = Vector3.zero; // Optionally reset the velocity to zero
            rb.angularVelocity = Vector3.zero; // Optionally reset the angular velocity to zero
        }
    }
}
