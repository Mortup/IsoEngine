using com.mortup.iso.world;
using UnityEngine;

namespace com.mortup.iso.demo.diablo {

    public class EnemySpawner : IsoMonoBehaviour {

        [SerializeField] private GameObject enemyPrefab;

        public override void OnLevelLoad(Level level) {
            ILevelData levelData = level.data;

            for (int x = 0; x < levelData.width; x++) {
                for (int y = 0; y < levelData.height; y++) {
                    if (Random.value < 0.025f) {
                        GameObject enemy = Instantiate(enemyPrefab, level.transformer.TileToWorld(x, y), Quaternion.identity);
                        enemy.GetComponent<PlayerSpriteManager>().Init(level);
                        enemy.GetComponent<JoystickPlayerMovement>().Init(level);
                    }
                }
            }
        }

    }

}