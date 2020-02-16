using com.mortup.iso.world;

namespace com.mortup.iso.serialization {

    public interface ILevelSerializer {
        LevelData LoadLevel(string levelName);
        void SaveLevel(LevelData levelData);
    }

}