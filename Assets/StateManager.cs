using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{

    public List<BaseState> m_states = new List<BaseState>();
    public StateIDs.States m_initialStateID = StateIDs.States.SID_Count;
    public StateIDs.States m_attackStateID = StateIDs.States.SID_Count;

    private BaseState m_currentState = null;

    // Use this for initialization
    void Start()
    {
        Agent agent = GetComponent<Agent>();
        Debug.Assert(agent != null, "AGENT IS NULL, NEED AGENT FOR STATE MANAGER");

        foreach (BaseState state in m_states)
        {
            state.agent = agent;
        }

        ChangeState(m_initialStateID);
        Debug.Log("ChangeState");
    }

    public BaseState FindState(StateIDs.States stateID)
    {
        foreach (BaseState state in m_states)
        {
            if (state.GetName() == stateID)
            {
                return state;
            }
        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_currentState != null)
        {
            m_currentState.OnUpdate(Time.deltaTime);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState(m_attackStateID);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ChangeState(m_initialStateID);
        }

    }

    void ChangeState(StateIDs.States newStateID)
    {
        if (m_currentState != null)
        {
            m_currentState.OnExit();
        }

        m_currentState = FindState(newStateID);
        Debug.Assert(m_currentState != null, "INVALID STATE");
        m_currentState.OnEnter();
    }
}
