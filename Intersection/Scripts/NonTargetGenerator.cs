using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class NonTargetGenerator : MonoBehaviour
{
    // a. non-agent 관련
    public GameObject TargetPrefab;
    private float vehicleLength;

    // b. path 관련
    public PathCreator[] Paths;

    /////////////////// Wast ///////////////////
    // a. non-agent 관련
    private GameObject[] nonAgent_W;
    private int targetMaxNum_W = 5;

    public float genProbability_W = 0.002f;
    private bool gen_W = true;

    private int lastSpawn_W;
    private int count_W;

    // b. path 관련
    private PathCreator[] Paths_W;
    ////////////////////////////////////////////

    /////////////////// East ///////////////////
    // a. non-agent 관련
    private GameObject[] nonAgent_E;
    private int targetMaxNum_E = 5;

    public float genProbability_E = 0.002f;
    private bool gen_E = true;

    private int lastSpawn_E;
    private int count_E;

    // b. path 관련
    private PathCreator[] Paths_E;
    ////////////////////////////////////////////



    // Start is called before the first frame update
    void Start()
    {
        // a. non-agent 관련
        nonAgent_W = new GameObject[targetMaxNum_W];
        nonAgent_E = new GameObject[targetMaxNum_E];

        vehicleLength = TargetPrefab.transform.localScale[2];

        // b. path 관련
        int Paths_Wnum = 0;
        int Paths_Enum = 0;
        for (int i = 0; i < Paths.Length; i++)
        {
            if (Paths[i].name[0].Equals('W'))
            {
                Paths_Wnum++;
            }
            else if (Paths[i].name[0].Equals('E'))
            {
                Paths_Enum++;
            }
        }

        Paths_W = new PathCreator[Paths_Wnum];
        Paths_E = new PathCreator[Paths_Enum];

        int j_W = 0;
        int j_E = 0;
        for (int i = 0; i < Paths.Length; i++)
        {
            if (Paths[i].name[0].Equals('W'))
            {
                Paths_W[j_W] = Paths[i];
                j_W++;
            }
            else if (Paths[i].name[0].Equals('E'))
            {
                Paths_E[j_E] = Paths[i];
                j_E++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        //////////////////////////////////////////// WEST
        // 타겟 갯수만큼 생성 후, 마지막에 가면 ㅃ
        for (int i = 0; i < targetMaxNum_W; i++)
        {
            if (nonAgent_W[i] == null)
            {
                if (gen_W && Random.value < genProbability_W)
                {
                    nonAgent_W[i] = Instantiate(TargetPrefab);
                    int random_path_idx = Random.Range(0, Paths_W.Length);
                    nonAgent_W[i].GetComponent<PathFollower>().pathCreator = Paths_W[random_path_idx];

                    lastSpawn_W = i;
                    gen_W = false;

                    count_W++;

                    Debug.Log(count_W);
                }
            }
            else
            {
                float distanceTravelled = nonAgent_W[i].GetComponent<PathFollower>().distanceTravelled;

                if (i == lastSpawn_W && distanceTravelled > vehicleLength * 2)
                {
                    gen_W = true;
                }

                if (distanceTravelled >= nonAgent_W[i].GetComponent<PathFollower>().pathCreator.path.length - 0.2f)
                {
                    Destroy(nonAgent_W[i]);
                    count_W--;
                }
            }

        }

        //////////////////////////////////////////// EAST
        // 타겟 갯수만큼 생성 후, 마지막에 가면 ㅃ
        for (int i = 0; i < targetMaxNum_E; i++)
        {
            if (nonAgent_E[i] == null)
            {
                if (gen_E && Random.value < genProbability_E)
                {
                    nonAgent_E[i] = Instantiate(TargetPrefab);
                    int random_path_idx = Random.Range(0, Paths_E.Length);
                    nonAgent_E[i].GetComponent<PathFollower>().pathCreator = Paths_E[random_path_idx];

                    lastSpawn_E = i;
                    gen_E = false;

                    count_E++;
                }
            }
            else
            {
                float distanceTravelled = nonAgent_E[i].GetComponent<PathFollower>().distanceTravelled;

                if (i == lastSpawn_E && distanceTravelled > vehicleLength * 2)
                {
                    gen_E = true;
                }

                if (distanceTravelled >= nonAgent_E[i].GetComponent<PathFollower>().pathCreator.path.length - 0.2f)
                {
                    Destroy(nonAgent_E[i]);
                    count_E--;
                }
            }
        }
    }
}


