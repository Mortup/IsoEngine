using UnityEngine;

namespace com.mortup.city.gamemodes {

    public abstract class GameMode : MonoBehaviour {

        public virtual void Activate() {

            GameMode[] gameModes = FindObjectsOfType<GameMode>();
            foreach (GameMode mode in gameModes) {
                if (mode != this) {
                    mode.Deactivate();
                }
            }

            Debug.Log("Activating " + Name());
            enabled = true;
        }

        protected abstract string Name();

        public virtual void Deactivate() {
            Debug.Log("Deactivating " + Name());
            enabled = false;
        }
    }

}