using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    // Start is called before the first frame update
    PathFollower pathFollower;
    void Start()
    {
        pathFollower = this.GetComponent<PathFollower>();
    }


    // Update is called once per frame
    public float value = 0;
    public int steps = 0;
    void Update()
    {
        value = Input.GetAxis("Vertical");
        pathFollower.speed += value;

        steps += 1;
        if (pathFollower.distanceTravelled <= 0.1)
        {
            steps = 0;
        }

        if (this.transform.position.z <= -23)
        {
            print(steps);
        }
    }

}
