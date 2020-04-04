using com.mortup.iso.world;

namespace com.mortup.iso.serialization {

    public interface ILevelSerializer {
        ILevelData LoadLevel(string levelName);
        void SaveLevel(ILevelData levelData);
    }

}