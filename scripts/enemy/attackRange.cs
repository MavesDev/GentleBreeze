using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class attackRange : MonoBehaviour
{
    private move playerMove;
    private enemyAI enemyAI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerMove = other.gameObject.GetComponent<move>();
            enemyAI = GetComponentInParent<enemyAI>();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
        playerMove.setAlive(false);
        enemyAI.SetPlayerTransform(null);
        enemyAI.SetState(enemyAI.State.Patrolling);
        Debug.Log("i get called instead");
        }
    }
}
