using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

using com.mortup.iso;
using com.mortup.iso.world;
using com.mortup.iso.world.commands;

namespace com.mortup.city.gamemodes {

    public abstract class DraggableTileMode : GameMode {

        [SerializeField] protected Level level;
        [SerializeField] protected GameObject cursorPrefab;

        protected GameObject cursorContainer;

        protected Vector2Int dragStartCoords;
        protected bool isDragging;

        protected List<GameObject> cursors;
        protected Stack<IWorldCommand> commandStack;

        public override void Activate() {
            base.Activate();

            cursorContainer = new GameObject(Name() + " Cursors");
            isDragging = false;
            cursors = new List<GameObject>();
            commandStack = new Stack<IWorldCommand>();
        }

        public override void Deactivate() {
            base.Deactivate();

            Destroy(cursorContainer);
            //TODO: Delete pool
        }

        protected void Update() {
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
                    spriteRenderer.sprite = GetCursorSprite(new Vector2Int(x, y));
                    spriteRenderer.sortingOrder = level.transformer.TileSortingOrder(x, y) + 1;
                    spriteRenderer.sortingLayerName = "Floor";
                }
            }

            if (Input.GetButtonDown("Undo")) {
                if (commandStack.Count > 0) {
                    commandStack.Pop().Excecute();
                }
            }
        }

        protected abstract IWorldCommand GetCommand(Vector2Int start, Vector2Int end);
        public abstract Sprite GetCursorSprite(Vector2Int position);

        protected void DeleteCursors() {
            for (int i = cursors.Count - 1; i >= 0; i--) {
                Destroy(cursors[i]);
            }
            cursors = new List<GameObject>();
        }

    }

}