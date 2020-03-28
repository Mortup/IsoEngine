using System.Collections.Generic;

using UnityEngine;

namespace com.mortup.iso.observers {

    public abstract class OrientableSprite : MonoBehaviour {

        private static List<OrientableSprite> instances;

        public static List<OrientableSprite> GetInstances() { return instances; }

        [SerializeField] private Level level;

        private SpriteRenderer spriteRenderer;

        protected virtual void Awake() {
            spriteRenderer = GetComponent<SpriteRenderer>();

            if (spriteRenderer == null) {
                Debug.LogError("OrientableSprite needs a SpriteRenderer.");
            }
        }

        public void Init(Level level) {
            if (instances == null) {
                instances = new List<OrientableSprite>();
            }
            instances.Add(this);

            this.level = level;
        }

        void Start() {
            UpdateSprite();
        }

        private void OnDestroy() {
            if (instances != null) {
                instances.Remove(this);
            }
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