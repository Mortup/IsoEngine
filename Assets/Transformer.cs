using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer
{
    private Transform levelTransform;

    public Transformer(Transform levelTransform) {
        this.levelTransform = levelTransform;
    }

    public Vector2 TileToLocal(int tileX, int tileY) {
        float x = (tileX + tileY) * 0.5f;
        float y = (tileY - tileX) * 0.25f;
        return new Vector2(x, y);
    }

    public Vector2 TileToLocal(Vector2Int tileCoords) {
        return TileToLocal(tileCoords.x, tileCoords.y);
    }

    public Vector2 TileToWorld(int tileX, int tileY) {
        Vector2 local = TileToLocal(tileX, tileY);
        return levelTransform.TransformPoint(local);
    }

    public Vector2 TileToWorld(Vector2Int tileCoords) {
        return TileToWorld(tileCoords.x, tileCoords.y);
    }

    public Vector2Int ScreenToTile(Vector2 screenCoords) {
        Vector2 world = Camera.main.ScreenToWorldPoint(screenCoords);
        return WorldToTile(world);
    }

    public Vector2Int WorldToTile(Vector2 world) {
        Vector2 local = levelTransform.InverseTransformPoint(world);
        return LocalToTile(local);
    }

    public Vector2Int LocalToTile(Vector2 local) {
        local += new Vector2(-0.5f, 0f);

        int x = Mathf.RoundToInt(local.x - 2 * local.y);
        int y = Mathf.RoundToInt(local.x + 2 * local.y);

        return new Vector2Int(x, y);
    }
}
