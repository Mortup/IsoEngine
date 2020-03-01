using UnityEngine;

namespace com.mortup.iso.observers {

    public class BiOrientableSprite : OrientableSprite {

        public Sprite northSouthSprite;
        public Sprite westEastSprite;
        public bool invertedSide = false;

        public override Sprite GetEastSprite() {
            return invertedSide ? westEastSprite : northSouthSprite;
        }

        public override Sprite GetNorthSprite() {
            return invertedSide ? northSouthSprite : westEastSprite;
        }

        public override Sprite GetSouthSprite() {
            return invertedSide ? northSouthSprite : westEastSprite;
        }

        public override Sprite GetWestSprite() {
            return invertedSide ? westEastSprite : northSouthSprite;
        }
    }
}