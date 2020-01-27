using UnityEngine;
using UnityEditor;

public class SpritePostProcessor : AssetPostprocessor
{
    private int PPU = 64;
    private Vector2 tilePivot = new Vector2(0f, 42f/68f);

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
        tis.spritePivot = tilePivot;


        textureImporter.SetTextureSettings (tis);
	}

}