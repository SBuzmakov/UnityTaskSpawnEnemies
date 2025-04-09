using UnityEngine;

namespace Source.Scripts
{
    public class EnemyFactory
    {
        private Enemy _enemyPrefab;

        public EnemyFactory(Enemy enemyPrefab)
        {
            _enemyPrefab = enemyPrefab;
        }

        public Enemy ConstructEnemy()
        {
            return Object.Instantiate(_enemyPrefab);
        }
    }
}