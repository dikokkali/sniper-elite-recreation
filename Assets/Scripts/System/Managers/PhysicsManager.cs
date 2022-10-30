using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsManager : MonoBehaviour
{
    private ProjectileTrajectory trajectoryCalculator;

    public Vector3 initialVelocity;
    public Vector3 initialPosition;
    public float maxTime;

    public bool showWeaponTrajectory;
    public float gravityAcceleration;
    public float physicsTimeStep;

    public List<GameObject> renderedObjects;

    private void Awake()
    {
        trajectoryCalculator = new ProjectileTrajectory();

        InitializePhysicsContext();
    }

    private void FixedUpdate()
    { 
        if (showWeaponTrajectory)
        {
            DrawTrajectory();
        }
    }

    private void InitializePhysicsContext()
    {
        gravityAcceleration = Physics.gravity.magnitude;
        physicsTimeStep = Time.fixedDeltaTime;
    }

    public void DrawTrajectory()
    {
        Vector3[] pathPoints = trajectoryCalculator.CalculateBallisticPathPoints(initialPosition, initialVelocity, gravityAcceleration, physicsTimeStep, maxTime);

        for (int i = 1; i < pathPoints.Length; i++)
        {
            Debug.DrawLine(pathPoints[i - 1], pathPoints[i], Color.green);
        }
    }
}
