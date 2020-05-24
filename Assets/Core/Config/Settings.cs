using UnityEngine;

public static class Settings
{
    public const int PPU = 64;
    public static Vector2 TilePivot = new Vector2(0f, 26f / 42f);
    public static Vector2 WallPivot = new Vector2(2 / 38f, 2 / 130f);
    public static Vector2 ItemPivot = new Vector2(0f, 0.1307692f);

    public static bool DrawWallBorders = true;

    public static string basePath = "Diablo";

}
