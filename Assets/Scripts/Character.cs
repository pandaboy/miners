using UnityEngine;
using System.Collections;
using FSM;

public class Character : MonoBehaviour
{
    // Agent script
    public Agent agent;

    // move speed
    public float speed = 1f;

    private bool in_motion = false;
    public bool InMotion
    {
        get { return in_motion; }
        set { in_motion = value; }
    }

    // where the agent should go to next
    private Vector3 objective;
    public Vector3 Objective
    {
        get { return objective; }
        set { objective = value; }
    }

    // Use this for initialization
    void Awake()
    {
        objective = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // update the Finite State Machine
        // when the character isn't moving.
        if (!InMotion)
        {
            agent.Update();
        }
    }

    void FixedUpdate()
    {
        // move the character to the target.
        MoveTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        Vector3 current = transform.position;

        if (Vector3.Distance(current, objective) > .1f)
        {
            Vector3 direction = objective - current;
            direction.Normalize();

            float st = speed * Time.deltaTime;
            transform.Translate(
                (direction.x * st),
                (direction.y * st),
                (direction.z * st),
            Space.World);

            in_motion = true;
        }
        else
        {
            in_motion = false;
        }
    }
}
