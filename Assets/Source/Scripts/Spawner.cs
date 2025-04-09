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
            while (_isWorking)
            {
                yield return new WaitForSeconds(_spawnInterval);

                Enemy enemy = _pool.Take();

                enemy.transform.position = SetSpawnPosition();
            }
        }

        private Vector3 SetSpawnPosition()
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

