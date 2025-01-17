﻿using System.Collections.Generic;

using UnityEngine;

using com.mortup.iso.world;

namespace com.mortup.iso.resources {

    public static class CustomWallCreator {

        private const int cropLength = 90;
        private const int uncroppableLength = 20;

        private static Vector2Int FrontTop = new Vector2Int(4, 111);
        private static Vector2Int FrontBottom = new Vector2Int(4, 0);
        private static Vector2Int LeftTop = new Vector2Int(FrontTop.x - 4, FrontTop.y + 2);
        private static Vector2Int LeftBottom = new Vector2Int(FrontBottom.x - 4, FrontBottom.y + 2);
        private static Vector2Int RightTop = new Vector2Int(37, 127);
        private static Vector2Int RightBottom = new Vector2Int(37, 16);
        private static Vector2Int BackTop = new Vector2Int(33, 129);

        private static Dictionary<string, Sprite> cachedSprites;

        public static Sprite DrawSpriteBorders(Sprite spr, int rotatedZ, InmediateWallNeighbors neighbors, bool isCropped) {

            string cacheHash = neighbors.GetHashForCaching() + spr.name + rotatedZ.ToString() + (isCropped ? "0" : "1");

            if (cachedSprites == null) {
                cachedSprites = new Dictionary<string, Sprite>();
            }

            if (cachedSprites.ContainsKey(cacheHash)) {
                return cachedSprites[cacheHash];
            }
            else {
                Sprite result = DrawSpriteBordersImpl(spr, rotatedZ, neighbors, isCropped);
                cachedSprites.Add(cacheHash, result);
                return result;
            }

        }

        public static Sprite DrawSpriteBordersImpl(Sprite spr, int rotatedZ, InmediateWallNeighbors neighbors, bool isCropped) {
                Texture2D tex = new Texture2D(spr.texture.width, spr.texture.height, spr.texture.format, false, false);
            tex.filterMode = FilterMode.Point;
            tex.wrapMode = TextureWrapMode.Clamp;

            Graphics.CopyTexture(spr.texture, tex);

            if (Settings.DrawWallBorders) {
                DrawBorders(tex, rotatedZ, neighbors);
            }
            else {
                CutCorners(tex, rotatedZ, neighbors);
            }

            if (isCropped)
                Crop(tex, cropLength, rotatedZ);

            tex.Apply();

            Sprite end = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Settings.WallPivot, Settings.PPU);

            return end;
        }

        private static void CutCorners(Texture2D tex, int z, InmediateWallNeighbors neighbors) {
            // Delete border color pixel
            tex.SetPixel(0, 0, Color.clear);

            if (z == 1) {
                FlipTex(tex);
                neighbors.FlipSideNeighbors();
            }

            if (neighbors.TopLeft != (int)FloorIndex.Empty) {
                DrawIsoLine(tex, Color.clear, BackTop, new Vector2Int(BackTop.x - 1, BackTop.y));

                if (neighbors.Top == (int)FloorIndex.Empty && neighbors.TopRight == (int)FloorIndex.Empty) {
                    Color topColor = tex.GetPixel(RightTop.x - 2, RightTop.y - 1);
                    DrawIsoLine(tex, topColor, new Vector2Int(RightTop.x, RightTop.y + 1), new Vector2Int(RightTop.x - 1, RightTop.y + 1));
                }
            }

            if (neighbors.TopRight != (int)FloorIndex.Empty) {
                DrawVerticalLine(tex, Color.clear, RightTop, RightBottom);
                DrawVerticalLine(tex, Color.clear, new Vector2Int(RightTop.x - 1, RightTop.y), new Vector2Int(RightBottom.x - 1, RightBottom.y));
                DrawVerticalLine(tex, Color.clear, new Vector2Int(RightTop.x - 2, RightTop.y + 1), new Vector2Int(RightBottom.x - 2, RightBottom.y - 1));

                Color topColor = tex.GetPixel(RightTop.x - 3, RightTop.y + 1);
                DrawVerticalLine(tex, topColor, new Vector2Int(RightTop.x - 3, RightTop.y + 2), new Vector2Int(RightTop.x - 3, RightTop.y + 2));
            }

            if (neighbors.BottomLeft != (int)FloorIndex.Empty) {
                DrawVerticalLine(tex, Color.clear, LeftTop, LeftBottom);
                DrawVerticalLine(tex, Color.clear, new Vector2Int(LeftTop.x + 1, LeftTop.y), new Vector2Int(LeftBottom.x + 1, LeftBottom.y));
                DrawVerticalLine(tex, Color.clear, new Vector2Int(LeftTop.x + 2, LeftTop.y + 1), new Vector2Int(LeftBottom.x + 2, LeftBottom.y - 1));

                Color topColor = tex.GetPixel(LeftTop.x + 4, LeftTop.y - 2);
                DrawIsoLine(tex, topColor, new Vector2Int(LeftTop.x + 3, LeftTop.y - 2), new Vector2Int(LeftTop.x + 3, LeftTop.y - 2));

                for (int yi = LeftBottom.y - 3; yi <= LeftTop.y - 3; yi++) {
                    DrawIsoLine(tex, tex.GetPixel(LeftTop.x + 4, yi), new Vector2Int(LeftTop.x + 3, yi), new Vector2Int(LeftTop.x + 3, yi));
                }
            }

            if (z == 1) {
                FlipTex(tex);
                neighbors.FlipSideNeighbors();
            }
        }

        private static void DrawBorders(Texture2D tex, int z, InmediateWallNeighbors neighbors) {
            Color borderColor = tex.GetPixel(0, 0);
            tex.SetPixel(0, 0, Color.clear);

            if (z == 1) {
                FlipTex(tex);
                neighbors.FlipSideNeighbors();
            }

            DrawIsoLine(tex, borderColor, FrontBottom, RightBottom);
            DrawIsoLine(tex, borderColor, FrontTop, RightTop);
            DrawIsoLine(tex, borderColor, new Vector2Int(LeftTop.x + 2, LeftTop.y + 1), new Vector2Int(BackTop.x - 2, BackTop.y - 1));

            if (neighbors.Bottom == (int)FloorIndex.Empty) {

                if (neighbors.BottomLeft == (int)FloorIndex.Empty) {
                    DrawVerticalLine(tex, borderColor, LeftBottom, LeftTop);
                    DrawVerticalLine(tex, borderColor, FrontBottom, FrontTop);

                    if (neighbors.BottomRight == (int)FloorIndex.Empty) {
                        DrawIsoLine(tex, borderColor, new Vector2Int(FrontTop.x + 1, FrontTop.y), LeftTop);
                        DrawIsoLine(tex, borderColor, new Vector2Int(FrontBottom.x + 1, FrontBottom.y), LeftBottom);
                    }
                }
            }

            if (neighbors.BottomLeft == (int)FloorIndex.Empty && neighbors.BottomRight != (int)FloorIndex.Empty) {
                DrawIsoLine(tex, borderColor, LeftTop, new Vector2Int(LeftTop.x + 1, LeftTop.y));
            }

            if (neighbors.Top == (int)FloorIndex.Empty) {
                DrawVerticalLine(tex, borderColor, RightBottom, RightTop);
            }

            if (neighbors.TopLeft == (int)FloorIndex.Empty) {
                DrawIsoLine(tex, borderColor, BackTop, new Vector2Int(BackTop.x - 1, BackTop.y));
            }
            else {
                DrawIsoLine(tex, Color.clear, new Vector2Int(BackTop.x - 1, BackTop.y), BackTop);
            }

            if (neighbors.Top == (int)FloorIndex.Empty && neighbors.TopLeft == (int)FloorIndex.Empty && neighbors.TopRight == (int)FloorIndex.Empty) {
                DrawIsoLine(tex, borderColor, new Vector2Int(BackTop.x + 1, BackTop.y - 1), new Vector2Int(BackTop.x + 2, BackTop.y - 1));
            }

            if (neighbors.TopRight != (int)FloorIndex.Empty && z == 0) {
                DrawVerticalLine(tex, Color.clear, new Vector2Int(tex.width - 1, 0), new Vector2Int(tex.width - 1, tex.height));
                DrawVerticalLine(tex, Color.clear, new Vector2Int(tex.width - 2, 0), new Vector2Int(tex.width - 2, tex.height));
                DrawVerticalLine(tex, Color.clear, new Vector2Int(tex.width - 3, 0), new Vector2Int(tex.width - 3, tex.height));
                DrawVerticalLine(tex, borderColor, new Vector2Int(RightTop.x - 3, RightTop.y - 2), new Vector2Int(RightBottom.x - 3, RightBottom.y));

                if (neighbors.Top == (int)FloorIndex.Empty && neighbors.TopLeft == (int)FloorIndex.Empty) {
                    DrawIsoLine(tex, borderColor, new Vector2Int(BackTop.x + 1, BackTop.y), new Vector2Int(BackTop.x + 2, BackTop.y));
                }
            }

            if (neighbors.TopLeft != (int)FloorIndex.Empty && neighbors.Top == (int)FloorIndex.Empty && neighbors.TopRight == (int)FloorIndex.Empty) {
                tex.SetPixel(RightTop.x - 1, RightTop.y + 1, tex.GetPixel(RightTop.x - 2, RightTop.y + 1));
            }

            if (neighbors.BottomLeft != (int)FloorIndex.Empty) {
                DrawVerticalLine(tex, Color.clear, new Vector2Int(0, 0), new Vector2Int(0, tex.height));
                DrawVerticalLine(tex, Color.clear, new Vector2Int(1, 0), new Vector2Int(1, tex.height));
                DrawVerticalLine(tex, Color.clear, new Vector2Int(2, 0), new Vector2Int(2, tex.height));

                if (neighbors.Bottom == (int)FloorIndex.Empty && neighbors.BottomRight == (int)FloorIndex.Empty) {
                    int x = FrontTop.x - 1;
                    tex.SetPixel(x, FrontTop.y, tex.GetPixel(x, FrontTop.y + 1));

                    if (z == 0) {
                        DrawVerticalLine(tex, borderColor, new Vector2Int(x, FrontTop.y - 1), new Vector2Int(x, FrontBottom.y));
                    }
                    else {
                        for (int y = FrontBottom.y + 1; y < FrontTop.y; y++) {
                            tex.SetPixel(x, y, tex.GetPixel(x + 1, y + 1));
                        }
                        tex.SetPixel(x, FrontBottom.y, borderColor);
                    }
                }
            }

            if (z == 1) {
                FlipTex(tex);
                neighbors.FlipSideNeighbors();
            }
        }

        private static void Crop(Texture2D tex, int length, int z) {
            if (z == 1)
                FlipTex(tex);

            for (int x = 0; x < tex.width; x++) {
                for (int y = (x - 4) / 2; y < length; y++) {
                    int sourceY = tex.height - uncroppableLength + Mathf.Max(y, 0);
                    int destinationY = sourceY - length;

                    Color sourceColor = (sourceY >= tex.height) ? Color.clear : tex.GetPixel(x, sourceY);
                    tex.SetPixel(x, destinationY, sourceColor);
                }

                for (int y = tex.height - uncroppableLength; y < tex.height; y++) {
                    tex.SetPixel(x, y, Color.clear);
                }
            }

            if (z == 1)
                FlipTex(tex);
        }

        private static void DrawIsoLine(Texture2D srcTex, Color color, Vector2Int start, Vector2Int end) {
            int x = start.x;
            int y = start.y;

            for (int i = 0; i <= Mathf.Abs(start.x - end.x); i++) {
                srcTex.SetPixel(x, Mathf.FloorToInt(y), color);

                if (start.x < end.x)
                    x++;
                else
                    x--;

                if (i % 2 == 1) {
                    if (start.y < end.y)
                        y++;
                    else
                        y--;
                }
            }

            if (y > Mathf.Max(start.y, end.y) + 1)
                Debug.LogError("Trying to draw non-isometric line");
        }

        private static void DrawVerticalLine(Texture2D srcTex, Color color, Vector2Int start, Vector2Int end) {
            if (start.x == end.x) {
                for (int y = Mathf.Min(start.y, end.y); y <= Mathf.Max(start.y, end.y); y++) {
                    srcTex.SetPixel(start.x, y, color);
                }
            }
            else {
                Debug.LogError("Trying to draw non-vertical line");
            }
        }

        private static void FlipTex(Texture2D tex) {
            for (int y = 0; y < tex.height; y++) {
                for (int x = 0; x < tex.width / 2; x++) {
                    Color buffer = tex.GetPixel(x, y);
                    tex.SetPixel(x, y, tex.GetPixel(tex.width - 1 - x, y));
                    tex.SetPixel(tex.width - 1 - x, y, buffer);
                }
            }
        }

    }
}