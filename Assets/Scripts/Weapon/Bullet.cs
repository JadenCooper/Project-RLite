using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 startPostion;
    private float conquaredDistance = 0;
    public Rigidbody2D rb2d;
    public BulletData bulletData;
    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        startPostion = transform.position;
        rb2d.velocity = bulletData.Direction * bulletData.Speed;
    }
    private void Update()
    {
        conquaredDistance = Vector2.Distance(transform.position, startPostion);
        if (conquaredDistance > bulletData.MaxDistance)
        {
            DisableObject();
        }
    }

    private void DisableObject()
    {
        rb2d.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hit = collision.gameObject;
        if (hit.layer == gameObject.layer)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), hit.GetComponent<Collider2D>());
            return;
        }
        //Debug.Log(hit.name);
        Health health;
        if (health = hit.GetComponent<Health>())
        {
            health.GetHit(bulletData.damage, gameObject, bulletData.knockbackStrength);
        }
        DisableObject();
    }
}
