using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIData : MonoBehaviour
{
    public List<Transform> targets = null;
    public Collider2D[] obstacles = null;

    public Transform currentTarget;

    // If targets Null return 0 else return targets.count
    public int GetTargetsCount() => targets == null ? 0 : targets.Count;
}
