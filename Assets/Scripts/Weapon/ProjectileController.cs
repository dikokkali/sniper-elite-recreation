using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private void Awake()
    {
        PhysicsManager.RegisterProjectile(this);
        Debug.Log("Bullet starting point: " + transform.position);
    }

    private void OnDestroy()
    {
        PhysicsManager.UnregisterProjectile();
    }

    private void FixedUpdate()
    {
        Ray bulletRay = new Ray(transform.position, transform.forward);
        RaycastHit bulletHit;

        if (Physics.Raycast(bulletRay, out bulletHit, PhysicsManager.projectileRaycastDistance, ~LayerMask.NameToLayer("BulletDamageable")))
        {
            Debug.Log("HIT " + bulletHit.collider.name);
        }
    }
}