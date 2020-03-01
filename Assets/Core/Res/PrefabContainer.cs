using UnityEngine;

using com.mortup.iso.world;

namespace com.mortup.iso.resources {

    public class PrefabContainer {

        private GameObject _gameObject;
        private IsometricTransform _isometricTransform;
        private SpriteRenderer _spriteRenderer;

        public GameObject gameObject => _gameObject;
        public IsometricTransform isometricTransform => _isometricTransform;
        public SpriteRenderer spriteRenderer => _spriteRenderer;

        public PrefabContainer (GameObject prefab) {
            _gameObject = GameObject.Instantiate(prefab);

            _isometricTransform = _gameObject.GetComponent<IsometricTransform>();
            if (_isometricTransform == null) {
                _isometricTransform = _gameObject.AddComponent<IsometricTransform>();
            }

            _spriteRenderer = _gameObject.GetComponent<SpriteRenderer>();
            if (_spriteRenderer == null) {
                _spriteRenderer = _gameObject.AddComponent<SpriteRenderer>();
            }

        }

        public void Destroy() {
            GameObject.Destroy(_gameObject);
        }
    }

}