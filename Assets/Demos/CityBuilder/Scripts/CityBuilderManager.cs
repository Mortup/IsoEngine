using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.mortup.iso.demo.citybuilder {

    public class CityBuilderManager : MonoBehaviour {
        public static int coins = 0;
        public static int food = 0;

        private void Start() {
            RestartGame();
        }

        public void RestartGame() {
            coins = 25;
            food = 10;
        }
    }

}