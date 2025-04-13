using UnityEngine;

namespace Source.Scripts
{
    public class EnemyFactory
    {
        private readonly Enemy _enemyPrefab;

        public EnemyFactory(Enemy enemyPrefab)
        {
            _enemyPrefab = enemyPrefab;
        }

        public Enemy Create()
        {
            return Object.Instantiate(_enemyPrefab);
        }
    }
}