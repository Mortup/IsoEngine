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
            switch (level.transformer.GetOrientation()) {
                case Transformer.Orientation.NORTH:
                    spriteRenderer.sprite = GetNorthSprite();
                    break;
                case Transformer.Orientation.SOUTH:
                    spriteRenderer.sprite = GetSouthSprite();
                    break;
                case Transformer.Orientation.WEST:
                    spriteRenderer.sprite = GetWestSprite();
                    break;
                case Transformer.Orientation.EAST:
                    spriteRenderer.sprite = GetEastSprite();
                    break;
            }
        }

        public abstract Sprite GetNorthSprite();
        public abstract Sprite GetSouthSprite();
        public abstract Sprite GetEastSprite();
        public abstract Sprite GetWestSprite();
    }

}