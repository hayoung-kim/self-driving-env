using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TTCColliderTrigger : MonoBehaviour
{
    public int targetID = -1;
    public string targetTag = " ";
    GameObject parent;
    public GameObject targetGameObject;
    int currentCarID;

    private void Start()
    {
        // print(this.tag);
        parent = this.transform.parent.gameObject;
        currentCarID = parent.GetComponent<PathFollower>().carID;
    }

    private void OnTriggerEnter(Collider other)
    {
        int otherID;

        try
        {
            if (other.transform.parent.tag == "otherCars")
            {
                otherID = other.GetComponentInParent<PathFollower>().carID;
                if (otherID != currentCarID)
                {
                    targetID = otherID;
                    targetTag = "otherCars";
                    targetGameObject = other.transform.parent.gameObject;
                    print(targetID);
                }
                else
                {
                    targetID = -1;
                    targetTag = " ";
                    targetGameObject = null;
                }
            }
        }
        catch (NullReferenceException e)
        {
            if (other.tag == "otherCars")
            {
                otherID = other.GetComponent<PathFollower>().carID;
                if (otherID != currentCarID)
                {
                    targetID = otherID;
                    targetTag = "otherCars";
                    targetGameObject = other.gameObject;
                    // print(targetID);
                }
                else
                {
                    targetID = -1;
                    targetTag = " ";
                    targetGameObject = null;
                }
            }
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        targetID = -1;
        targetTag = " ";
        targetGameObject = null;
    }
}

