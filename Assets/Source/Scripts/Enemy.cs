using System;
using UnityEngine;

namespace Source.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Mover _mover;

        private TargetDetector _targetDetector;

        public event Action<Enemy> TargetReached;
        public event Action<Enemy> Destroyed;

        private void Awake()
        {
            _targetDetector = new TargetDetector(transform);
        }

        private void OnEnable()
        {
            _targetDetector.MinDistanceReached += OnTargetReached;
        }

        private void Update()
        {
            _targetDetector.Update();
        }
        
        private void OnDisable()
        {
            _targetDetector.MinDistanceReached -= OnTargetReached;
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }

        public void Initialize(Transform targetTransform)
        {
            _mover.SetTargetDirection(targetTransform);
            _targetDetector.SetValue(targetTransform);
        }

        private void OnTargetReached()
        {
            TargetReached?.Invoke(this);
        }
    }
}