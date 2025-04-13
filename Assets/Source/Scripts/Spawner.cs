using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Scripts
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private EnemiesPool _pool;
        [SerializeField] private float _spawnInterval = 2.0f;
        [SerializeField] private bool _isWorking = true;
        [SerializeField] private Transform _ground;

        private float _positionY;
        private Coroutine _coroutine;

        private void Start()
        {
            _positionY = _ground.position.y + 1;

            _coroutine = StartCoroutine(SpawnEnemy());
        }

        private void OnDisable()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            else
                throw new NullReferenceException("_coroutine is null");
        }

        private IEnumerator SpawnEnemy()
        {
            WaitForSeconds wait = new WaitForSeconds(_spawnInterval);

            while (_isWorking)
            {
                yield return wait;

                Enemy enemy = _pool.Take();

                enemy.transform.position = GetSpawnPosition();

                Vector3 direction = GetRandomDirection();

                enemy.GetDirection(direction);
            }
        }

        private Vector3 GetRandomDirection()
        {
            return new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
        }

        private Vector3 GetSpawnPosition()
        {
            Vector3 spawnPointPosition = GetRandomSpawnPoint().position;

            return new Vector3(spawnPointPosition.x, _positionY, spawnPointPosition.z);
        }

        private Transform GetRandomSpawnPoint()
        {
            return _spawnPoints[Random.Range(0, _spawnPoints.Count)].transform;
        }
    }
}