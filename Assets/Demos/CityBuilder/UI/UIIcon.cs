using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.mortup.iso.demo.citybuilder {

    public class UIIcon : MonoBehaviour {
        [SerializeField] private float movementLerpSpeed;
        [SerializeField] private float movement;

        private Vector3 startingPos;
        private Vector3 targetPos;

        private SpriteRenderer spriteRenderer;

        private void Awake() {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start() {
            startingPos = transform.position;
            targetPos = startingPos + Vector3.up * movement;
        }

        private void Update() {
            transform.position = Vector3.Lerp(transform.position, targetPos, movementLerpSpeed * Time.deltaTime);

            Color color = spriteRenderer.color;
            color.a = Mathf.Abs((targetPos - transform.position).y) / Mathf.Abs((targetPos - startingPos).y);
            spriteRenderer.color = color;

            if (Vector3.Distance(transform.position, targetPos) < 0.1f) {
                Destroy(gameObject, 2f);
            }
        }
    }

}