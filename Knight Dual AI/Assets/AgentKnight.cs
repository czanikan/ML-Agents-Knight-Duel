using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;

public class AgentKnight : Agent
{
    public float moveSpeed = 1f;
    public float rotateAmount = 100f;
    private float moveScale = 1f;

    public int maxHP;
    [HideInInspector]
    public int currHP;

    private Rigidbody rb;

    public override void Initialize()
    {
        rb.GetComponent<Rigidbody>();
        currHP = maxHP;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;

        //forward
        if (Input.GetKey(KeyCode.W))
        {
            discreteActionsOut[0] = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            discreteActionsOut[0] = 2;
        }
        //right
        if (Input.GetKey(KeyCode.D))
        {
            discreteActionsOut[1] = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            discreteActionsOut[1] = 2;
        }
        //rotate
        if (Input.GetKey(KeyCode.Q))
        {
            discreteActionsOut[2] = 1;
        }
        if (Input.GetKey(KeyCode.E))
        {
            discreteActionsOut[2] = 2;
        }
        
    }



    public override void OnActionReceived(ActionBuffers actions)
    {
        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;

        var forwardAxis = actions.DiscreteActions[0];
        var rightAxis = actions.DiscreteActions[1];
        var rotateAxis = actions.DiscreteActions[2];

        switch (forwardAxis)
        {
            case 1:
                dirToGo = transform.forward * moveScale;
                break;
            case 2:
                dirToGo = transform.forward * -moveScale;
                break;
        }

        switch (rightAxis)
        {
            case 1:
                dirToGo = transform.right * moveScale;
                break;
            case 2:
                dirToGo = transform.right * -moveScale;
                break;
        }

        switch (rotateAxis)
        {
            case 1:
                rotateDir = transform.up * -1f;
                break;
            case 2:
                rotateDir = transform.up * 1f;
                break;
        }

        transform.Rotate(rotateDir, Time.deltaTime * rotateAmount);
        rb.AddForce(dirToGo * moveSpeed, ForceMode.VelocityChange);
    }

    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
    }


}
