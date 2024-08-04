using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{

    [Header("Stun Setting")]
    [SerializeField] bool canStun;
    [SerializeField] float stunCooldown = 5f;
    [SerializeField] float stunRadius = 1f;
    [SerializeField] float stunDuration = 2f;

    [Header("Copy Skill Setting")]
    [SerializeField] float copyRadius = 2f;
    [SerializeField] float copyDuration = 3f;
    [SerializeField] float copyCooldown = 5f;
    private bool isCopying = false;
    private bool canCopy = true;

    private Coroutine copyCoroutine = null;
    private Collider2D targetEnemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R) && canStun)
        {
            StunEnemies();
            StartCoroutine(StunCooldown());
        }

        if (Input.GetKeyDown(KeyCode.E) && !isCopying && canCopy)
        {
            CopySkill();
            //StartCoroutine(CopyCooldown());   
        } 
        else if (Input.GetKeyDown(KeyCode.E) && isCopying && canCopy)
        {
            CancelCopy();
            StartCoroutine(CopyCooldown());
        }

        if (isCopying && targetEnemy != null)
        {
            if (Vector2.Distance(transform.position, targetEnemy.transform.position) > copyRadius)
            {
                CancelCopy();
                Debug.Log("Copy skill canceled: enemy out of range.");
            }
        }
    }

    void CopySkill()
    {
         Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, copyRadius);
         foreach (Collider2D enemy in hitEnemies)
         {
             if (enemy.CompareTag("EnemyWithStun"))
             {
                if(canStun == true)
                {
                    Debug.Log("Skill Already Copied");
                }
                else
                {
                isCopying = true;
                targetEnemy = enemy;
                Debug.Log("Copying Skill...");

                copyCoroutine = StartCoroutine(CopyDuration());
                }
                return;
                }
            }
        Debug.Log("No Suitable Enemy");
    }

    private IEnumerator CopyDuration()
    {
        yield return new WaitForSeconds(copyDuration);
        if (isCopying && targetEnemy != null && Vector2.Distance(transform.position, targetEnemy.transform.position) <= copyRadius)
        {
            canStun = true;
            //hasCopied = true;
            isCopying = false;
            StartCoroutine(CopyCooldown());
            Debug.Log("Skill copied successfully!");
        }
        else
        {
            CancelCopy();
        }
    }

    private void CancelCopy()
    {
        if(copyCoroutine != null)
        {
            StopCoroutine(copyCoroutine);
        }
        isCopying = false;
        targetEnemy = null;
        Debug.Log("Copy Cancelled");
    }

    private IEnumerator StunCooldown()
    {
        canStun = false;
        yield return new WaitForSeconds(stunCooldown);
        canStun = true;
    }


    private IEnumerator CopyCooldown()
    {
        canCopy = false;
        yield return new WaitForSeconds(copyCooldown);
        canCopy = true;
        Debug.Log("Copy skill cooldown finished.");
    }

    void StunEnemies()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, stunRadius);
        foreach (Collider2D enemy in hitEnemies)
        {
            Stun stun = enemy.GetComponent<Stun>();
            if (stun != null)
            {
                stun.Stuns(stunDuration);
            }

        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stunRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, copyRadius);
    }
}
