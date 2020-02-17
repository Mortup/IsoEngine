using com.mortup.iso.world;
using com.mortup.iso.world.commands;
using UnityEngine;

namespace com.mortup.city.gamemodes {

    public class PaintTileMode : DraggableTileMode {

        [SerializeField] private Sprite regularSprite;
        [SerializeField] private Sprite removeSprite;

        private int tileIndex;

        public override void Activate() {
            base.Activate();

            tileIndex = (int)FloorIndex.FirstDesign;
        }

        public override Sprite GetCursorSprite(Vector2Int position) {
            if (level.data.IsFloorInBounds(position) == false)
                return null;

            if (level.data.GetFloor(position.x, position.y) == (int)FloorIndex.Empty)
                return null;

            if (Input.GetButton("Remove"))
                return removeSprite;

            return regularSprite;
        }

        protected override IWorldCommand GetCommand(Vector2Int start, Vector2Int end) {
            if (Input.GetButton("Remove")) {
                return new PaintFloorAreaCommand(level, start, end, (int)FloorIndex.New);
            }
            else {
                return new PaintFloorAreaCommand(level, start, end, tileIndex);
            }
        }

        protected override string Name() {
            return "Tile Paint Mode";
        }

    }

}