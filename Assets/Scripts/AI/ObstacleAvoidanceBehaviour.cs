using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidanceBehaviour : SteeringBehaviour
{
    [SerializeField]
    private float raidus = 2f, agentColliderSize = 0.6f;
    [SerializeField]
    private bool showGizmo = true;
    //Gizmo Parameters
    private float[] dangersResultTemp = null;

    public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aIData)
    {
        foreach (Collider2D obstacleCollider in aIData.obstacles)
        {
            Vector2 directionToObstacle = obstacleCollider.ClosestPoint(transform.position) - (Vector2)transform.position;
            float distanceToObstacle = directionToObstacle.magnitude;

            //Calculate Weight Based On The Distance Agent<--->Obstacle
            float weight = distanceToObstacle <= agentColliderSize ? 1 : (raidus - distanceToObstacle) / raidus;
            Vector2 directionToObstacleNormalized = directionToObstacle.normalized;

            // Add Obstacle Parameters To The Danger Array
            for (int i = 0; i < Directions.eightDirections.Count; i++)
            {
                float result = Vector2.Dot(directionToObstacleNormalized, Directions.eightDirections[i]);
                float valueToPutIn = result * weight;

                //Override Value Only If It Is Higher Than The Current One Stored In The Danger Array
                if (valueToPutIn > danger[i])
                {
                    danger[i] = valueToPutIn;
                }
            }
        }
        dangersResultTemp = danger;
        return (danger, interest);
    }

    private void OnDrawGizmos()
    {
        if (showGizmo == false)
        {
            return;
        }
        if (Application.isPlaying && dangersResultTemp != null)
        {
            if (dangersResultTemp != null)
            {
                Gizmos.color = Color.red;
                for (int i = 0; i < dangersResultTemp.Length; i++)
                {
                    Gizmos.DrawRay(transform.position, Directions.eightDirections[i] * dangersResultTemp[i]);
                }
            }
        }
        else
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, raidus);
        }
    }
}
public static class Directions
{
    public static List<Vector2> eightDirections = new List<Vector2>
    {
        new Vector2(0,1).normalized,
        new Vector2(1,1).normalized,
        new Vector2(1,0).normalized,
        new Vector2(1,-1).normalized,
        new Vector2(0,-1).normalized,
        new Vector2(-1,-1).normalized,
        new Vector2(-1,0).normalized,
        new Vector2(-1,1).normalized,
    };
}
