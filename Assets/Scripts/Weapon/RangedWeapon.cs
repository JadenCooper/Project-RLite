using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public GameObject Bullet;
    public Transform barrel;
    public int MaxAmmo = 5;
    public int currentClip = 5;
    public bool isReloading = false;
    public float ReloadDelay = 2f;
    private void Start()
    {
        currentClip = MaxAmmo;
    }
    public override void Attack()
    {
        if (attackBlock || isReloading)
        {
            return;
        }
        //animator.SetTrigger("Attack");
        Shoot();
        IsAttacking = true;
        attackBlock = true;
        StartCoroutine(DelayAttack());
    }
    public override void OnEnable()
    {
        attackBlock = false;
        IsAttacking = false;
        isReloading = false;
        currentClip = MaxAmmo;
    }
    public void Shoot()
    {
        GameObject newBullet = Instantiate(Bullet, barrel.position, barrel.rotation);
        newBullet.layer = gameObject.layer;
        Bullet bullet = newBullet.GetComponent<Bullet>();
        bullet.damage = damage;
        bullet.Direction = direciton;
        currentClip--;
        if (currentClip <= 0)
        {
            isReloading = true;
            StartCoroutine(Reloading());
        }
        //AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        //audioSource.PlayOneShot(audioSource.clip);
    }
    public IEnumerator Reloading()
    {
        Debug.Log("Reloading");
        yield return new WaitForSeconds(ReloadDelay);
        isReloading = false;
        currentClip = MaxAmmo;
    }
}
