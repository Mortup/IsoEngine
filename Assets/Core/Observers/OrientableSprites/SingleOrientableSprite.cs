using UnityEngine;

namespace com.mortup.iso.observers {

    public class SingleOrientableSprite : OrientableSprite {

        public Sprite sprite;

        public override Sprite GetEastSprite() {
            return sprite;
        }

        public override Sprite GetNorthSprite() {
            return sprite;
        }

        public override Sprite GetSouthSprite() {
            return sprite;
        }

        public override Sprite GetWestSprite() {
            return sprite;
        }
    }

}