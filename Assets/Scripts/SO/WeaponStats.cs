using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponStats", menuName = "Data/WeaponStatData")]
public class WeaponStats : ScriptableObject
{
    public float AttackDelay = 0.3f;
    public float knockbackStrength = 5;
    public int damage;
}
