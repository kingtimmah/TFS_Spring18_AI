using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {

    private static Color[] colors = { Color.blue, Color.green, Color.red, Color.yellow };
    static int BLUE = 0;
    static int GREEN = 1;
    static int RED = 2;
    static int YELLOW = 3;

    public enum EMood
    {
        Neutral = 0,
        Happy,
        Angry,
        Scared,
        Count,
    };

    private EMood m_mood = EMood.Neutral;
    public EMood Mood {
        set { m_mood = value; UpdateColor(); } get { return m_mood; } }

    //meters/second
    public float MaxLinearSpeed = 5.0f;
    private float m_linearSpeed = 0.0f;
    //meters/second
    public float MaxStrafeSpeed = 3.0f;
    private float m_strafeSpeed = 0.0f;
    //radians/second
    public float MaxAngularSpeed = 2.0f;
    private float m_angularSpeed = 0.0f;

    private Rigidbody m_rb = null;

    public void UpdateColor()
    {
        GetComponent<Renderer>().material.color = colors[(int)m_mood];
    }

    public void Move(float speedRatio)
    {
        m_linearSpeed = Mathf.Clamp(speedRatio, -1.0f, 1.0f) * MaxLinearSpeed;
    }

    public void Strafe(float speedRatio)
    {
        m_strafeSpeed = Mathf.Clamp(speedRatio, -1.0f, 1.0f) * MaxStrafeSpeed;
    }


    public void Turn(float speedRatio)
    {
        m_angularSpeed = Mathf.Clamp(speedRatio, -1.0f, 1.0f) * MaxAngularSpeed;
    }

    public void StopMoving()
    {
        m_linearSpeed = 0.0f;
    }

    public void StopStrafing()
    {
        m_strafeSpeed = 0.0f;
    }

    public void StopTurning()
    {
        m_angularSpeed = 0.0f;
    }

    // Use this for initialization
    void Start () {
        m_rb = GetComponent<Rigidbody>();
    }
	

	// Update is called once per frame
	void Update () {
        Vector3 upVelocity = m_rb.velocity.y * transform.up;
        Vector3 linearVelocity = transform.forward;
        linearVelocity.y = 0.0f;
        //flatten the y on linear movement
        linearVelocity *= m_linearSpeed;

        Vector3 strafeVelocity = transform.right;
        strafeVelocity.y = 0.0f;
        strafeVelocity *= m_strafeSpeed;
        Vector3 angularVelocity = transform.up * m_angularSpeed;

        m_rb.velocity = linearVelocity + strafeVelocity + upVelocity;
        m_rb.angularVelocity = angularVelocity;
    }
}
