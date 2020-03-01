using UnityEngine;

using com.mortup.iso.observers;
using com.mortup.iso.world;

namespace com.mortup.iso.resources {

    public class PrefabContainer {

        private GameObject _gameObject;
        private IsometricTransform _isometricTransform;
        private SpriteRenderer _spriteRenderer;
        private OrientableSprite _orientableSprite;

        public GameObject gameObject => _gameObject;
        public IsometricTransform isometricTransform => _isometricTransform;
        public SpriteRenderer spriteRenderer => _spriteRenderer;
        public OrientableSprite orientableSprite => _orientableSprite;

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

            _orientableSprite = _gameObject.GetComponent<OrientableSprite>();
            if (_orientableSprite == null) {
                _orientableSprite = _gameObject.AddComponent<SingleOrientableSprite>();
            }
        }

        public void Destroy() {
            GameObject.Destroy(_gameObject);
        }
    }

}