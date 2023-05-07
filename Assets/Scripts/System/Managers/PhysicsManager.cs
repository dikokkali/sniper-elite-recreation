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

    public static float projectileRaycastDistance = 0.2f;

    public bool showWeaponTrajectory;
    public float gravityAcceleration;
    public float physicsTimeStep;

    public static ProjectileController watchedProjectile;

    // TEST
    public Weapon currentWeaponData;
    public Transform muzzle;

    private float _timeStepSquared;
    private float _timeAccumulator;

    private void Awake()
    {
        trajectoryCalculator = new ProjectileTrajectory();

        InitializePhysicsContext();

        _timeStepSquared = Mathf.Pow(physicsTimeStep, 2); // So it isn't calculated every frame
        initialPosition = muzzle.position;
    }

    private void FixedUpdate()
    {         
        if (showWeaponTrajectory)
        {
            DrawTrajectory();
        }

        if (watchedProjectile != null)
        {
            SimulateBulletMotion();
        }
    }



    private void InitializePhysicsContext()
    {
        gravityAcceleration = Physics.gravity.magnitude;
        physicsTimeStep = Time.fixedDeltaTime;
    }

    private void SimulateBulletMotion()
    {
        Vector3 bulletVelocity = new Vector3(0f, 0f, currentWeaponData.muzzleVelocity); // Z is forward by convention
        Vector3 gravityVector = new Vector3(0f, gravityAcceleration, 0f);

        watchedProjectile.transform.position +=
            bulletVelocity * Time.fixedDeltaTime - 0.5f * gravityVector * _timeStepSquared;
    }

    public void DrawTrajectory()
    {
        Vector3[] pathPoints = trajectoryCalculator.CalculateBallisticPathPoints(initialPosition, initialVelocity, gravityAcceleration, physicsTimeStep, maxTime);

        for (int i = 1; i < pathPoints.Length; i++)
        {
            Debug.DrawLine(pathPoints[i - 1], pathPoints[i], Color.green);
        }
    }

    public static void RegisterProjectile(ProjectileController projectile)
    {
        watchedProjectile = projectile;
    }

    public static void UnregisterProjectile()
    {
        watchedProjectile = null;
    }
}
