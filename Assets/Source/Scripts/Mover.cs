using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts
{
    public class Mover : MonoBehaviour
    {
        private const float ChangeWaypointDistance = 0.1f;
        
        [SerializeField] float _speed = 1.0f;
        [SerializeField] private List<Transform> _waypoints;

        private Transform _target;
        private int _currentWaypoint = 0;

        private void Start()
        {
            if (_target != null)
                _waypoints.Add(_target);
        }

        private void Update()
        {
            Move();
        }

        public void SetTargetDirection(Transform targetTransform)
        {
            _target = targetTransform;
        }

        private void Move()
        {
            if (_waypoints.Count == 0 || _waypoints == null)
                return;
            
            transform.LookAt(_waypoints[_currentWaypoint].position);
            transform.position =
                Vector3.MoveTowards(transform.position, _waypoints[_currentWaypoint].position,
                    (_speed * Time.deltaTime));
            
            if (Vector3.Distance(transform.position, _waypoints[_currentWaypoint].position) < ChangeWaypointDistance)
                _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Count;
        }
    }
}