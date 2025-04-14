using UnityEngine;

namespace Source.Scripts
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] float _speed = 1.0f;
        private Transform _targetTransform;

        private void Update()
        {
            transform.LookAt(_targetTransform.position);
            transform.position = Vector3.MoveTowards(transform.position, _targetTransform.position , (_speed * Time.deltaTime));
        }

        public void SetTargetDirection(Transform targetTransform)
        {
            _targetTransform = targetTransform;
        }
    }
}