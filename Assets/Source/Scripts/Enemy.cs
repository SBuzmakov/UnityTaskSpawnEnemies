using System;
using UnityEngine;

namespace Source.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Mover _mover;
        
        public event Action<Enemy> ExitedZone;
        public event Action<Enemy> Destroyed;
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<ZoneTrigger>(out _))
            {
                ExitedZone?.Invoke(this);
            }
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