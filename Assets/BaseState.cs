using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public abstract class BaseState : MonoBehaviour {

    public BaseState() { }

    public abstract void OnEnter();
    public abstract void OnUpdate(float deltaTime);
    public abstract void OnExit();

    public abstract StateIDs.States GetName();

    public Agent agent { set; get; }
}
