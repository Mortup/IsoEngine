using UnityEngine;

namespace com.mortup.city.world.commands {

    public class RemoveFloorWorldCommand : IWorldCommand {

        private Level level;
        private Vector2Int start;
        private Vector2Int end;

        public RemoveFloorWorldCommand(Level level, Vector2Int start, Vector2Int end) {
            this.level = level;
            this.start = start;
            this.end = end;
        }

        public IWorldCommand Excecute() {

            int xMin = Mathf.Min(start.x, end.x);
            int xMax = Mathf.Max(start.x, end.x);
            int yMin = Mathf.Min(start.y, end.y);
            int yMax = Mathf.Max(start.y, end.y);

            for (int x = xMin; x <= xMax; x++) {
                for (int y = yMin; y <= yMax; y++) {
                    level.data.SetFloor(x, y, (int)FloorIndex.Empty);
                }
            }

            return new BuildFloorWorldCommand(level, start, end);
        }

    }

}