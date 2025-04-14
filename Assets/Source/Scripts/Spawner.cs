using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Source.Scripts
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private float _spawnInterval = 2.0f;
        [SerializeField] private bool _isWorking = true;
        [SerializeField] private Transform _ground;
        [SerializeField] private Enemy _enemyPrefab;

        private float _positionY;
        private Coroutine _coroutine;
        private EnemyFactory _enemyFactory;
        private ObjectPool<Enemy> _pool;

        public void Awake()
        {
            _enemyFactory = new EnemyFactory(_enemyPrefab);

            _pool = new ObjectPool<Enemy>(
                createFunc: CreateEnemy
            );
        }

        private void Start()
        {
            _positionY = _ground.position.y + 1;

            _coroutine = StartCoroutine(SpawnEnemy());
        }

        private void OnDestroy()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }

        private Enemy CreateEnemy()
        {
            Enemy newEnemy = _enemyFactory.Create();
            newEnemy.ExitedZone += ReleaseEnemy;
            newEnemy.Destroyed += Dispose;

            return newEnemy;
        }

        private void Dispose(Enemy enemy)
        {
           enemy.Destroyed -= Dispose;
           enemy.ExitedZone -= ReleaseEnemy;
        }
        
        private IEnumerator SpawnEnemy()
        {
            WaitForSeconds wait = new WaitForSeconds(_spawnInterval);

            while (_isWorking)
            {
                yield return wait;

                Enemy enemy = _pool.Get();
                TakeEnemyFromPool(enemy);
            }

            _coroutine = null;
        }

        private void TakeEnemyFromPool(Enemy enemy)
        {
            enemy.gameObject.SetActive(true);
            enemy.transform.position = GetSpawnPosition();
            enemy.SetDirection(GetRandomDirection());
        }

        private void ReleaseEnemy(Enemy enemy)
        {
            _pool.Release(enemy);
            enemy.gameObject.SetActive(false);
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