public interface ILevelSerializer
{
    LevelData LoadLevel(string levelName);
    void SaveLevel(LevelData levelData);
}