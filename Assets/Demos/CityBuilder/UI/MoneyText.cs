using UnityEngine;
using TMPro;

namespace com.mortup.iso.demo.citybuilder {

    public class MoneyText : MonoBehaviour {
        [SerializeField] TMP_Text text;

        void Update() {
            text.text = CityBuilderManager.coins.ToString();
        }
    }

}