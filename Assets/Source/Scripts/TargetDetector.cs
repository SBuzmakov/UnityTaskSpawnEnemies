using System;
using UnityEngine;

namespace Source.Scripts
{
    public class TargetDetector
    {
        private const float MinDistance = 3.0f;
        
        private readonly Transform _transform;

        private Transform _value;

        public TargetDetector(Transform transform)
        {
            _transform = transform;
        }

        public event Action MinDistanceReached;

        public void Update()
        {
            if (CheckMinDistance())
                MinDistanceReached?.Invoke();
        }
        
        public void SetValue(Transform target)
        {
            _value = target;
        }

        private bool CheckMinDistance()
        {
            return _transform.position.IsEnoughClose(_value.position, MinDistance);
        }
    }
}