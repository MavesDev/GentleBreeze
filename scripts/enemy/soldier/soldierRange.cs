using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class soldierRange : attackRange
{
    // bullet
    public GameObject Bullet;
    private Transform parentTransform;
    public float fireRate = 1f; // shots per second
    private float nexTimeToFire = 3f; // next time until next shot
    public Transform player;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            parentTransform = GetComponentInParent<Transform>();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(Time.time >= nexTimeToFire){
                nexTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
    }

    public void Shoot()
    {
        // Vector2 direction = (player.position - transform.position).normalized;
        Vector2 direction = player.position;
        Instantiate(Bullet, transform.position, quaternion.identity);
        Bullet.GetComponent<bulletBehavior>().SetDirection(direction);
    }
}
