using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class detectPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private enemyAI enemyAI;
    public float speed;
    private Vector2 originalScale;

    void Start()
    {
        enemyAI = GetComponentInParent<enemyAI>();
        originalScale = new Vector2(transform.localScale.x, transform.localScale.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            transform.localScale = new Vector2(transform.localScale.x * 3, transform.localScale.y * 3);
            enemyAI.SetPlayerTransform(other.transform);
            enemyAI.SetState(enemyAI.State.Alert);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemyAI.SetPlayerTransform(null);
            transform.localScale = originalScale;
            enemyAI.IdleNext(enemyAI.State.Patrolling);
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
}
