using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dronerBehavior : enemyAI
{
    private droneBehavior droneBehavior;

    public override void Start(){
        base.Start();
        droneBehavior = GetComponentInChildren<droneBehavior>();
    }

    public override void Chase()
    {
        IdleUntil(20);
    }
}
