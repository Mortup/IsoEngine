using com.mortup.iso.observers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.mortup.iso.demo.citybuilder {

    public class House : MonoBehaviour {

        [SerializeField] private GameObject iconPrefab;
        [SerializeField] private Transform iconSpawnPoint;
        [SerializeField] private float moneyPeriod;

        [SerializeField] private Sprite spriteOption1;
        [SerializeField] private Sprite spriteOption2;

        private SpriteRenderer spriteRenderer;
        private float remainingTime;

        private void Awake() {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start() {
            remainingTime = Random.Range(0f, moneyPeriod);
            spriteRenderer.sprite = Random.value < 0.5f ? spriteOption1 : spriteOption2;
        }

        private void Update() {
            if (remainingTime <= 0f) {
                AddMoney();
            }
            else {
                remainingTime -= Time.deltaTime;
            }

            spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.white, Time.deltaTime * 2f);
        }

        private void AddMoney() {
            remainingTime = moneyPeriod;
            CityBuilderManager.coins += 1;
            Instantiate(iconPrefab, iconSpawnPoint.position, Quaternion.identity, transform);
            spriteRenderer.color = Color.green;
        }

    }

}