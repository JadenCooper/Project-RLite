using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 20;
    public float MaxDistance = 10;
    private Vector2 startPostion;
    private float conquaredDistance = 0;
    public Rigidbody2D rb2d;
    public Vector2 Direction;
    public int damage = 1;

    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        startPostion = transform.position;
        rb2d.velocity = Direction * Speed;
    }
    private void Update()
    {
        conquaredDistance = Vector2.Distance(transform.position, startPostion);
        if (conquaredDistance > MaxDistance)
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
            Debug.Log(health);
            health.GetHit(damage, gameObject);
        }
        DisableObject();
    }
}
