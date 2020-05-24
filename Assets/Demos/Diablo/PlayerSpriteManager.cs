using UnityEngine;

namespace com.mortup.iso.demo.diablo {

    public class PlayerSpriteManager : MonoBehaviour {
        [SerializeField] private Level level;
        [SerializeField] private Transform feetPosition;
        private SpriteRenderer[] spriteRenderers;

        private void Awake() {
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }

        public void Init(Level level) {
            this.level = level;
        }

        private void Start() {
            for (int i = 0; i < spriteRenderers.Length; i++) {
                spriteRenderers[i].sortingLayerName = "Items";
            }
        }

        private void Update() {
            Vector2Int tilePos = level.transformer.WorldToTile(feetPosition.position);
            for (int i = 0; i < spriteRenderers.Length; i++) {
                spriteRenderers[i].sortingLayerName = "Items";
                spriteRenderers[i].sortingOrder = level.transformer.ItemSortingOrder(tilePos.x, tilePos.y);
            }
        }
    }

}