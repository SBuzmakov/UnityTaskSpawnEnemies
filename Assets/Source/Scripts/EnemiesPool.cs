using UnityEngine;
using UnityEngine.Pool;

namespace Source.Scripts
{
    public class EnemiesPool : MonoBehaviour
    {
        [SerializeField] private Enemy _enemyPrefab;
        
        private EnemyFactory _enemyFactory;
        private ObjectPool<Enemy> _pool;

        public void Awake()
        {
            _enemyFactory = new EnemyFactory(_enemyPrefab);

            _pool = new ObjectPool<Enemy>(
                createFunc: CreateEnemy,
                actionOnGet: OnTakeFromPool,
                actionOnRelease: OnReturnToPool,
                actionOnDestroy: OnDestroyEnemy,
                collectionCheck: false
            );
        }
        
        public Enemy Take()
        {
            return _pool.Get();
        }

        private void OnTakeFromPool(Enemy enemy)
        {
            enemy.gameObject.SetActive(true);
        }

        private void OnReturnToPool(Enemy enemy)
        {
            enemy.gameObject.SetActive(false);
        }

        private void OnDestroyEnemy(Enemy enemy)
        {
            enemy.LeftZone -= Release;
            
            Destroy(enemy.gameObject);
        }

        private void Release(Enemy enemy)
        {
            _pool.Release(enemy);
        }

        private Enemy CreateEnemy()
        {
            Enemy newEnemy = _enemyFactory.ConstructEnemy();

            newEnemy.LeftZone += Release;
            
            return newEnemy;
        }
    }
}
