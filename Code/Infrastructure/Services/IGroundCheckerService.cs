using UnityEngine;

namespace Code.Infrastructure.Services
{
    public interface IGroundCheckerService : IService
    { 
        bool Grounded(Transform obj, float height, out Vector3 groundNormal);
    }
}