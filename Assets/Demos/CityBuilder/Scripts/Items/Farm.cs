using com.mortup.iso.observers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour {

    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private Transform iconSpawnPoint;
    [SerializeField] private float foodPeriod;

    private SpriteRenderer spriteRenderer;
    private float remainingTime;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        remainingTime = Random.Range(0f, foodPeriod);
    }

    private void Update() {
        if (remainingTime <= 0f) {
            AddFood();
        }
        else {
            remainingTime -= Time.deltaTime;
        }

        spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.white, Time.deltaTime * 2f);
    }

    private void AddFood() {
        remainingTime = foodPeriod;
        CityBuilderManager.food += 1;
        Instantiate(iconPrefab, iconSpawnPoint.position, Quaternion.identity, transform);
        spriteRenderer.color = Color.green;
    }

}
