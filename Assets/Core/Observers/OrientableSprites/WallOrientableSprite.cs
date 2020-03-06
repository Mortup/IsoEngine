using UnityEngine;

using com.mortup.iso.world;
using com.mortup.iso.resources;

namespace com.mortup.iso.observers {

    public class WallOrientableSprite : OrientableSprite {
        public Sprite northSouthSprite;
        public Sprite westEastSprite;
        public bool invertedSide = false;

        private IsometricTransform isoTransform;
        private bool isCropped = true;

        protected override void Awake() {
            base.Awake();

            isoTransform = GetComponent<IsometricTransform>();
            if (isoTransform == null) {
                Debug.LogError("Wall Orientable Sprites need an IsometricTransform to find it's neighbors");
            }
        }

        public override Sprite GetEastSprite() {
            InmediateWallNeighbors neighbors = new InmediateWallNeighbors(GetLevel(), isoTransform.coords);
            Sprite vanilaSprite = invertedSide ? westEastSprite : northSouthSprite;
            int rotatedZ = GetLevel().transformer.RotateWall(isoTransform.coords).z;
            return CustomWallCreator.DrawSpriteBorders(vanilaSprite, rotatedZ, neighbors, isCropped);
        }

        public override Sprite GetNorthSprite() {
            InmediateWallNeighbors neighbors = new InmediateWallNeighbors(GetLevel(), isoTransform.coords);
            Sprite vanilaSprite = invertedSide ? northSouthSprite : westEastSprite;
            int rotatedZ = GetLevel().transformer.RotateWall(isoTransform.coords).z;
            return CustomWallCreator.DrawSpriteBorders(vanilaSprite, rotatedZ, neighbors, isCropped);
        }

        public override Sprite GetSouthSprite() {
            return GetNorthSprite();
        }

        public override Sprite GetWestSprite() {
            return GetEastSprite();
        }
    }

}