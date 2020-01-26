using UnityEngine;

namespace com.mortup.iso.world.commands {

    public class BuildFloorCommand : IWorldCommand {

        private Level level;
        private Vector2Int position;
        private int floorType;

        public BuildFloorCommand(Level level, Vector2Int position, int floorType = (int)FloorIndex.New) {
            this.level = level;
            this.position = position;
            this.floorType = floorType;
        }

        public IWorldCommand Excecute() {
            if (level.data.GetFloor(position.x, position.y) != (int)FloorIndex.Empty) {
                return new NullCommand();
            }

            level.data.SetFloor(position.x, position.y, floorType);
            return new RemoveFloorCommand(level, position);
        }
    }

}