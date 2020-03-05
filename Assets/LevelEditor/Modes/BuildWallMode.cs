using UnityEngine;

using com.mortup.iso;

namespace com.mortup.city.gamemodes {

    public class BuildWallMode : GameMode {

        [SerializeField] Level level;

        private Sprite cursorSprite;
        private GameObject cursor;
        private SpriteRenderer cursorSr;

        public override void Activate() {
            base.Activate();

            cursor = new GameObject("Wall Cursor");
            cursorSr = cursor.AddComponent<SpriteRenderer>();
            cursorSr.sortingLayerName = "Wall";
            cursorSprite = Resources.Load<Sprite>("Sprites/Cursors/BuildVertex");
        }

        public override void Deactivate() {
            base.Deactivate();

            Destroy(cursor);
        }

        private void Update() {
            Vector3 position = level.transformer.ScreenVertexRounded(Input.mousePosition);
            position.z = cursor.transform.position.z;
            cursor.transform.position = position;

            cursorSr.sprite = cursorSprite;
        }

        protected override string Name() {
            return "Build Wall Mode";
        }

    }

}