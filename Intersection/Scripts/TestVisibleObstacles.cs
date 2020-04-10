using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVisibleObstacles : MonoBehaviour
{
    FieldOfView sensor;

    void Start()
    {
        sensor = GetComponent<FieldOfView>();
    }

    // Update is called once per frame
    int nVisibleObjects = 0;
    void Update()
    {
        nVisibleObjects = sensor.visibleTargets.Count;
        if (nVisibleObjects > 0)
        {
            for (int i=0; i < nVisibleObjects; i++)
            {
                print(sensor.visibleTargets[i].name);
                print(sensor.visibleTargets[i].transform.position);
            }
            print("----------------");
        }
    }
}
