using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotCollisionScript : MonoBehaviour
{
    public List<GameObject> elementRefurbCollided = new List<GameObject>();
    public bool collided;
    string nameGO;

    private void Start()
    {
        nameGO = gameObject.name;
    }

    private void OnTriggerStay(Collider other)
    {
        collided = true;
        elementRefurbCollided.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
}
