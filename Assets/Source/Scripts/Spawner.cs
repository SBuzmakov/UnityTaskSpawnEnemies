using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Source.Scripts
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _spawnInterval = 10.0f;
        [SerializeField] private bool _isWorking = true;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Transform _spawnPosition;

        private Coroutine _coroutine;
        private EnemyFactory _enemyFactory;
        private ObjectPool<Enemy> _pool;

        public void Awake()
        {
            _enemyFactory = new EnemyFactory(_enemyPrefab);

            _pool = new ObjectPool<Enemy>(
                createFunc: CreateEnemy
            );
            
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
            newEnemy.Finalized += ReleaseEnemy;
            newEnemy.Destroyed += Dispose;

            return newEnemy;
        }

        private void Dispose(Enemy enemy)
        {
            enemy.Destroyed -= Dispose;
            enemy.Finalized -= ReleaseEnemy;
        }

        private IEnumerator SpawnEnemy()
        {
            WaitForSeconds wait = new WaitForSeconds(_spawnInterval);

            while (_isWorking)
            {
                Enemy enemy = _pool.Get();
                TakeEnemyFromPool(enemy);

                yield return wait;
            }

            _coroutine = null;
        }

        private void TakeEnemyFromPool(Enemy enemy)
        {
            enemy.gameObject.SetActive(true);
            enemy.transform.position = _spawnPosition.position;
            enemy.SetDirection(_target);
        }

        private void ReleaseEnemy(Enemy enemy)
        {
            _pool.Release(enemy);
            enemy.gameObject.SetActive(false);
        }
    }
}