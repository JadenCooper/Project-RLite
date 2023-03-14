using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : Detector
{
    [SerializeField]
    private float targetDetectionRange = 5;

    [SerializeField]
    private LayerMask obstacleLayerMask, playerLayerMask;
    public List<Collider2D> Targets = new List<Collider2D>();
    [SerializeField]
    private bool showGizmos = false;

    //gizmo parameters
    private List<Transform> colliders;
    public override void Detect(AIData aIData)
    {
        //Find out if player is near
        Targets.Clear();
        Targets.AddRange(Physics2D.OverlapCircleAll(transform.position, targetDetectionRange, playerLayerMask));

        Collider2D playerCollider = null;
        float closestDistanceSqr = Mathf.Infinity;
        foreach (Collider2D target in Targets)
        {
            if (target == null)
            {
                Targets.Remove(target);
                break;
            }
            if (target.gameObject.tag == "Agent")
            {
                Vector3 directionToTarget = target.gameObject.transform.position - transform.position;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    playerCollider = target;
                }
            }
        }
        if (playerCollider != null)
        {
            // Check if you see the player
            Vector2 direction = (playerCollider.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, targetDetectionRange, obstacleLayerMask);

            // Make sure that the collider we see is on  Player Layer
            if (hit.collider != null && (playerLayerMask & (1 << hit.collider.gameObject.layer)) != 0)
            {
                Debug.DrawRay(transform.position, direction * targetDetectionRange, Color.magenta);
                colliders = new List<Transform>() { playerCollider.transform };
            }
            else
            {
                colliders = null;
            }
        }
        else
        {
            // Enemy doesent see player
            colliders = null;
        }
        aIData.targets = colliders;
    }

    private void OnDrawGizmosSelected()
    {
        if (showGizmos == false || colliders == null)
        {
            return;
        }
        Gizmos.color = Color.magenta;
        foreach (var item in colliders)
        {
            Gizmos.DrawSphere(item.position, 0.5f);
        }
    }
}
