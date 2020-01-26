using UnityEngine;

namespace com.mortup.iso.world.commands {

    public class RemoveFloorCommand : IWorldCommand {

        private Level level;
        private Vector2Int position;

        public RemoveFloorCommand(Level level, Vector2Int position) {
            this.level = level;
            this.position = position;
        }

        public IWorldCommand Excecute() {
            if (level.data.GetFloor(position.x, position.y) == (int)FloorIndex.Empty) {
                return new NullCommand();
            }

            int floorType = level.data.GetFloor(position.x, position.y);
            level.data.SetFloor(position.x, position.y, (int)FloorIndex.Empty);
            return new BuildFloorCommand(level, position, floorType);
        }
    }

}