using UnityEngine;

using com.mortup.iso;
using com.mortup.iso.resources;

namespace com.mortup.city.gamemodes {

    public class PaintWallMode : GameMode {

        [SerializeField] Level level;

        private Sprite cursorSprite;
        private GameObject cursor;
        private SpriteRenderer cursorSr;

        public override void Activate() {
            base.Activate();

            cursor = new GameObject("Wall Cursor");
            cursorSr = cursor.AddComponent<SpriteRenderer>();
            cursorSr.sortingLayerName = "Wall";
        }

        public override void Deactivate() {
            base.Deactivate();

            Destroy(cursor);
        }

        private void Update() {
            Vector3Int wallCoords = level.transformer.ScreenToWall(Input.mousePosition);
            Vector3 position = level.transformer.WallToWorld(wallCoords);
            position.z = cursor.transform.position.z;
            cursor.transform.position = position;

            cursorSprite = ResourceManager.GetWallSprite(2, level.transformer.RotateWallInsideTile(wallCoords).z);
            cursorSr.sprite = cursorSprite;
        }

        protected override string Name() {
            return "Paint Wall Mode";
        }

    }

}