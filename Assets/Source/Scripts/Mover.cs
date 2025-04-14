using UnityEngine;

namespace Source.Scripts
{
    public class Mover : MonoBehaviour
    {
        private const float Speed = 3.0f;

        private Vector3 _direction;

        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(_direction);
            transform.position += _direction * (Speed * Time.deltaTime);
        }

        public void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }
    }
}