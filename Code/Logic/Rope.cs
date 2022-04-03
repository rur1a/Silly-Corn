using UnityEngine;

namespace Code.Logic
{
    public class Rope : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [SerializeField] private FixedJoint _joint; 
        [SerializeField] private Vector2 _range;


        public void Move(Vector3 direction)
        {
            Rigidbody.AddForce(transform.forward*direction.x, ForceMode.Acceleration);
            _joint.transform.localPosition = CalculateDesplacement(direction);
        }

        private Vector3 CalculateDesplacement(Vector3 direction)
        {
            Vector3 localPosition = _joint.transform.localPosition;
            localPosition = localPosition.Where(y: localPosition.y + direction.z * Time.deltaTime);
            localPosition = localPosition.Where(y: Mathf.Clamp(localPosition.y, _range.x, +_range.y));
            return localPosition;
        }

        public void AddRigidbody(Rigidbody rigidbody)
        {
            _joint.connectedBody = rigidbody;
        }
        public void Reset()
        {
            _joint.connectedBody = null;
            _joint.transform.localPosition = new Vector3(0, _range.x, 0);
        }
    }
}
