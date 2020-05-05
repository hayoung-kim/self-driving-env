using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawnManager : MonoBehaviour
{
    public GameObject TargetPrefab;
    public PathCreation.PathCreator[] Paths;
    public GameObject Env;

    GameObject newAgent;
    PathCreation.PathCreator path;
    List<GameObject> otherAgents = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i =0; i < 2; i++)
        {
            otherAgents.Add(Instantiate(TargetPrefab));
            // (겹치지 않게 차 ID 지정하도록 하도록 수정)
            otherAgents[i].GetComponent<PathFollower>().carID = Random.Range(0, 100);

        }
        otherAgents[0].GetComponent<PathFollower>().pathCreator = Paths[2];
        otherAgents[1].GetComponent<PathFollower>().pathCreator = Paths[3];
    }

    // Update is called once per frame
    int nOtherAgents = 0;
    void FixedUpdate()
    {
        // 마지막 지점 까지 도달한 차량 Destroy + List에서 제거
        nOtherAgents = otherAgents.Count;
        List<bool> doDestroy = new List<bool>();
        for (int i = 0; i < nOtherAgents; i++)
        {
            
            path = otherAgents[i].GetComponent<PathFollower>().pathCreator;
            float distanceTravelled = otherAgents[i].GetComponent<PathFollower>().distanceTravelled;
            if (distanceTravelled >= path.path.length - 0.2f)
            {
                doDestroy.Add(true);
                Destroy(otherAgents[i]);
            }
            else
            {
                doDestroy.Add(false);
            }
        }

        for (int i = 0; i < nOtherAgents; i++)
        {
            if (doDestroy[i])
            {
                otherAgents.RemoveAt(i);
            }
        }
        
        
    }
}
