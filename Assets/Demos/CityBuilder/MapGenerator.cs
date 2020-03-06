using System.Collections.Generic;
using UnityEngine;

using com.mortup.iso.serialization;
using com.mortup.iso.world;

namespace com.mortup.citybuilder {

    public class MapGenerator : MonoBehaviour, ILevelSerializer {

        [SerializeField] private int width;
        [SerializeField] private int height;


        public LevelData LoadLevel(string levelName) {
            LevelData levelData = new LevelData(width, height);

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    levelData.SetFloor(x, y, 3);
                }
            }

            for (int x = 3; x < 5; x++) {
                for (int y = 0; y < height; y++) {
                    levelData.SetFloor(y, x, 1);
                }
            }

            levelData.SetFloor(2, 0, 1);
            levelData.SetFloor(2, 1, 1);
            levelData.SetFloor(2, 2, 1);
            levelData.SetFloor(8, 5, 1);
            levelData.SetFloor(8, 6, 1);
            levelData.SetFloor(7, 6, 1);
            levelData.SetFloor(6, 6, 1);
            levelData.SetFloor(6, 7, 1);
            levelData.SetFloor(6, 8, 1);
            levelData.SetFloor(6, 9, 1);

            return levelData;
        }

        public void SaveLevel(LevelData levelData) {
            throw new System.NotImplementedException();
        }

    }

}