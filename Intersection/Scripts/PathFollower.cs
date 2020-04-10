using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathFollower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 3;
    public float distanceTravelled;
    public float speedLimit = 9;
    public float dt = 0.1f;

    // Update is called once per frame
    public int steps = 0;
    void FixedUpdate()
    {
        speed = Mathf.Clamp(speed, 0, speedLimit);
        distanceTravelled += speed * dt;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);

    }
}
