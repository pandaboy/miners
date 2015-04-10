using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour
{
    // move speed
    public float speed = 1f;

    // where the agent should go to next
    private Vector3 objective;
    public Vector3 Objective
    {
        get { return objective; }
        set { objective = value; }
    }

    void Awake()
    {
        objective = Vector3.zero;
    }
	
	void Update () {
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
        }
    }
}
