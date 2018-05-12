using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleeState : BaseState
{
    public FleeState() { }
    private AIAgentController m_agentController = null;
    //this is when we check for target distance
    private float m_reactionTimer = 0.1f;
    private float m_currentReactionTime = 0.0f;

    public override void OnEnter()
    {
        agent.Mood = Agent.EMood.Scared;
        m_agentController = agent.GetComponent<AIAgentController>();
        Debug.Assert(m_agentController != null, "MISSING AGENT CONTROLLER!");
    }

    private void CalculateSafeFleeDestination()
    {
        Vector3 toTarget = m_agentController.m_chaseTarget.position - agent.transform.position;
        toTarget.Normalize();
        float safeDistance = m_agentController.m_fleeDistance + Random.Range(0.0f, m_agentController.m_safebuffer);
        Vector3 awayFromTarget = toTarget * -1.0f * safeDistance;
        Vector3 desiredDestination = agent.transform.position + awayFromTarget;
        float radius = 1.5f;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(desiredDestination, out hit, radius, NavMesh.AllAreas))
        {
            m_agentController.currentDestination = hit.position;
        }
        else
        {
            //no navmesh found
            m_agentController.currentDestination = Vector3.zero;
        }

    }

    // Update is called once per frame
    public override void OnUpdate(float deltaTime)
    {
        //only flee if target is in flee range
        //flee away from target
        //stop when in safe distance
        if(m_currentReactionTime < m_reactionTimer)
        {
            m_currentReactionTime += Time.deltaTime;
        }
        else
        {
            m_currentReactionTime = 0.0f;
            if(m_agentController.IsTargetWithinFleeRange())
            {
                CalculateSafeFleeDestination();
            }
            else
            {
                m_agentController.currentDestination = Vector3.zero;
                m_agentController.StopAllMovement();
            }
        }

        m_agentController.MoveTowardsDestination();

    }

    public override void OnExit()
    {

    }

    public override StateIDs.States GetName() { return StateIDs.States.SID_Flee; }
}
