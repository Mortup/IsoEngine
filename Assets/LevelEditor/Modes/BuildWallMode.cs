using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

using com.mortup.iso;
using com.mortup.iso.world.commands;
using com.mortup.iso.world;

namespace com.mortup.city.gamemodes {

    public class BuildWallMode : GameMode {

        [SerializeField] Level level;
        [SerializeField] protected GameObject cursorPrefab;

        private Sprite cursorSprite;
        private GameObject mainCursor;
        private SpriteRenderer cursorSr;

        private Vector2Int startDragCoords = Vector2Int.zero;
        private bool isDragging = false;

        protected GameObject cursorContainer;
        protected List<GameObject> cursors;
        protected Stack<IWorldCommand> commandStack;

        public override void Activate() {
            base.Activate();

            cursors = new List<GameObject>();
            cursorContainer = new GameObject(Name() + " Cursors");
            mainCursor = new GameObject("Wall Cursor");
            cursorSr = mainCursor.AddComponent<SpriteRenderer>();
            cursorSr.sortingLayerName = "Wall";
            cursorSprite = Resources.Load<Sprite>("Sprites/Cursors/BuildVertex");
            commandStack = new Stack<IWorldCommand>();

            isDragging = false;
        }

        public override void Deactivate() {
            base.Deactivate();
            DestroyCursors();
            Destroy(mainCursor);
        }

        private void Update() {
            if (Input.GetButtonDown("Undo")) {
                Undo();
            }

            if (Input.GetButton("Remove")) {
                cursorSprite = Resources.Load<Sprite>("Sprites/Cursors/RemoveVertex");
            }
            else {
                cursorSprite = Resources.Load<Sprite>("Sprites/Cursors/BuildVertex");
            }

            DestroyCursors();

            Vector2Int vertexCoords = level.transformer.ScreenToVertex(Input.mousePosition);
            Vector3 position = level.transformer.VertexToWorld(vertexCoords);
            position.z = mainCursor.transform.position.z;
            mainCursor.transform.position = position;

            cursorSr.sprite = cursorSprite;

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
                isDragging = true;
                startDragCoords = vertexCoords;
            }

            if (Input.GetMouseButton(0) == false && isDragging) {
                isDragging = false;

                Vector2Int closestStraightMouseCoords;
                Vector2Int offset = Vector2Int.zero;
                if (Mathf.Abs(vertexCoords.x - startDragCoords.x) > Mathf.Abs(vertexCoords.y - startDragCoords.y)) {
                    closestStraightMouseCoords = new Vector2Int(vertexCoords.x, startDragCoords.y);
                }
                else {
                    closestStraightMouseCoords = new Vector2Int(startDragCoords.x, vertexCoords.y);
                    offset = new Vector2Int(0, 1);
                }

                if (Input.GetButton("Remove")) {
                    commandStack.Push(new RemoveWallLineCommand(level, startDragCoords + offset, closestStraightMouseCoords + offset).Excecute());
                }
                else {
                    commandStack.Push(new BuildWallLineCommand(level, startDragCoords + offset, closestStraightMouseCoords + offset).Excecute());
                }

            }

            if (isDragging) {
                Vector2Int closestStraightMouseCoords;
                if (Mathf.Abs(vertexCoords.x - startDragCoords.x) > Mathf.Abs(vertexCoords.y - startDragCoords.y)) {
                    closestStraightMouseCoords = new Vector2Int(vertexCoords.x, startDragCoords.y);

                    for (int x = Mathf.Min(vertexCoords.x, startDragCoords.x); x <= Mathf.Max(vertexCoords.x, startDragCoords.x); x++) {
                        GameObject c = SimplePool.Spawn(cursorPrefab, level.transformer.VertexToWorld(x, startDragCoords.y), Quaternion.identity);
                        c.transform.SetParent(cursorContainer.transform, true);
                        cursors.Add(c);
                        c.GetComponent<SpriteRenderer>().sprite = cursorSprite;
                        c.GetComponent<SpriteRenderer>().sortingLayerName = "Wall";
                    }
                }
                else {
                    closestStraightMouseCoords = new Vector2Int(startDragCoords.x, vertexCoords.y);

                    for (int y = Mathf.Min(vertexCoords.y, startDragCoords.y); y <= Mathf.Max(vertexCoords.y, startDragCoords.y); y++) {
                        GameObject c = SimplePool.Spawn(cursorPrefab, level.transformer.VertexToWorld(startDragCoords.x, y), Quaternion.identity);
                        c.transform.SetParent(cursorContainer.transform, true);
                        cursors.Add(c);
                        c.GetComponent<SpriteRenderer>().sprite = cursorSprite;
                        c.GetComponent<SpriteRenderer>().sortingLayerName = "Wall";
                    }
                }
            }
        }

        private void Undo() {
            if (commandStack != null && commandStack.Count > 0) {
                IWorldCommand command = commandStack.Pop();
                command.Excecute();
            }
        }

        private void DestroyCursors() {
            if (cursors == null) {
                return;
            }

            for (int i = cursors.Count - 1; i >= 0; i--) {
                Destroy(cursors[i]);
            }
            cursors = new List<GameObject>();
        }

        protected override string Name() {
            return "Build Wall Mode";
        }

    }

}