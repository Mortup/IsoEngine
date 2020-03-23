using UnityEngine;

namespace com.mortup.iso.observers {

    public class RegularOrientableSprite : OrientableSprite {

        public Sprite northSprite;
        public Sprite eastSprite;
        public Sprite westSprite;
        public Sprite southSprite;

        public Transformer.Orientation localOrientation;

        public override Sprite GetEastSprite() {
            return GetSprite((int)Transformer.Orientation.EAST + (int)localOrientation);
        }

        public override Sprite GetNorthSprite() {
            return GetSprite((int)Transformer.Orientation.NORTH + (int)localOrientation);
        }

        public override Sprite GetSouthSprite() {
            return GetSprite((int)Transformer.Orientation.SOUTH + (int)localOrientation);
        }

        public override Sprite GetWestSprite() {
            return GetSprite((int)Transformer.Orientation.WEST + (int)localOrientation);
        }

        private Sprite GetSprite(int orientation) {
            orientation = orientation % 4;
            switch (orientation) {
                case (int)Transformer.Orientation.NORTH:
                    return northSprite;
                case (int)Transformer.Orientation.WEST:
                    return westSprite;
                case (int)Transformer.Orientation.SOUTH:
                    return southSprite;
                case (int)Transformer.Orientation.EAST:
                    return eastSprite;
                default:
                    Debug.LogError("Searching for an unkwnown sprite orientation.");
                    return northSprite;
            }
        }
    }

}