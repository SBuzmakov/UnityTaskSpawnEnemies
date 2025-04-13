using UnityEngine;

namespace Source.Scripts
{
    public class Mover : MonoBehaviour
    {
        private const float Speed = 3.0f;

        private Vector3 _direction;

        private void Update()
        {
            Move(_direction);
        }

        public void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }

        private void Move(Vector3 direction)
        {
            transform.rotation = Quaternion.LookRotation(_direction);
            
            transform.position += direction * (Speed * Time.deltaTime);
        }
    }
}