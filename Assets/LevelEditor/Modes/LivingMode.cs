namespace com.mortup.city.gamemodes {

    public class LivingMode : GameMode {

        private void Start() {
            Activate();
        }

        protected override string Name() {
            return "Living Mode";
        }
    }

}