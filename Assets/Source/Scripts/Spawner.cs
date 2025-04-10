using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private EnemiesPool _pool;
        [SerializeField] private float _spawnInterval = 2.0f;
        [SerializeField] private bool _isWorking = true;
        [SerializeField] private GameObject _ground;

        private float _positionY;
        
        private void Start()
        {
            _positionY = _ground.transform.position.y + 1;
            
            StartCoroutine(SpawnEnemy());
        }

        private void OnDisable()
        {
            StopCoroutine(SpawnEnemy());
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

                if (enemy.TryGetComponent(out Mover mover))
                {
                    mover.SetDirection(direction);
                    
                    mover.Rotate(direction);
                }
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

