using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIMk2 : MonoBehaviour
{
    [SerializeField]
    private List<Detector> detectors;
    [SerializeField]
    private AIData aiData;
    [SerializeField]
    private float detectionDelay = 0.05f;
    void Start()
    {
        // Detecting Player And Obstacles Around
        InvokeRepeating("PerformDetection", 0, detectionDelay);
    }

    private void PerformDetection()
    {
        foreach (Detector detector in detectors)
        {
            detector.Detect(aiData);
        }
    }
}
