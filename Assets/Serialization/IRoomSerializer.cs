public interface IRoomSerializer
{
    LevelData LoadLevel(string levelName);
    void SaveLevel(LevelData levelData);
}