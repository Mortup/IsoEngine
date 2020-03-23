using System.Collections.Generic;
using UnityEngine;

using com.mortup.iso.serialization;
using com.mortup.iso.world;

namespace com.mortup.iso.demo.citybuilder {

    public class MapGenerator : MonoBehaviour, ILevelSerializer {

        [Range (0,1)][SerializeField] private float pathProbability;
        [SerializeField] private int width;
        [SerializeField] private int height;


        public LevelData LoadLevel(string levelName) {
            LevelData levelData = new LevelData(width, height);

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    if (Random.value < pathProbability) {
                        levelData.SetFloor(x, y, 1);
                    }
                    else {
                        levelData.SetFloor(x, y, 0);
                    }
                }
            }

            return levelData;
        }

        public void SaveLevel(LevelData levelData) {
            throw new System.NotImplementedException();
        }

    }

}