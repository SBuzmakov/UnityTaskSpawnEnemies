using System;
using UnityEngine;

namespace Source.Scripts
{
    public class Enemy : MonoBehaviour
    {
        public event Action<Enemy> LeftZone;
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<ZoneTrigger>(out _))
            {
                LeftZone?.Invoke(this);
            }
        }
    }
}