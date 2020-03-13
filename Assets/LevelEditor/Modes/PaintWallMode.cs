using System.Collections.Generic;

using UnityEngine;

using com.mortup.iso;
using com.mortup.iso.resources;
using com.mortup.iso.world;
using com.mortup.iso.world.commands;

namespace com.mortup.city.gamemodes {

    public class PaintWallMode : GameMode {

        [SerializeField] Level level;

        private int selectedIndex = 2;

        private Sprite cursorSprite;
        private GameObject cursor;
        private SpriteRenderer cursorSr;

        private Stack<IWorldCommand> commandStack;

        public override void Activate() {
            base.Activate();

            cursor = new GameObject("Wall Cursor");
            cursorSr = cursor.AddComponent<SpriteRenderer>();
            cursorSr.sortingLayerName = "Wall";

            commandStack = new Stack<IWorldCommand>();
        }

        public override void Deactivate() {
            base.Deactivate();

            Destroy(cursor);
        }

        private void Update() {
            HandleUndo();            

            int currentIndex = Input.GetButton("Remove") ? (int)WallIndex.New : selectedIndex;

            Vector3Int wallCoords = level.transformer.ScreenToWall(Input.mousePosition);
            Vector3 position = level.transformer.WallToWorld(wallCoords);
            position.z = cursor.transform.position.z;
            cursor.transform.position = position;

            cursorSprite = ResourceManager.GetWallSprite(currentIndex, level.transformer.RotateWallInsideTile(wallCoords).z);
            cursorSr.sprite = cursorSprite;

            HandleClick(wallCoords, currentIndex);
        }

        private void HandleClick(Vector3Int wallCoords, int currentIndex) {
            if (Input.GetMouseButton(0)) {
                if (level.data.GetWall(wallCoords) == (int)WallIndex.Empty) {
                    return;
                }

                IWorldCommand command = new PaintWallCommand(level, wallCoords, currentIndex);
                IWorldCommand inverse = command.Excecute();

                if (!(inverse is NullCommand)) {
                    commandStack.Push(inverse);
                }
            }
        }

        private void HandleUndo() {
            if (Input.GetButtonDown("Undo") == false)
                return;

            if (commandStack != null && commandStack.Count > 0) {
                commandStack.Pop().Excecute();
            }
        }

        protected override string Name() {
            return "Paint Wall Mode";
        }

    }

}