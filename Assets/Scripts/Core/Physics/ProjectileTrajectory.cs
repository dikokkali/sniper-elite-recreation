using UnityEngine;

// TODO: Make this more encapsulated
public class ProjectileTrajectory 
{
    public Vector3[] CalculateBallisticPathPoints(Vector3 initialPos, Vector3 initialVel, float gravityAcc, float timeStep, float maxTime)
    {
        int maxIterations = Mathf.CeilToInt(maxTime / timeStep);

        Vector3[] points = new Vector3[Mathf.CeilToInt(maxIterations)];

        Vector3 gravity = new Vector3(0f, gravityAcc, 0f);
        
        Vector3 currentVel = initialVel;    
        points[0] = initialPos;

       for (int i = 1; i < maxIterations; i++)
        {
            currentVel = currentVel - gravity * timeStep;
            points[i] = points[i-1]  + currentVel * timeStep - 0.5f * gravity * Mathf.Pow(timeStep, 2);
        }

        return points;
    } 
    
    public bool ProjectileHitCast(Vector3[] path, out RaycastHit hitObject, int ignoreLayers)
    {
        RaycastHit hit = new RaycastHit();

        for (int i = 1; i < path.Length; i++)
        {
            Ray lineRay = new Ray(path[i - 1], path[i] - path[i - 1]);            

            if (Physics.Raycast(lineRay, out hit, Vector3.Distance(path[i - 1], path[i]), ignoreLayers))
            {
                hitObject = hit;
                return true;
            }
        }

        hitObject = hit;
        return false;
    }
}
