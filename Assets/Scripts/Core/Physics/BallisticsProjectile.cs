using UnityEngine;

public class BallisticsProjectile : MonoBehaviour
{
    public Vector3 velocity;
    public float kineticEnergy;  
    public float mass; 


    private void Awake()
    {
        PhysicsManager.RegisterProjectile(this);
        Debug.Log("Bullet starting point: " + transform.position);
    }

    private void OnDestroy()
    {
        PhysicsManager.UnregisterProjectile(this);        
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

    public float getKineticEnergy() {
        return 0.5f * mass * velocity.magnitude * velocity.magnitude;
    }

    public Vector3 getLinearMomentum() {
        return mass * velocity;
    } 
}
