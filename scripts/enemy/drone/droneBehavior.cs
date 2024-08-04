using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class droneBehavior : enemyAI
{
    public Transform parentTransform;
    public float droneSpeed = 35f;

    public override void Update()
    {
        base.Update();
        if (currentState == State.Alert)
        {
            GetComponentInParent<dronerBehavior>().SetState(State.Alert);
        }
    }

    public override void Patrolling()
    {
        // transform.position = Vector2.MoveTowards(transform.position, parentTransform.position, speed * Time.deltaTime);
        // GetComponentInParent<dronerBehavior>().SetState(State.Patrolling);
        transform.RotateAround(parentTransform.position, Vector3.forward, droneSpeed * Time.deltaTime);
    }
}
