using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class IntersectionAgent : Agent
{
    PathFollower pf;
    FieldOfView sensor;
    CollisionChecker cc;

    public Transform goalPosition;
    private bool done_ = false;
    void Start()
    {
        pf = GetComponent<PathFollower>();
        sensor = GetComponent<FieldOfView>();
        cc = GetComponent<CollisionChecker>();
    }

    
    public override void AgentReset()
    {
        pf.distanceTravelled = Random.value * 3.0f;
        pf.speed = 0f;
        cc.collide = false;
        done_ = false;
    }

    public GameObject[] otherCars;
    public override void CollectObservations()
    {
        // agent position and velocity
        AddVectorObs(this.transform.position.x);
        AddVectorObs(this.transform.position.z);
        AddVectorObs(Mathf.Cos(this.transform.rotation.y));  // heading 
        AddVectorObs(Mathf.Sin(this.transform.rotation.y));  // heading 
        AddVectorObs(pf.speed);

        // obstacles
        for (int i=0; i<2; i++)
        {
            AddVectorObs(otherCars[i].transform.position.x);
            AddVectorObs(otherCars[i].transform.position.z);
            AddVectorObs(Mathf.Cos(otherCars[i].transform.rotation.y));  // heading 
            AddVectorObs(Mathf.Sin(otherCars[i].transform.rotation.y));  // heading 
            AddVectorObs(otherCars[i].GetComponent<PathFollower>().speed);
        }


    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        // Actions, size = 1
        pf.speed += vectorAction[0];
        
        // (2) Collision reward
        if (cc.collide)
        {
            if (!done_)
            {
                // 이게 있어야 한번만 들어옴
                AddReward(-1.0f);
            }

            Done();
            done_ = true;
        }

        // (3) Reached target reward (goal)
        if (this.transform.position.z - goalPosition.transform.position.z <= 0)
        {
            if (!done_) // 있어야 한번만 들어옴
            {
                AddReward(1.0f);
            }

            Done();
            done_ = true;
        }

        // (1) efficiency
        AddReward(-0.001f);


    }
}
