using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState {

    public IdleState() { }

    public override void OnEnter()
    {
        agent.Mood = Agent.EMood.Neutral;
    }

    // Update is called once per frame
    public override void OnUpdate(float deltaTime)
    {
        //have whatever logic we need
    }

    public override void OnExit()
    {

    }

    public override StateIDs.States GetName() { return StateIDs.States.SID_Idle; }

    
}
