using UnityEngine;
using UnityEditor;

public class SpritePostProcessor : AssetPostprocessor
{
    private int PPU = 64;
    private Vector2 tilePivot = new Vector2(0f, 26f/42f);
    private Vector2 wallPivot = new Vector2(4 / 38f, 1 / 130f);

	void OnPreprocessTexture ()
	{
		TextureImporter textureImporter  = (TextureImporter) assetImporter;

		TextureImporterSettings tis = new TextureImporterSettings ();
		textureImporter.ReadTextureSettings (tis);

		tis.spritePixelsPerUnit = PPU;
		tis.filterMode = FilterMode.Point;
        tis.mipmapEnabled = false;
        tis.spriteMode = (int)SpriteImportMode.Single;
        tis.wrapMode = TextureWrapMode.Clamp;
        tis.spriteAlignment = (int)SpriteAlignment.Custom;

        if (assetPath.Contains("Floor") || assetPath.Contains("Tile")) {
            tis.spritePivot = tilePivot;
        }
        if (assetPath.Contains("Wall")) {
            tis.spritePivot = wallPivot;
        }

        textureImporter.SetTextureSettings (tis);
	}

}