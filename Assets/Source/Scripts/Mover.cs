using UnityEngine;

namespace Source.Scripts
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] float _speed;
        
        private void OnEnable()
        {
            RandomRotate();
        }

        private void Update()
        {
            MoveForward();
        }

        private void MoveForward()
        {
            transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
        }

        private void RandomRotate()
        {
            float randomRotation = Random.Range(0f, 360f);

            transform.Rotate(Vector3.up, randomRotation);
        }
    }
}