using System;
using UnityEngine;

namespace Source.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Mover _mover;
        [SerializeField] private AudioSource _audio;

        public event Action<Enemy> Finalized;
        public event Action<Enemy> Destroyed;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<Target>(out _))
            {
                if (_audio != null)
                    _audio.Play();
                
                Finalized?.Invoke(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<ZoneTrigger>(out _))
                Finalized?.Invoke(this);
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }

        public void SetDirection(Transform targetPosition)
        {
            _mover.SetTargetDirection(targetPosition);
        }
    }
}