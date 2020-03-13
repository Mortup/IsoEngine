using UnityEngine;

namespace com.mortup.iso.world.commands {

    public class RemoveWallCommand : IWorldCommand {

        private Level level;
        private Vector3Int position;

        public RemoveWallCommand(Level level, Vector3Int position) {
            this.level = level;
            this.position = position;
        }

        public IWorldCommand Excecute() {
            if (level.data.GetWall(position.x, position.y, position.z) == (int)WallIndex.Empty) {
                return new NullCommand();
            }

            int wallType = level.data.GetWall(position.x, position.y, position.z);
            level.data.SetWall(position.x, position.y, position.z, (int)WallIndex.Empty);
            return new BuildWallCommand(level, position, wallType);
        }
    }

}