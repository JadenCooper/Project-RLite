using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public Transform barrel;
    public bool isReloading = false;
    public int currentClip;
    public RangedStats rangedStats;
    [SerializeField]
    private ObjectPool bulletPool;
    [SerializeField]
    private int bulletPoolCount = 5;
    private void Awake()
    {
        SetBulletStats();
        bulletPool = GetComponent<ObjectPool>();
    }
    private void Start()
    {
        bulletPool.Initialize(rangedStats.Bullet, bulletPoolCount);
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
        //GameObject newBullet = Instantiate(rangedStats.Bullet, barrel.position, barrel.rotation);
        GameObject newBullet = bulletPool.CreateObject();
        newBullet.transform.position = barrel.position;
        newBullet.transform.rotation = barrel.rotation;
        newBullet.layer = gameObject.layer;
        newBullet.GetComponent<Bullet>().Initialize(rangedStats.bulletData, direciton);
        //Bullet bullet = newBullet.GetComponent<Bullet>();
        //rangedStats.bulletData.Direction = direciton;
        //bullet.bulletData = rangedStats.bulletData;
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
        yield return new WaitForSeconds(rangedStats.ReloadDelay);
        isReloading = false;
        currentClip = rangedStats.MaxAmmo;
    }

    public override IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(rangedStats.AttackDelay);
        attackBlock = false;
    }
    public override void Reload()
    {
        if (isReloading == false)
        {
            Debug.Log("Reload");
            isReloading = true;
            StartCoroutine(Reloading());
        }
    }
}
