using UnityEngine;

namespace com.mortup.iso.world.commands {

    public class BuildWallCommand : IWorldCommand {

        private Level level;
        private Vector3Int position;
        private int wallType;

        public BuildWallCommand(Level level, Vector3Int position, int wallType = (int)WallIndex.New) {
            this.level = level;
            this.position = position;
            this.wallType = wallType;
        }

        public IWorldCommand Excecute() {
            if (level.data.GetWall(position.x, position.y, position.z) != (int)WallIndex.Empty) {
                return new NullCommand();
            }

            level.data.SetWall(position.x, position.y, position.z, wallType);
            return new RemoveWallCommand(level, position);
        }
    }

}