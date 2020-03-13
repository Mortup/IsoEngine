using UnityEngine;

namespace com.mortup.iso.world.commands {

    public class PaintWallCommand : IWorldCommand {

        private Level level;
        private Vector3Int position;
        private int wallType;

        public PaintWallCommand(Level level, Vector3Int position, int wallType) {
            this.level = level;
            this.position = position;
            this.wallType = wallType;
        }

        public IWorldCommand Excecute() {
            if (wallType == (int)WallIndex.Empty) {
                Debug.LogWarning("Can't use paint wall commands to remove walls.");
                return new NullCommand();
            }

            int initialType = level.data.GetWall(position.x, position.y, position.z);
            if (initialType == (int)WallIndex.Empty) {
                Debug.LogWarning("Can't use paint wall commands to build walls.");
                return new NullCommand();
            }

            if (initialType == wallType) {
                return new NullCommand();
            }

            level.data.SetWall(position.x, position.y, position.z, wallType);
            return new PaintWallCommand(level, position, initialType);
        }
    }

}