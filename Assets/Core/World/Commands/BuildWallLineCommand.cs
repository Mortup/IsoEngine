using System.Collections.Generic;

using UnityEngine;

namespace com.mortup.iso.world.commands {

    public class BuildWallLineCommand : IWorldCommand {

        private CompositeCommand command;

        public BuildWallLineCommand(Level level, Vector2Int vertexStart, Vector2Int vertexEnd, int wallType = (int)WallIndex.New) {
            Debug.LogFormat("Creating from {0} to {1}", vertexStart, vertexEnd);
            if (vertexEnd.x != vertexEnd.x && vertexStart.y != vertexEnd.y) {
                Debug.Log("Diagonal lines are not allowed.");
            }

            int xMin = Mathf.Min(vertexStart.x, vertexEnd.x);
            int xMax = Mathf.Max(vertexStart.x, vertexEnd.x);
            int yMin = Mathf.Min(vertexStart.y, vertexEnd.y);
            int yMax = Mathf.Max(vertexStart.y, vertexEnd.y);

            List<IWorldCommand> commands = new List<IWorldCommand>();

            int diffX = xMax - xMin;
            int diffY = yMax - yMin;

            int z = 0;
            if (diffX > diffY) {
                xMax -= 1;
                z = 1;
            }
            else
                yMax -= 1;

            for (int x = xMin; x <= xMax; x++) {
                for (int y = yMin; y <= yMax; y++) {
                    commands.Add(new BuildWallCommand(level, new Vector3Int(x,y,z)));
                }

            }

            command = new CompositeCommand(commands);
        }

        public IWorldCommand Excecute() {
            return command.Excecute();
        }

    }

}