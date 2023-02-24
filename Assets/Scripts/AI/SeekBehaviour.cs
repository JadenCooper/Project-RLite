using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeekBehaviour : SteeringBehaviour
{
    [SerializeField]
    private float targetReachedThreshold = 0.5f;
    [SerializeField]
    private bool showGizmo = true;
    private bool reachedLastTarget = true;
    //Gizmo Parameters
    private Vector2 targetPositionCached;
    private float[] interestsTemp;
    public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aIData)
    {
        // If We Don't Have Target Stop Seeking
        // Else Set A New Target
        if (reachedLastTarget)
        {
            if (aIData.targets == null || aIData.targets.Count <= 0)
            {
                aIData.currentTarget = null;
                return (danger, interest);
            }
            else
            {
                reachedLastTarget = false;
                aIData.currentTarget = aIData.targets.OrderBy(target => Vector2.Distance(target.position, transform.position)).FirstOrDefault();
            }
        }

        // Cache The Last Position Only If We Still See The Target (If The Targets Collection Is Not Empty)
        if (aIData.currentTarget != null && aIData.targets != null && aIData.targets.Contains(aIData.currentTarget))
        {
            targetPositionCached = aIData.currentTarget.position;
        }

        // First Check If We Have Reached The Target
        if (Vector2.Distance(transform.position, targetPositionCached) < targetReachedThreshold)
        {
            reachedLastTarget = true;
            aIData.currentTarget = null;
            return (danger, interest);
        }

        // If We Haven't Yet Reached The Target Do The Main Logic Of Finding The Interest Directions
        Vector2 directionToTarget = (targetPositionCached - (Vector2)transform.position);
        for (int i = 0; i < interest.Length; i++)
        {
            float result = Vector2.Dot(directionToTarget.normalized, Directions.eightDirections[i]);

            // Accept Only Directions At The Less Than 90 Degrees To The Target Direction
            if (result > 0)
            {
                float valueToPutIn = result;
                if (valueToPutIn > interest[i])
                {
                    interest[i] = valueToPutIn;
                }
            }
        }
        interestsTemp = interest;
        return (danger, interest);
    }

    private void OnDrawGizmos()
    {
        if (showGizmo == false)
        {
            return;
        }
        Gizmos.DrawSphere(targetPositionCached, 0.2f);

        if (Application.isPlaying && interestsTemp != null)
        {
            if (interestsTemp != null)
            {
                Gizmos.color = Color.green;
                for (int i = 0; i < interestsTemp.Length; i++)
                {
                    Gizmos.DrawRay(transform.position, Directions.eightDirections[i] * interestsTemp[i]);
                }
            }
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(targetPositionCached, 0.1f);
        }
    }
}
