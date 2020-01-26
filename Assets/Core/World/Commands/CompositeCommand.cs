using System.Collections.Generic;

namespace com.mortup.iso.world.commands {

    public class CompositeCommand : IWorldCommand {

        private List<IWorldCommand> commands;

        public CompositeCommand(List<IWorldCommand> commands) {
            this.commands = commands;
        }

        public IWorldCommand Excecute() {
            List<IWorldCommand> inverseCommands = new List<IWorldCommand>();

            foreach(IWorldCommand command in commands) {
                inverseCommands.Add(command.Excecute());
            }

            return new CompositeCommand(inverseCommands);
        }

    }

}