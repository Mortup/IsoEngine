using UnityEngine;
using TMPro;

public class FoodText : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    void Update()
    {
        text.text = CityBuilderManager.food.ToString();
    }
}
