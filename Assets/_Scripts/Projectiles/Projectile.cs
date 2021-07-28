using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public delegate void ProjectileEvents();
    public static event ProjectileEvents OnProjectileHit;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnProjectileHit.Invoke();
        }

        anim.SetTrigger("explode");
    }

    //Destroy projectile when it goes outside screen boundaries
    private void OnBecameInvisible()
    {
        DestroyProjectile();
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

}
