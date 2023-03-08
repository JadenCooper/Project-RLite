using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public Transform barrel;
    public bool isReloading = false;
    public int currentClip;
    public RangedStats rangedStats;
    private void Awake()
    {
        SetBulletStats();
    }
    private void Start()
    {
        currentClip = rangedStats.MaxAmmo;
    }
    public void SetBulletStats()
    {
        rangedStats.bulletData.damage = rangedStats.damage;
        rangedStats.bulletData.knockbackStrength = rangedStats.knockbackStrength;
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
        currentClip = rangedStats.MaxAmmo;
    }
    public void Shoot()
    {
        GameObject newBullet = Instantiate(rangedStats.Bullet, barrel.position, barrel.rotation);
        newBullet.layer = gameObject.layer;
        Bullet bullet = newBullet.GetComponent<Bullet>();
        rangedStats.bulletData.Direction = direciton;
        bullet.bulletData = rangedStats.bulletData;
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
        yield return new WaitForSeconds(rangedStats.ReloadDelay);
        isReloading = false;
        currentClip = rangedStats.MaxAmmo;
    }

    public override IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(rangedStats.AttackDelay);
        attackBlock = false;
    }
}
