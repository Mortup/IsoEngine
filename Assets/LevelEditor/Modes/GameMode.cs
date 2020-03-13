using UnityEngine;

namespace com.mortup.city.gamemodes {

    public abstract class GameMode : MonoBehaviour {

        public virtual void Activate() {
            if (enabled) {
                return;
            }

            GameMode[] gameModes = FindObjectsOfType<GameMode>();
            foreach (GameMode mode in gameModes) {
                if (mode != this) {
                    mode.Deactivate();
                }
            }

            enabled = true;
        }

        protected abstract string Name();

        public virtual void Deactivate() {
            enabled = false;
        }
    }

}