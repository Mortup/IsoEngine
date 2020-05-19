using System.Collections.Generic;
using UnityEngine;

using com.mortup.iso.world;

namespace com.mortup.iso.demo.chess {

    public class ChessPieceSelector : MonoBehaviour {
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color mouseOverColor;

        public static List<ChessPieceSelector> instances;
        private static List<Vector2Int> highlightedFloorTiles;
        private static ChessPieceSelector selectedPiece;
        private static float timeSelectedPiece = 0f;

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

            if (highlightedFloorTiles == null)
                highlightedFloorTiles = new List<Vector2Int>();

            instances.Add(this);
            isoTransform = GetComponent<IsometricTransform>();
            level = isoTransform.GetLevel();
            pieceType = (ChessIndex)level.data.GetItem(isoTransform.coords.x, isoTransform.coords.y).x;
        }

        private void OnDestroy() {
            instances.Remove(this);
        }

        private void Update() {
            transform.position = new Vector3(transform.position.x, transform.position.y, sr.sortingOrder * -0.01f);

            if (IsSelected()) {
                UpdateHighlightedTiles();
            }
        }

        private void UpdateHighlightedTiles() {
            foreach (Vector2Int tilePos in highlightedFloorTiles) {
                level.GetFloorGameObject(tilePos).GetComponent<SpriteRenderer>().color = Color.Lerp(Color.green, Color.white, 0.3f);
            }

            Vector2Int mouseCoords = level.transformer.ScreenToTile(Input.mousePosition);
            if (highlightedFloorTiles.Contains(mouseCoords)) {
                level.GetFloorGameObject(mouseCoords).GetComponent<SpriteRenderer>().color = Color.Lerp(Color.green, Color.blue, 0.3f);

                if (Input.GetMouseButtonDown(0) && Time.time != timeSelectedPiece) {
                    Vector2Int previousPosition = (Vector2Int)isoTransform.coords;
                    level.data.SetItem(mouseCoords.x, mouseCoords.y, (int)pieceType, 0);
                    level.data.SetItem(previousPosition.x, previousPosition.y, (int)ChessIndex.EMPTY, 0);
                    CleanHighlightedFloorTiles();
                    selectedPiece = null;
                }
            }
        }

        private void OnMouseEnter() {
            ClearAllPieceSprites();
            sr.color = mouseOverColor;
        }

        private void OnMouseExit() {
            if (IsSelected() == false)
                ClearSprite();
        }

        private void OnMouseDown() {
            selectedPiece = this;
            timeSelectedPiece = Time.time;
            ClearAllPieceSprites(true);
            sr.color = selectedColor;

            CleanHighlightedFloorTiles();

            List<Vector2Int> movementOptions = ChessMovements.GetMovementOptions(level, pieceType, new Vector2Int(isoTransform.coords.x, isoTransform.coords.y));
            foreach (Vector2Int option in movementOptions) {
                highlightedFloorTiles.Add(option);
            }
        }

        private void CleanHighlightedFloorTiles() {
            foreach (Vector2Int tilePos in highlightedFloorTiles) {
                level.GetFloorGameObject(tilePos).GetComponent<SpriteRenderer>().color = Color.white;
            }
            highlightedFloorTiles = new List<Vector2Int>();
        }

        private void ClearSprite() {
            sr.color = originalColor;
        }

        private void ClearAllPieceSprites(bool clearSelected = false) {
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