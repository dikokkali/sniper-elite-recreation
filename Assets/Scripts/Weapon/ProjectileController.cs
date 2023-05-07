using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Vector3 bulletVelocity = Vector3.zero;   

    private void Awake()
    {
        PhysicsManager.RegisterProjectile(this);
        Debug.Log("Bullet starting point: " + transform.position);
    }

    private void OnDestroy()
    {
        PhysicsManager.UnregisterProjectile();        
    }   

    private void Update()
    {
        Ray bulletRay = new Ray(transform.position, transform.forward);
        RaycastHit bulletHit;

        Debug.DrawLine(transform.position, transform.position + transform.forward * PhysicsManager.projectileRaycastDistance);

        if (Physics.Raycast(bulletRay, out bulletHit, PhysicsManager.projectileRaycastDistance, ~LayerMask.NameToLayer("BulletDamageable")))
        {
            //Debug.Break();
        }
    }
}