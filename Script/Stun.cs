using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : MonoBehaviour
{
    public bool isStunned
    {
        get; private set;
    }
    public float stunTime;

    public void Stuns(float duration)
    {
        isStunned = true;
        stunTime = duration;
        StartCoroutine(StunCoroutine());
    }

    private IEnumerator StunCoroutine()
    {
        yield return new WaitForSeconds(stunTime);
        isStunned = false;
    }

}
