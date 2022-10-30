using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsManager : MonoBehaviour
{
    ProjectileTrajectory trajectoryCalculator;

    public Vector3 initialVelocity;
    public Vector3 initialPosition;
    public float maxTime;

    public List<GameObject> renderedObjects;

    private void Awake()
    {
        trajectoryCalculator = new ProjectileTrajectory();
    }

    private void FixedUpdate()
    { 
       Vector3[] pathPoints = trajectoryCalculator.CalculateBallisticPathPoints(initialPosition, initialVelocity, Physics.gravity.magnitude, Time.fixedDeltaTime, maxTime);

        RaycastHit hitInfo;

        if (trajectoryCalculator.ProjectileHitCast(pathPoints, out hitInfo, 1))
        {
            Debug.Log("Object " + hitInfo.collider.name + " intersects");
        }
        else Debug.ClearDeveloperConsole();

       for (int i = 1; i < pathPoints.Length; i++)
       {
           Debug.DrawLine(pathPoints[i - 1], pathPoints[i], Color.green);
       }

    }
}
