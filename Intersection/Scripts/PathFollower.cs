using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathFollower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 3;
    public float distanceTravelled;

    // Update is called once per frame
    void Update()
    {
        speed = Mathf.Clamp(speed, 0f, 4f);
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
    }
}
