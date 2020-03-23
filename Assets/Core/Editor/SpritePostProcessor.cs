using UnityEngine;
using UnityEditor;

public class SpritePostProcessor : AssetPostprocessor
{
    private int PPU = Settings.PPU;
    private Vector2 tilePivot = Settings.TilePivot;
    public Vector2 wallPivot = Settings.WallPivot;
    public Vector2 itemPivot = Settings.ItemPivot;

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
        if (assetPath.Contains("Item")) {
            tis.spritePivot = itemPivot;
        }

        textureImporter.SetTextureSettings(tis);

        UnityEditor.TextureCompressionQuality tcq = new UnityEditor.TextureCompressionQuality();

	}

}