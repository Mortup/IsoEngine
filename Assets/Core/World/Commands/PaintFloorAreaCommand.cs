using System.Collections.Generic;
using UnityEngine;

namespace com.mortup.iso.world.commands {

    public class PaintFloorAreaCommand : IWorldCommand {

        private CompositeCommand command;

        public PaintFloorAreaCommand(Level level, Vector2Int start, Vector2Int end, int floorType) {
            int xMin = Mathf.Min(start.x, end.x);
            int xMax = Mathf.Max(start.x, end.x);
            int yMin = Mathf.Min(start.y, end.y);
            int yMax = Mathf.Max(start.y, end.y);

            List<IWorldCommand> commands = new List<IWorldCommand>();

            for (int x = xMin; x <= xMax; x++) {
                for (int y = yMin; y <= yMax; y++) {
                    commands.Add(new PaintFloorCommand(level, new Vector2Int(x,y), floorType));
                }
            }

            command = new CompositeCommand(commands);
        }

        public IWorldCommand Excecute() {
            return command.Excecute();
        }
    }

}