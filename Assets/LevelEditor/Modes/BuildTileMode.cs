using System.Collections.Generic;
using UnityEngine;

using com.mortup.iso.world;
using com.mortup.iso.world.commands;

namespace com.mortup.city.gamemodes {

    public class BuildTileMode : DraggableTileMode {
        [SerializeField] private Sprite regularSprite;
        [SerializeField] private Sprite removeSprite;

        public override void Activate() {
            base.Activate();

            regularSprite = Resources.Load<Sprite>("Sprites/Cursors/TileRegular");
            removeSprite = Resources.Load<Sprite>("Sprites/Cursors/TileRemove");
        }

        protected override IWorldCommand GetCommand(Vector2Int start, Vector2Int end) {
            if (Input.GetButton("Remove")) {
                return new RemoveFloorAreaCommand(level, start, end);
            }
            else {
                return new BuildFloorAreaCommand(level, start, end);
            }
        }

        public override Sprite GetCursorSprite(Vector2Int position) {
            if (level.data.IsFloorInBounds(position) == false)
                return null;

            if (Input.GetButton("Remove"))
                return removeSprite;

            if (level.data.GetFloor(position.x, position.y) != (int)FloorIndex.Empty)
                return null;

            return regularSprite;
        }

        protected override string Name() {
            return "Build Tile Mode";
        }
    }

}