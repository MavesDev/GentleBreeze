using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class enemyAI : MonoBehaviour
{

    public enum State
    {
        Patrolling,
        Alert,
        Idle
    }
    public State currentState = State.Patrolling;

    //patrolling
    public Transform[] walkPoints;
    public int targetPoint;
    public float speed = 4f;

    public float StartWaitTime;
    private float waitTime;
    bool increasing = true;


    //alert
    public Transform playerTransform;

    public virtual void Start()
    {
        // rb = GetComponent<Rigidbody2D>();
        targetPoint = 0;
        waitTime = StartWaitTime;
    }


    // Update is called once per frame
    public virtual void Update()
    {
        switch (currentState)
        {
            case State.Patrolling:
                Patrolling();
                break;
            case State.Alert:
                Chase();
                break;
        }
        // radar.transform.position = transform.position;
    }

    public void SetPlayerTransform(Transform player)
    {
        playerTransform = player;
    }

    public void SetState(State NewState)
    {
        currentState = NewState;
    }

    public void IdleNext(State NewState)
    {
        StartCoroutine(waiter(NewState));
    }

    IEnumerator waiter(State NewState)
    {
        currentState = State.Idle;
        yield return new WaitForSeconds(3);
        if (currentState != State.Alert)
        {
            currentState = NewState;
        }
    }

    public virtual void Chase()
    {
        if (playerTransform != null)
        {

            // confusing rotation shit
            // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // Quaternion targetRotation = Quaternion.Euler(new Vector3(0,0,angle));
            // transform.rotation = Quaternion.Slerp(transform.rotation, playerTransform.rotation, 5f * Time.deltaTime);

            Vector2 direction = (playerTransform.position - transform.position).normalized;
            transform.position += (Vector3)(direction * (speed * 2) * Time.deltaTime);
        }
    }

    public virtual void Patrolling()
    {
        if (Vector2.Distance(transform.position, walkPoints[targetPoint].position) < 0.2f)
        {
            IdleNext(State.Patrolling);
            increaseTargetInt();
        }
        transform.position = Vector2.MoveTowards(transform.position, walkPoints[targetPoint].position, speed * Time.deltaTime);
    }

    public virtual void IdleUntil(float seconds)
    {
        StartCoroutine(waitUntil(seconds));
    }

    IEnumerator waitUntil(float seconds)
    {
        currentState = State.Idle;
        yield return new WaitForSeconds(seconds);
        if (currentState != State.Alert)
        {
            currentState = State.Patrolling;
            yield break;
        }
    }

    void increaseTargetInt()
    {
        if (increasing)
        {
            if (targetPoint == walkPoints.Length - 1)
            {
                Debug.Log("i get called");
                increasing = false;
                targetPoint--;
            }
            targetPoint++;
        }
        else if (!increasing)
        {
            if (targetPoint == 0)
            {
                increasing = true;
                targetPoint++;
            }
            targetPoint--;
        }
        Debug.LogFormat("targetpoint {0} walkpointslength {1}", targetPoint, walkPoints.Length);
    }
}
