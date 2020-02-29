﻿using UnityEngine;

using com.mortup.iso.resources;

namespace com.mortup.iso.observers {

    public class WallObserver {

        Level level;
        IResource[,,] wallResources;
        
        public WallObserver(Level level) {
            this.level = level;
            wallResources = new IResource[level.data.width + 1, level.data.height + 1, 2];
            UpdateAllTiles();
            level.data.RegisterWallObserver(this);
        }

        public void UpdateWall(int x, int y, int z) {
            DestroyWall(x, y, z);

            int wallIndex = level.data.GetWall(x, y, z);

            IResource wallRes = ResourceManager.GetWall(wallIndex, z);

            wallRes.gameObject.name = string.Format("Wall [{0}, {1} {2}]", x, y, z);
            wallRes.gameObject.transform.SetParent(level.transform);

            wallRes.spriteRenderer.sortingLayerName = "Wall";

            wallRes.isometricTransform.Init(level);
            wallRes.isometricTransform.coords = new Vector3Int(x, y, z);

            wallResources[x, y, z] = wallRes;
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
            if (wallResources[x,y,z] != null) {
                wallResources[x, y, z].Destroy();
            }
        }

    }

}