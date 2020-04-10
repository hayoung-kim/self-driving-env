using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    public bool collide = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "otherCars")
        {
            collide = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "otherCars")
        {
            collide = false;
        }
    }
}
