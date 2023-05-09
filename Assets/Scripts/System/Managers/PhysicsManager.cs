using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    private ProjectileTrajectory trajectoryCalculator;

    // Physical constant settings
    public float gravityAcceleration;
    public float airDragCoefficient;
    public Vector3 windAcceleration;

    // Simulation settings
    public float physicsTimeStep;
    public float maxTime;
    public bool showWeaponTrajectory;
    
    public static float projectileRaycastDistance = 0.2f;

    public static List<BallisticsProjectile> watchedProjectiles = new List<BallisticsProjectile>();

    // TEST
    public Weapon currentWeaponData;
    public Transform muzzle;

    private float _timeStepSquared;
    private Vector3 _gravityVector;

    private void Awake()
    {
        trajectoryCalculator = new ProjectileTrajectory();

        InitializePhysicsContext();

        _timeStepSquared = Mathf.Pow(physicsTimeStep, 2); // So it isn't calculated every frame
    }

    private void FixedUpdate()
    {         
        if (showWeaponTrajectory)
        {
            DrawTrajectory();
        }

        if (watchedProjectiles.Count > 0)
        {
            foreach (var projectile in watchedProjectiles)
            {
                SimulateBulletMotion(projectile);
            }
        }
    }

    private void InitializePhysicsContext()
    {        
        // Temporary values
        gravityAcceleration = Physics.gravity.magnitude;
        physicsTimeStep = Time.fixedDeltaTime;

        _gravityVector = new Vector3(0f, -gravityAcceleration, 0f);
    }

    private void SimulateBulletMotion(BallisticsProjectile projectile)
    {
        Vector3 totalAcceleration = _gravityVector + windAcceleration;

        projectile.velocity = projectile.velocity + totalAcceleration * physicsTimeStep;
        projectile.transform.position = projectile.transform.position +  projectile.velocity * Time.fixedDeltaTime + 0.5f * totalAcceleration * _timeStepSquared;
    }

    public void DrawTrajectory()
    {
        Vector3 initialPos = muzzle.position;
        Vector3 initialVel = new Vector3(0f, 0f, currentWeaponData.muzzleVelocity);
        Vector3 totalAcceleration = _gravityVector + windAcceleration;

        Vector3[] pathPoints = trajectoryCalculator.CalculateBallisticPathPoints(initialPos, initialVel, totalAcceleration, physicsTimeStep, maxTime);
        Vector3[] pathPointsGravOnly = trajectoryCalculator.CalculateBallisticPathPoints(initialPos, initialVel, _gravityVector, physicsTimeStep, maxTime);

        for (int i = 1; i < pathPoints.Length; i++)
        {
            Debug.DrawLine(pathPoints[i - 1], pathPoints[i], Color.red);
            Debug.DrawLine(pathPointsGravOnly[i - 1], pathPointsGravOnly[i], Color.green);

        }
    }

    public static void RegisterProjectile(BallisticsProjectile projectile)
    {
        watchedProjectiles.Add(projectile);
    }

    public static void UnregisterProjectile(BallisticsProjectile projectile)
    {
        watchedProjectiles.Remove(projectile);
    }
}
