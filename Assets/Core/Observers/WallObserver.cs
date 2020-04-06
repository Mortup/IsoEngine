using UnityEngine;

using com.mortup.iso.resources;
using com.mortup.iso.world;

namespace com.mortup.iso.observers {

    public class WallObserver : IsoMonoBehaviour {

        Level level;
        PrefabContainer[,,] wallPrefabs;
        
        public override void OnLevelLoad(Level level) {
            this.level = level;
            wallPrefabs = new PrefabContainer[level.data.width + 1, level.data.height + 1, 2];
            UpdateAllTiles();
            level.data.RegisterWallObserver(this);
        }

        public void UpdateWall(int x, int y, int z) {
            DestroyWall(x, y, z);

            int wallIndex = level.data.GetWall(x, y, z);

            int rotatedZ = level.transformer.RotateWallInsideTile(new Vector3Int(x, y, z)).z;
            PrefabContainer wallRes = ResourceManager.GetWallPrefab(wallIndex, z);

            wallRes.gameObject.name = string.Format("Wall [{0}, {1} {2}]", x, y, z);
            wallRes.gameObject.transform.SetParent(level.transform);

            wallRes.spriteRenderer.sortingLayerName = "Wall";

            wallRes.isometricTransform.Init(level, IsometricTransform.ElementType.Wall);
            wallRes.isometricTransform.coords = new Vector3Int(x, y, z);

            wallRes.orientableSprite.Init(level);

            wallPrefabs[x, y, z] = wallRes;
        }

        public GameObject GetWallGameObject(Vector3Int coords) {
            return GetWallGameObject(coords.x, coords.y, coords.z);
        }

        public GameObject GetWallGameObject(int x, int y, int z) {
            return wallPrefabs[x, y, z].gameObject;
        }

        private void UpdateAllTiles() {

            for (int x = 0; x <= level.data.width; x++) {
                for (int y = 0; y <= level.data.height; y++) {
                    UpdateWall(x, y, 0);
                    UpdateWall(x, y, 1);
                }
            }

        }

        public void DestroyWall(int x, int y, int z) {
            if (wallPrefabs[x,y,z] != null) {
                wallPrefabs[x, y, z].Destroy();
            }
        }

    }

}