using System;
using UnityEngine;

namespace Source.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Mover _mover;
        
        public event Action<Enemy> ExitedZone;
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<ZoneTrigger>(out _))
            {
                ExitedZone?.Invoke(this);
            }
        }

        public void SetDirection(Vector3 direction)
        {
            _mover.SetDirection(direction);
        }
    }
}