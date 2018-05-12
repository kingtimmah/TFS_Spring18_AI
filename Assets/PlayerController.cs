using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Agent m_agent = null;

	// Use this for initialization
	void Start () {
        m_agent = GetComponent<Agent>();
    }
	

    // Update is called once per frame
    void Update () {

        if(m_agent == null)
        {
            return;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticallInput = Input.GetAxis("Vertical");

        float turnInput = 0.0f;
        if(Input.GetKey(KeyCode.E))
        {
            turnInput += 1.0f;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            turnInput += -1.0f;
        }

        m_agent.Turn(turnInput);
        m_agent.Move(verticallInput);
        m_agent.Strafe(horizontalInput);

        

        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //}
    }
}
