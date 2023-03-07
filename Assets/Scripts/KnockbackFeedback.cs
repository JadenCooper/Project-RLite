using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnockbackFeedback : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2d;

    [SerializeField]
    private float defaultDelay = 0.15f;
    public float Mass = 5;
    public UnityEvent OnBegin, OnDone;
    public void PlayFeedback(GameObject sender, float knockbackStrength)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        knockbackStrength -= Mass;
        if (knockbackStrength > 0)
        {
            // Mass Not High Enough To Stop Knockback
            Vector2 direction = (transform.position - sender.transform.position).normalized;
            rb2d.AddForce(direction * knockbackStrength, ForceMode2D.Impulse);
        }
        StartCoroutine(Reset(defaultDelay));
    }

    private IEnumerator Reset(float delay)
    {
        yield return new WaitForSeconds(delay);
        rb2d.velocity = Vector3.zero;
        OnDone?.Invoke();
    }
}
