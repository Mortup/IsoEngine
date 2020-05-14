using System.Collections.Generic;
using UnityEngine;

using com.mortup.iso.world;

namespace com.mortup.iso.demo.chess {

    public class ChessPieceSelector : MonoBehaviour {
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color mouseOverColor;

        public static List<ChessPieceSelector> instances;
        private static ChessPieceSelector selectedPiece;

        private ChessIndex pieceType;
        private SpriteRenderer sr;
        private Color originalColor;

        private Level level;
        private IsometricTransform isoTransform;

        private void Awake() {
            sr = GetComponent<SpriteRenderer>();
            originalColor = sr.color;
        }

        private void Start() {
            if (instances == null)
                instances = new List<ChessPieceSelector>();

            instances.Add(this);
            isoTransform = GetComponent<IsometricTransform>();
            level = isoTransform.GetLevel();
            pieceType = (ChessIndex)level.data.GetItem(isoTransform.coords.x, isoTransform.coords.y).x;
        }

        private void Update() {
            transform.position = new Vector3(transform.position.x, transform.position.y, sr.sortingOrder * -0.01f);
        }

        private void OnMouseEnter() {
            ClearAllSprites();
            sr.color = mouseOverColor;
        }

        private void OnMouseExit() {
            if (IsSelected() == false)
                ClearSprite();
        }

        private void OnMouseDown() {
            selectedPiece = this;
            ClearAllSprites(true);
            sr.color = selectedColor;
        }

        private void ClearSprite() {
            sr.color = originalColor;
        }

        private void ClearAllSprites(bool clearSelected = false) {
            foreach (ChessPieceSelector cps in instances) {
                if (clearSelected == false && cps == selectedPiece)
                    continue;

                cps.ClearSprite();
            }
        }

        private bool IsSelected() {
            return this == selectedPiece;
        }

        private bool IsAnyPieceSelected() {
            return selectedPiece != null;
        }
    }

}