using UnityEngine;
using TMPro;

public class MoneyText : MonoBehaviour {
    [SerializeField] TMP_Text text;

    void Update() {
        text.text = CityBuilderManager.coins.ToString();
    }
}
