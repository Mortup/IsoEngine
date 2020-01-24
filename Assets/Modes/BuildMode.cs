using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using com.mortup.city.world;
using com.mortup.city.world.commands;

namespace com.mortup.city.gamemodes {

    public class BuildMode : GameMode {
        [SerializeField] private Level level;
        [SerializeField] private GameObject cursorPrefab;

        GameObject cursorContainer;
        int tileIndex;

        Sprite regularSprite;
        Sprite removeSprite;

        Vector2Int dragStartCoords;
        bool isDragging;

        Stack<IWorldCommand> commandStack;
        List<GameObject> cursors;

        public override void Activate() {
            base.Activate();

            tileIndex = 1;
            cursorContainer = new GameObject("Build Cursors");
            isDragging = false;
            cursors = new List<GameObject>();
            commandStack = new Stack<IWorldCommand>();

            regularSprite = Resources.Load<Sprite>("Sprites/Cursors/TileRegular");
            removeSprite = Resources.Load<Sprite>("Sprites/Cursors/TileRemove");
        }

        public override void Deactivate() {
            base.Deactivate();

            Destroy(cursorContainer);
            //TODO: Delete pool
        }

        private void Update() {
            DeleteCursors();
            Vector2Int mouseCoords = level.transformer.ScreenToTile(Input.mousePosition);

            if (!isDragging) {
                dragStartCoords = level.transformer.ScreenToTile(Input.mousePosition);
            }

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
                isDragging = true;
            }

            if (Input.GetMouseButtonUp(0)) {
                isDragging = false;
                IWorldCommand cmd = GetCommand(dragStartCoords, mouseCoords);
                commandStack.Push(cmd.Excecute());
            }

            int xMin = Mathf.Min(dragStartCoords.x, mouseCoords.x);
            int xMax = Mathf.Max(dragStartCoords.x, mouseCoords.x);
            int yMin = Mathf.Min(dragStartCoords.y, mouseCoords.y);
            int yMax = Mathf.Max(dragStartCoords.y, mouseCoords.y);
            for (int x = xMin; x <= xMax; x++) {
                for (int y = yMin; y <= yMax; y++) {
                    GameObject c = SimplePool.Spawn(cursorPrefab, level.transformer.TileToWorld(x, y), Quaternion.identity);
                    c.transform.SetParent(cursorContainer.transform, true);
                    cursors.Add(c);

                    SpriteRenderer spriteRenderer = c.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = GetCursorSprite(new Vector2Int(x,y));
                    spriteRenderer.sortingOrder = level.floorObserver.SortingOrder(x, y) + 1;
                    spriteRenderer.sortingLayerName = "Floor";
                }
            }

        }

        protected virtual IWorldCommand GetCommand(Vector2Int start, Vector2Int end) {
            if (Input.GetButton("Remove")) {
                return new RemoveFloorWorldCommand(level, start, end);
            }
            else {
                return new BuildFloorWorldCommand(level, start, end);
            }
        }

        public virtual Sprite GetCursorSprite(Vector2Int position) {
            if (level.data.IsFloorInBounds(position) == false)
                return null;

            if (Input.GetButton("Remove"))
                return removeSprite;

            if (level.data.GetFloor(position.x, position.y) != (int)FloorIndex.Empty)
                return null;

            return regularSprite;
        }

        private void DeleteCursors() {
            for (int i = cursors.Count - 1; i >= 0; i--) {
                Destroy(cursors[i]);
            }
            cursors = new List<GameObject>();
        }

        protected override string Name() {
            return "Build Mode";
        }
    }

}