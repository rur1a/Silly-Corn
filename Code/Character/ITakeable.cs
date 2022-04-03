using UnityEngine;

namespace Code.Character
{
    interface ITakeable
    {
        Rigidbody Rigidbody { get; }
        void StopMovement();
    }
}