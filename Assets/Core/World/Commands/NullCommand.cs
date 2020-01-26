namespace com.mortup.iso.world.commands {

    public class NullCommand : IWorldCommand {

        public IWorldCommand Excecute() {
            return new NullCommand();
        }

    }

}