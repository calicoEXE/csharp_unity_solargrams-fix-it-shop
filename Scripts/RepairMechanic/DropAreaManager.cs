using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum droptype { battery, motor, transmission, wire1, wire2, wire3, wire4, carrier, control, droparea, work, drive, storage };

public class DropAreaManager : MonoBehaviour
{
    
    public droptype dropType;
    public CarrierElementClass carrierElementClass;
    public AudioManagerScript audioManagerScript;   

   // public AudioSource itemAudioSource;
   // public AudioClip itemPlaced;
    


    // Start is called before the first frame update
    void Start()
    {
        audioManagerScript = FindObjectOfType<AudioManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    private void OnTriggerEnter(Collider collision)
    {
        ElementDataScript elementData = collision.gameObject.GetComponent<ElementDataScript>();

        //Debug.Log("TriggerDrop Works!");

        if (elementData != null)
        {
            //Debug.Log("Element: " + elementData.elementType.ToString() + ", DropSlot: " + this.gameObject.GetComponent<DropAreaManager>().dropType);

           



            if (this.gameObject.GetComponent<DropAreaManager>().dropType == droptype.battery && elementData.usable == true)
            {
                
                if (elementData.elementType == droptype.battery)
                {
                    
                    // TODO: Add code for when the collision involves a battery
                    carrierElementClass.carrierList.Add(elementData);
                    carrierElementClass.carrierNameList.Add(elementData.elementType.ToString());
                    carrierElementClass.batteryCheck = true;

                    //Debug.Log("This is a battery");
                }
            }
            else if (this.gameObject.GetComponent<DropAreaManager>().dropType == droptype.motor && elementData.usable == true)
            {
                if (elementData.elementType == droptype.motor)
                {
                    // TODO: Add code for when the collision involves a motor
                    carrierElementClass.carrierList.Add(elementData);
                    carrierElementClass.carrierNameList.Add(elementData.elementType.ToString());
                    //Debug.Log("This is a motor");
                    carrierElementClass.motorCheck = true;
                }
            }
            else if (this.gameObject.GetComponent<DropAreaManager>().dropType == droptype.transmission && elementData.usable == true)
            {
                if (elementData.elementType == droptype.transmission)
                {
                    // TODO: Add code for when the collision involves a transmission
                    carrierElementClass.carrierList.Add(elementData);
                    carrierElementClass.carrierNameList.Add(elementData.elementType.ToString());
                    //Debug.Log("This is a transmission");
                    carrierElementClass.transmissionCheck = true;
                }
            }
            else if (this.gameObject.GetComponent<DropAreaManager>().dropType == droptype.wire1)
            {
                if (elementData.elementType == droptype.wire1)
                {
                    // TODO: Add code for when the collision involves a wire
                    carrierElementClass.carrierList.Add(elementData);
                    carrierElementClass.carrierNameList.Add(elementData.elementType.ToString());
                    //Debug.Log("This is a wire");
                    carrierElementClass.transmissionCheck = true; // Note: There was a potential typo in your original code (transmissionCheck instead of wireCheck)
                }
            } 
            else if (this.gameObject.GetComponent<DropAreaManager>().dropType == droptype.wire2)
            {
                if (elementData.elementType == droptype.wire2)
                {
                    // TODO: Add code for when the collision involves a wire
                    carrierElementClass.carrierList.Add(elementData);
                    carrierElementClass.carrierNameList.Add(elementData.elementType.ToString());
                    //Debug.Log("This is a wire");
                    carrierElementClass.transmissionCheck = true; // Note: There was a potential typo in your original code (transmissionCheck instead of wireCheck)
                }
            } 
            else if (this.gameObject.GetComponent<DropAreaManager>().dropType == droptype.wire3)
            {
                if (elementData.elementType == droptype.wire3)
                {
                    // TODO: Add code for when the collision involves a wire
                    carrierElementClass.carrierList.Add(elementData);
                    carrierElementClass.carrierNameList.Add(elementData.elementType.ToString());
                    //Debug.Log("This is a wire");
                    carrierElementClass.transmissionCheck = true; // Note: There was a potential typo in your original code (transmissionCheck instead of wireCheck)
                }
            }   
            else if (this.gameObject.GetComponent<DropAreaManager>().dropType == droptype.wire4)
            {
                if (elementData.elementType == droptype.wire4)
                {
                    // TODO: Add code for when the collision involves a wire
                    carrierElementClass.carrierList.Add(elementData);
                    carrierElementClass.carrierNameList.Add(elementData.elementType.ToString());
                    //Debug.Log("This is a wire");
                    carrierElementClass.transmissionCheck = true; // Note: There was a potential typo in your original code (transmissionCheck instead of wireCheck)
                }
            }

            if (elementData.usable)
            {
                audioManagerScript.ItemPlacedMachine();

            }

            if (!elementData.usable)
            {
                audioManagerScript.WrongItemPlaced();

            }

        }
    }





    //  private void OnCollisionEnter(Collision collision)
    //  {
    //
    //
    //      Debug.Log("Element: " + collision.collider.gameObject.GetComponent<ElementDataScript>().elementType.ToString() + ", DropSlot: " + this.gameObject.GetComponent<DropAreaManager>().dropType);
    //
    //      if(dropType == droptype.battery)
    //      {
    //          if (collision.collider.gameObject.GetComponent<ElementDataScript>().elementType == droptype.battery)
    //          {
    //              carrierElementClass.carrierList.Add(collision.collider.gameObject.GetComponent<ElementDataScript>());
    //              carrierElementClass.carrierNameList.Add(collision.collider.GetComponent<ElementDataScript>().elementType.ToString());
    //              carrierElementClass.batteryCheck = true;
    //
    //              Debug.Log("This bitch batteries");
    //          }
    //      }
    //      else if(dropType == droptype.motor)
    //      {
    //          if ((int)collision.collider.gameObject.GetComponent<ElementDataScript>().elementType == (int)droptype.motor)
    //          {
    //
    //              carrierElementClass.carrierList.Add(collision.collider.gameObject.GetComponent<ElementDataScript>());
    //              carrierElementClass.carrierNameList.Add(collision.collider.GetComponent<ElementDataScript>().elementType.ToString());
    //              Debug.Log("This bitch motors");
    //              carrierElementClass.motorCheck= true;
    //          }
    //      }
    //    
    //      else if(dropType == droptype.transmission)
    //      {
    //          if ((int)collision.collider.gameObject.GetComponent<ElementDataScript>().elementType == (int)droptype.transmission)
    //          {
    //              carrierElementClass.carrierList.Add(collision.collider.gameObject.GetComponent<ElementDataScript>());
    //              carrierElementClass.carrierNameList.Add(collision.collider.GetComponent<ElementDataScript>().elementType.ToString());
    //              Debug.Log("This bitch transmissions");
    //              carrierElementClass.transmissionCheck= true;
    //          }
    //      }
    //      else if (dropType == droptype.wire)
    //      {
    //          if ((int)collision.collider.gameObject.GetComponent<ElementDataScript>().elementType == (int)droptype.wire)
    //          {
    //              carrierElementClass.carrierList.Add(collision.collider.gameObject.GetComponent<ElementDataScript>());
    //              carrierElementClass.carrierNameList.Add(collision.collider.GetComponent<ElementDataScript>().elementType.ToString());
    //              Debug.Log("This bitch wires");
    //              carrierElementClass.transmissionCheck = true;
    //          }
    //      }
    //
    //
    //
    //  }




    private void OnTriggerExit(Collider collision)
    {
        ElementDataScript elementData = collision.gameObject.GetComponent<ElementDataScript>();

        if (elementData != null)
        {
            if (elementData.elementType == droptype.battery && elementData.usable==true)
            {
                // TODO: Add code for when a battery exits the trigger zone
                carrierElementClass.carrierList.Remove(elementData);
                carrierElementClass.carrierNameList.Remove(elementData.elementType.ToString());
            }

            if (elementData.elementType == droptype.motor)
            {
                // TODO: Add code for when a motor exits the trigger zone
                carrierElementClass.carrierList.Remove(elementData);
                carrierElementClass.carrierNameList.Remove(elementData.elementType.ToString());
            }

            if (elementData.elementType == droptype.transmission)
            {
                // TODO: Add code for when a transmission exits the trigger zone
                carrierElementClass.carrierList.Remove(elementData);
                carrierElementClass.carrierNameList.Remove(elementData.elementType.ToString());
            }

            if (dropType == droptype.wire1 && elementData.elementType == droptype.wire1)
            {
                // TODO: Add code for when a wire exits the trigger zone
                carrierElementClass.carrierList.Remove(elementData);
                carrierElementClass.carrierNameList.Remove(elementData.elementType.ToString());
                carrierElementClass.transmissionCheck = true; // Note: There was a potential typo in your original code (transmissionCheck instead of wireCheck)
            }
            if (dropType == droptype.wire2 && elementData.elementType == droptype.wire2)
            {
                // TODO: Add code for when a wire exits the trigger zone
                carrierElementClass.carrierList.Remove(elementData);
                carrierElementClass.carrierNameList.Remove(elementData.elementType.ToString());
                carrierElementClass.transmissionCheck = true; // Note: There was a potential typo in your original code (transmissionCheck instead of wireCheck)
            }
            if (dropType == droptype.wire3 && elementData.elementType == droptype.wire3)
            {
                // TODO: Add code for when a wire exits the trigger zone
                carrierElementClass.carrierList.Remove(elementData);
                carrierElementClass.carrierNameList.Remove(elementData.elementType.ToString());
                carrierElementClass.transmissionCheck = true; // Note: There was a potential typo in your original code (transmissionCheck instead of wireCheck)
            }
            if (dropType == droptype.wire4 && elementData.elementType == droptype.wire4)
            {
                // TODO: Add code for when a wire exits the trigger zone
                carrierElementClass.carrierList.Remove(elementData);
                carrierElementClass.carrierNameList.Remove(elementData.elementType.ToString());
                carrierElementClass.transmissionCheck = true; // Note: There was a potential typo in your original code (transmissionCheck instead of wireCheck)
            }
        }
    }



  // private void OnCollisionExit(Collision collision)
  // {
  //
  //
  //     //  Debug.Log(collision.collider.gameObject.GetComponent<ElementDataScript>().elementType.ToString() + droptype.battery);
  //
  //     if ((int)collision.collider.gameObject.GetComponent<ElementDataScript>().elementType == (int)droptype.battery)
  //
  //     {
  //         carrierElementClass.carrierList.Remove(collision.collider.gameObject.GetComponent<ElementDataScript>());
  //         carrierElementClass.carrierNameList.Remove(collision.collider.GetComponent<ElementDataScript>().elementType.ToString());
  //
  //         
  //     }
  //
  //     if ((int)collision.collider.gameObject.GetComponent<ElementDataScript>().elementType == (int)droptype.motor)
  //     {
  //
  //         carrierElementClass.carrierList.Remove(collision.collider.gameObject.GetComponent<ElementDataScript>());
  //         carrierElementClass.carrierNameList.Remove(collision.collider.GetComponent<ElementDataScript>().elementType.ToString());
  //         
  //     }
  //
  //     if ((int)collision.collider.gameObject.GetComponent<ElementDataScript>().elementType == (int)droptype.transmission)
  //     {
  //         carrierElementClass.carrierList.Remove(collision.collider.gameObject.GetComponent<ElementDataScript>());
  //         carrierElementClass.carrierNameList.Remove(collision.collider.GetComponent<ElementDataScript>().elementType.ToString());
  //         
  //     }
  //     if (dropType == droptype.wire)
  //     {
  //         if ((int)collision.collider.gameObject.GetComponent<ElementDataScript>().elementType == (int)droptype.wire)
  //         {
  //             carrierElementClass.carrierList.Remove(collision.collider.gameObject.GetComponent<ElementDataScript>());
  //             carrierElementClass.carrierNameList.Remove(collision.collider.GetComponent<ElementDataScript>().elementType.ToString());
  //             
  //             carrierElementClass.transmissionCheck = true;
  //         }
  //     }
  //
  // }

}
