using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : BaseState
{
    public AttackState() { }
    private AIAgentController m_agentController = null;
    public override void OnEnter()
    {
        agent.Mood = Agent.EMood.Angry;
        m_agentController = agent.GetComponent<AIAgentController>();
        Debug.Assert(m_agentController != null, "MISSING AGENT CONTROLLER!");
    }

    // Update is called once per frame
    public override void OnUpdate(float deltaTime)
    {
        float radius = 1.5f;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(m_agentController.m_chaseTarget.position, out hit, radius, NavMesh.AllAreas))
        {
            m_agentController.currentDestination = hit.position;
        }
        else
        {
            //no navmesh found
            m_agentController.currentDestination = Vector3.zero;
            m_agentController.StopAllMovement();
        }
        m_agentController.MoveTowardsDestination();
    }

    public override void OnExit()
    {

    }

    public override StateIDs.States GetName() { return StateIDs.States.SID_Attack; }
}
