using UnityEngine;

using com.mortup.iso;

namespace com.mortup.iso.world {

    public struct InmediateWallNeighbors {

        public InmediateWallNeighbors(Level level, Vector3Int wallPosition) {

            int rotatedZ = level.transformer.RotateWall(new Vector3Int(0, 0, wallPosition.z)).z;

            if (level.transformer.GetOrientation() == Transformer.Orientation.NORTH) {
                if (rotatedZ == 0) {
                    Top = level.data.GetWall(wallPosition + new Vector3Int(0,1,0));
                    TopLeft = level.data.GetWall(wallPosition + new Vector3Int(-1, 0, 1));
                    TopRight = level.data.GetWall(wallPosition + new Vector3Int(0, 0, 1));
                    Bottom = level.data.GetWall(wallPosition + new Vector3Int(0, -1, 0));
                    BottomLeft = level.data.GetWall(wallPosition + new Vector3Int(-1, -1, 1));
                    BottomRight = level.data.GetWall(wallPosition + new Vector3Int(0, -1, 1));
                }
                else {
                    Top = level.data.GetWall(wallPosition + new Vector3Int(-1, 0, 0));
                    TopLeft = level.data.GetWall(wallPosition + new Vector3Int(0, 0, -1));
                    TopRight = level.data.GetWall(wallPosition + new Vector3Int(0, 1, -1));
                    Bottom = level.data.GetWall(wallPosition + new Vector3Int(1, 0, 0));
                    BottomLeft = level.data.GetWall(wallPosition + new Vector3Int(1, 0, -1));
                    BottomRight = level.data.GetWall(wallPosition + new Vector3Int(1, 1, -1));
                }
            }
            else if(level.transformer.GetOrientation() == Transformer.Orientation.EAST) {
                if (rotatedZ == 0) {
                    Top = level.data.GetWall(wallPosition + new Vector3Int(1, 0, 0));
                    TopLeft = level.data.GetWall(wallPosition + new Vector3Int(1, 1, -1));
                    TopRight = level.data.GetWall(wallPosition + new Vector3Int(1, 0, -1));
                    Bottom = level.data.GetWall(wallPosition + new Vector3Int(-1, 0, 0));
                    BottomLeft = level.data.GetWall(wallPosition + new Vector3Int(0, 1, -1));
                    BottomRight = level.data.GetWall(wallPosition + new Vector3Int(0, 0, -1));
                }
                else {
                    Top = level.data.GetWall(wallPosition + new Vector3Int(0, 1, 0));
                    TopLeft = level.data.GetWall(wallPosition + new Vector3Int(-1, 0, 1));
                    TopRight = level.data.GetWall(wallPosition + new Vector3Int(0, 0, 1));
                    Bottom = level.data.GetWall(wallPosition + new Vector3Int(0, -1, 0));
                    BottomLeft = level.data.GetWall(wallPosition + new Vector3Int(-1, -1, 1));
                    BottomRight = level.data.GetWall(wallPosition + new Vector3Int(0, -1, 1));
                }
            }
            else if (level.transformer.GetOrientation() == Transformer.Orientation.SOUTH) {
                if (rotatedZ == 0) {
                    Top = level.data.GetWall(wallPosition + new Vector3Int(0, -1, 0));
                    TopLeft = level.data.GetWall(wallPosition + new Vector3Int(0, -1, 1));
                    TopRight = level.data.GetWall(wallPosition + new Vector3Int(-1, -1, 1));
                    Bottom = level.data.GetWall(wallPosition + new Vector3Int(0, 1, 0));
                    BottomLeft = level.data.GetWall(wallPosition + new Vector3Int(0, 0, 1));
                    BottomRight = level.data.GetWall(wallPosition + new Vector3Int(-1, 0, 1));
                }
                else {
                    Top = level.data.GetWall(wallPosition + new Vector3Int(1, 0, 0));
                    TopLeft = level.data.GetWall(wallPosition + new Vector3Int(1, 1, -1));
                    TopRight = level.data.GetWall(wallPosition + new Vector3Int(1, 0, -1));
                    Bottom = level.data.GetWall(wallPosition + new Vector3Int(-1, 0, 0));
                    BottomLeft = level.data.GetWall(wallPosition + new Vector3Int(0, 1, -1));
                    BottomRight = level.data.GetWall(wallPosition + new Vector3Int(0, 0, -1));
                }
            }
            else {
                if (rotatedZ == 0) {
                    Top = level.data.GetWall(wallPosition + new Vector3Int(-1, 0, 0));
                    TopLeft = level.data.GetWall(wallPosition + new Vector3Int(0, 0, -1));
                    TopRight = level.data.GetWall(wallPosition + new Vector3Int(0, 1, -1));
                    Bottom = level.data.GetWall(wallPosition + new Vector3Int(1, 0, 0));
                    BottomLeft = level.data.GetWall(wallPosition + new Vector3Int(1, 0, -1));
                    BottomRight = level.data.GetWall(wallPosition + new Vector3Int(1, 1, -1));
                }
                else {
                    Top = level.data.GetWall(wallPosition + new Vector3Int(0, -1, 0));
                    TopLeft = level.data.GetWall(wallPosition + new Vector3Int(0, -1, 1));
                    TopRight = level.data.GetWall(wallPosition + new Vector3Int(-1, -1, 1));
                    Bottom = level.data.GetWall(wallPosition + new Vector3Int(0, 1, 0));
                    BottomLeft = level.data.GetWall(wallPosition + new Vector3Int(0, 0, 1));
                    BottomRight = level.data.GetWall(wallPosition + new Vector3Int(-1, 0, 1));
                }
            }

        }

        public int Top { get; }
        public int TopLeft { get; private set; }
        public int TopRight { get; private set; }
        public int Bottom { get; }
        public int BottomLeft { get; private set; }
        public int BottomRight { get; private set; }

        public void FlipSideNeighbors() {
            int buff = TopLeft;
            TopLeft = TopRight;
            TopRight = buff;

            buff = BottomLeft;
            BottomLeft = BottomRight;
            BottomRight = buff;
        }
    }

}