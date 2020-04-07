﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CarState
{
    public int playerIndex; 
    public Rigidbody agentRB; 
    public Vector3 startingPos; 
    public AgentCars agentScript; 
    public float ballPosReward;
}

public class CarsFieldArea : MonoBehaviour
{
    public Transform redGoal;
    public Transform blueGoal;
    public AgentCars redStriker;
    public AgentCars blueStriker;
    public AgentCars redGoalie;
    public AgentCars blueGoalie;
    public GameObject ball;
    [HideInInspector]
    public Rigidbody ballRB;
    public GameObject ground; 
    public GameObject centerPitch;
    CarsBallController ballController;
    public List<CarState> playerStates = new List<CarState>();
    [HideInInspector]
    public Vector3 ballStartingPos;
    public bool drawSpawnAreaGizmo;
    Vector3 spawnAreaSize;
    public float goalScoreByTeamReward;
    public float goalScoreAgainstTeamReward;
    public GameObject goalTextUI;
    public float totalPlayers;
    [HideInInspector]
    public bool canResetBall;
    public bool useSpawnPoint;
    public Transform spawnPoint;
    Material groundMaterial;
    Renderer groundRenderer;
    CarsAcademy academy;
    public float blueBallPosReward;
    public float redBallPosReward;

    public IEnumerator GoalScoredSwapGroundMaterial(Material mat, float time)
    {
        groundRenderer.material = mat;
        yield return new WaitForSeconds(time); 
        groundRenderer.material = groundMaterial;
    }


    void Awake()
    {
        academy = FindObjectOfType<CarsAcademy>();
        groundRenderer = centerPitch.GetComponent<Renderer>(); 
        groundMaterial = groundRenderer.material;
        canResetBall = true;
        if (goalTextUI) { goalTextUI.SetActive(false); }
        ballRB = ball.GetComponent<Rigidbody>();
        ballController = ball.GetComponent<CarsBallController>();
        ballController.area = this;
        ballStartingPos = ball.transform.position;
        Mesh mesh = ground.GetComponent<MeshFilter>().mesh;
    }

    IEnumerator ShowGoalUI()
    {
        if (goalTextUI) goalTextUI.SetActive(true);
        yield return new WaitForSeconds(.25f);
        if (goalTextUI) goalTextUI.SetActive(false);
    }

    public void AllPlayersDone(float reward)
    {
        foreach (CarState ps in playerStates)
        {
            if (ps.agentScript.gameObject.activeInHierarchy)
            {
                if (reward != 0)
                {
                    ps.agentScript.AddReward(reward);
                }
                ps.agentScript.Done();
            }

        }
    }

    public void GoalTouched(AgentCars.Team scoredTeam)
    {
        foreach (CarState ps in playerStates)
        {
            if (ps.agentScript.team == scoredTeam)
            {
                RewardOrPunishPlayer(ps, academy.strikerReward, academy.goalieReward);
            }
            else
            {
                RewardOrPunishPlayer(ps, academy.strikerPunish, academy.goaliePunish);
            }
            if (academy.randomizePlayersTeamForTraining)
            {
                ps.agentScript.ChooseRandomTeam();
            }

            if (scoredTeam == AgentCars.Team.Red)
            {
                StartCoroutine(GoalScoredSwapGroundMaterial(academy.redMaterial, 1));
            }
            else
            {
                StartCoroutine(GoalScoredSwapGroundMaterial(academy.blueMaterial, 1));
            }
            if (goalTextUI)
            {
                StartCoroutine(ShowGoalUI());
            }
        }
    }

    public void RewardOrPunishPlayer(CarState ps, float striker, float goalie)
    {
        if (ps.agentScript.agentRole == AgentCars.AgentRole.Striker)
        {
            ps.agentScript.AddReward(striker);
        }
        if (ps.agentScript.agentRole == AgentCars.AgentRole.Goalie)
        {
            ps.agentScript.AddReward(goalie);
        }
        ps.agentScript.Done();  //all agents need to be reset
    }


    public Vector3 GetRandomSpawnPos(AgentCars.AgentRole role, AgentCars.Team team)
    {
        float xOffset = 0f;
        if (role == AgentCars.AgentRole.Goalie)
        {
            xOffset = 13f;
        }
        if (role == AgentCars.AgentRole.Striker)
        {
            xOffset = 7f;
        }
        if (team == AgentCars.Team.Blue)
        {
            xOffset = xOffset * -1f;
        }
        var randomSpawnPos = ground.transform.position + 
                               new Vector3(xOffset, 0f, 0f) 
                               + (Random.insideUnitSphere * 2);
        randomSpawnPos.y = ground.transform.position.y + 2;
        return randomSpawnPos;
    }

    public Vector3 GetBallSpawnPosition()
    {
        var randomSpawnPos = ground.transform.position + 
                             new Vector3(0f, 0f, 0f) 
                             + (Random.insideUnitSphere * 2);
        randomSpawnPos.y = ground.transform.position.y + 2;
        return randomSpawnPos;
    }

    void SpawnObjAtPos(GameObject obj, Vector3 pos)
    {
        obj.transform.position = pos;
    }

    public void ResetBall()
    {
        ball.transform.position = GetBallSpawnPosition();
        ballRB.velocity = Vector3.zero;
        ballRB.angularVelocity = Vector3.zero;
    }
}
