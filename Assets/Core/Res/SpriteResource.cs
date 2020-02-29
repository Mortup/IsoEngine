using com.mortup.iso.world;
using UnityEngine;

namespace com.mortup.iso.resources {

    public class SpriteResource : IResource {
        private GameObject _gameObject;
        private IsometricTransform _isometricTransform;
        private SpriteRenderer _spriteRenderer;

        public GameObject gameObject { get { return _gameObject; } }

        public IsometricTransform isometricTransform => _isometricTransform;

        public SpriteRenderer spriteRenderer => _spriteRenderer;

        public SpriteResource(Sprite sprite) {
            _gameObject = new GameObject("Tile");
            _isometricTransform = _gameObject.AddComponent<IsometricTransform>();
            _spriteRenderer = _gameObject.AddComponent<SpriteRenderer>();
            _spriteRenderer.sprite = sprite;
        }

        public void Destroy() {
            GameObject.Destroy(gameObject);
        }
    }

}