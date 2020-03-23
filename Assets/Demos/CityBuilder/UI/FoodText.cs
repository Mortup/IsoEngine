using UnityEngine;
using TMPro;

namespace com.mortup.iso.demo.citybuilder {

    public class FoodText : MonoBehaviour {
        [SerializeField] TMP_Text text;

        void Update() {
            text.text = CityBuilderManager.food.ToString();
        }
    }

}