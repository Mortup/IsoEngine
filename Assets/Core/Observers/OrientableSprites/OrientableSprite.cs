using UnityEngine;

using com.mortup.iso.world;

namespace com.mortup.iso.observers {

    public abstract class OrientableSprite : MonoBehaviour {

        [SerializeField] private Level level;

        private SpriteRenderer spriteRenderer;

        protected virtual void Awake() {
            spriteRenderer = GetComponent<SpriteRenderer>();

            if (spriteRenderer == null) {
                Debug.LogError("OrientableSprite needs a SpriteRenderer.");
            }
        }

        public void Init(Level level) {
            this.level = level;
        }

        void Start() {
            UpdateSprite();
        }

        public Level GetLevel() {
            return level;
        }

        public void UpdateSprite() {
            Sprite sprite;
            switch (level.transformer.GetOrientation()) {
                case Transformer.Orientation.NORTH:
                    sprite = GetNorthSprite();
                    break;
                case Transformer.Orientation.SOUTH:
                    sprite = GetSouthSprite();
                    break;
                case Transformer.Orientation.WEST:
                    sprite = GetWestSprite();
                    break;
                default:
                    sprite = GetEastSprite();
                    break;
            }
            if (sprite != null) {
                spriteRenderer.sprite = sprite;
            }
        }

        public abstract Sprite GetNorthSprite();
        public abstract Sprite GetSouthSprite();
        public abstract Sprite GetEastSprite();
        public abstract Sprite GetWestSprite();
    }

}