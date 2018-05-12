using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgentController : MonoBehaviour {

    private Agent m_agent = null;

    public Transform m_chaseTarget = null;

    public float m_visionFOV = 45.0f;
    public float m_closeDistance = 1.0f;
    public float m_fleeDistance = 5.0f;

    //private Vector3 m_currentDestination = Vector3.zero;
    public Vector3 currentDestination { set; get; }
    //anywhere from the flee distance plus this buffer is considered a safe zone
    //will use a random variable so we don't always stop at a predictable point
    public float m_safebuffer = 2.0f;
    // Use this for initialization
    void Start()
    {
        m_agent = GetComponent<Agent>();
        currentDestination = Vector3.zero;
    }


	//void CheckForThreats()
 //   {
 //       Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, m_threatRadius, LayerMask.GetMask("Player"));
 //       if (hitColliders.Length > 0)
 //       {
 //           m_state = AIAgentState.AS_Threatened;
 //           UpdateColor();
 //           Vector3 threatToAgent = this.transform.position - hitColliders[0].transform.position;
 //           threatToAgent.Normalize();
 //           m_escapeDirection = threatToAgent;
 //       }
 //       else
 //       {
 //           m_state = AIAgentState.AS_Neutral;
 //           UpdateColor();
 //           m_escapeDirection = Vector3.zero;
 //       }
 //   }

    public bool IsTargetWithinFleeRange()
    {
        float distanceToTarget = Vector3.Distance(m_chaseTarget.position, transform.position);
        return distanceToTarget < m_fleeDistance;
    }

    public void MoveTowardsDestination()
    {
        if(currentDestination == Vector3.zero)
        {
            //no destination set, don't go anywhere
            return;
        }

        //only move when we are facing destination
        //speed is determined by distance to destination
        //the closer we are the slower we go
        Vector3 toDestination = currentDestination - transform.position;
        float toDestinationDistance = toDestination.magnitude;
        toDestination.Normalize();
        float angleDegrees = Vector3.Angle(transform.forward, toDestination);
        float rightDotResult = (angleDegrees > m_visionFOV) ? Vector3.Dot(transform.right, toDestination) : 0.0f;
        //turn towards destination
        m_agent.Turn(rightDotResult);

        //almost looking at destination, so start moving
        if(Mathf.Abs(angleDegrees) - m_visionFOV < 5.0f )
        {
            toDestinationDistance = Mathf.Max(m_closeDistance, toDestinationDistance);
            float speed = 1.0f - (m_closeDistance / toDestinationDistance);
            m_agent.Move(speed);
        }

    }

    public void TurnAwayFromTarget()
    {
        Vector3 toTarget = m_chaseTarget.position - transform.position;
        toTarget.Normalize();
        float angleDegrees = Vector3.Angle(transform.forward, toTarget);
        bool isFacingTarget = (angleDegrees < m_visionFOV);
        //turn away from target
        float rightDotResult = isFacingTarget ? Vector3.Dot(transform.right, toTarget) * -1.0f : 0.0f;
        m_agent.Turn(rightDotResult);

    }

    public void MoveAwayFromTarget()
    {
        float distanceToTarget = Vector3.Distance(m_chaseTarget.position, transform.position);
        float safeDistance = m_fleeDistance + Random.Range(0.0f, m_safebuffer);
        float speed = Mathf.Clamp(1.0f - (distanceToTarget / safeDistance), 0.0f, 1.0f);
        m_agent.Move(speed);
    }

    //implement chase behaviour
    public void TurnTowardsTarget()
    {
        //find out if we need to turn towards target
        Vector3 toTarget = m_chaseTarget.position - transform.position;
        toTarget.Normalize();
        float angleDegrees = Vector3.Angle(transform.forward, toTarget);
        float rightDotResult = (angleDegrees > m_visionFOV) ? Vector3.Dot(transform.right, toTarget) : 0.0f;
        m_agent.Turn(rightDotResult);
        
    }

    public void MoveTowardsTarget()
    {
        float distance = Mathf.Clamp(Vector3.Distance(m_chaseTarget.position, transform.position), m_closeDistance, 100.0f);
        float speed = 1.0f - (m_closeDistance / distance);
        m_agent.Move(speed);
    }

    //includes turning
    public void StopAllMovement()
    {
        m_agent.StopMoving();
        m_agent.StopStrafing();
        m_agent.StopTurning();
    }

    // Update is called once per frame
    void Update () {

        if (m_agent == null)
        {
            return;
        }
    }
}
