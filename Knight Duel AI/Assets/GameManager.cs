using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;

public class GameManager : MonoBehaviour
{
    public List<AgentKnight> Agents = new List<AgentKnight>();

    public int MaxEnvironmentSteps = 25000;
    private int resetTimer = 0;

    private void Start()
    {
        ResetGame();
    }

    private void ResetGame()
    {
        foreach (Agent agent in Agents)
        {
            agent.transform.position = agent.GetComponent<AgentKnight>().startPos;
            agent.GetComponent<Rigidbody>().velocity = Vector3.zero;
            agent.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            agent.GetComponent<AgentKnight>().currHP = agent.GetComponent<AgentKnight>().maxHP;
            agent.GetComponent<AgentKnight>().UpdateUI();
        }

        resetTimer = 0;
    }

    void FixedUpdate()
    {
        resetTimer += 1;
        if (resetTimer >= MaxEnvironmentSteps && MaxEnvironmentSteps > 0)
        {
            Agents[0].EpisodeInterrupted();
            Agents[1].EpisodeInterrupted();
            ResetGame();
        }
    }

    public void RoundOver(string deadKnightName)
    {
        if (Agents[0].gameObject.name != deadKnightName)
        {
            Agents[0].SetReward(2f);
            Agents[1].SetReward(-2f);

            Debug.Log(Agents[0].gameObject.name + " won the round!");
        }

        if (Agents[1].gameObject.name != deadKnightName)
        {
            Agents[1].SetReward(2f);
            Agents[0].SetReward(-2f);

            Debug.Log(Agents[1].gameObject.name + " won the round!");
        }

        Agents[0].EndEpisode();
        Agents[1].EndEpisode();
        ResetGame();
    }
}
