using UnityEngine;

namespace Code.Infrastructure.Services
{
    public class GroundCheckerService : IGroundCheckerService
    {

        public bool Grounded(Transform obj, float height, out Vector3 groundNormal)
        {
            groundNormal = Vector3.zero;
            if (!Physics.SphereCast(obj.position, 0.2f, -obj.up, out RaycastHit hit,
                0.5f * height)) return false;
            groundNormal = hit.normal;
            return true;
        }
    
    }
}
