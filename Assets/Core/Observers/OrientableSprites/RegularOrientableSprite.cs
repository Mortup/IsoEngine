using UnityEngine;

namespace com.mortup.iso.observers {

    public class RegularOrientableSprite : OrientableSprite {

        public Sprite northSprite;
        public Sprite eastSprite;
        public Sprite westSprite;
        public Sprite southSprite;

        public override Sprite GetEastSprite() {
            return eastSprite;
        }

        public override Sprite GetNorthSprite() {
            return northSprite;
        }

        public override Sprite GetSouthSprite() {
            return southSprite;
        }

        public override Sprite GetWestSprite() {
            return westSprite;
        }
    }

}