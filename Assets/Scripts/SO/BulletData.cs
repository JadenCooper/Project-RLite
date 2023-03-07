using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewBulletData", menuName = "Data/BulletData")]
public class BulletData : ScriptableObject
{
    public Vector2 Direction;
    public int damage = 1;
    public float knockbackStrength = 1;
    public float Speed = 20;
    public float MaxDistance = 10;
}
