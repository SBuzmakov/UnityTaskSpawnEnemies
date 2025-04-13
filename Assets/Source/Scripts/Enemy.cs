using System;
using UnityEngine;

namespace Source.Scripts
{
    public class Enemy : MonoBehaviour
    {
        private Mover _mover;
        
        public event Action<Enemy> ExitedZone;

        private void Awake()
        {
            _mover = gameObject.AddComponent<Mover>();
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<ZoneTrigger>(out _))
            {
                ExitedZone?.Invoke(this);
            }
        }

        public void GetDirection(Vector3 direction)
        {
            _mover.SetDirection(direction);
        }
    }
}