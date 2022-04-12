using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WLA.GameComponents.Sprites;

namespace WLA.Utilities
{
    static class SpriteUtilities
    {
        public static Texture2D[] CutSheet(GraphicsDevice graphicsDevice, Spritesheet sheet)
        {
            Texture2D[] sprites = new Texture2D[sheet.Count];

            Color[] data = new Color[sheet.Width * sheet.Height];
            sheet.SpritesheetTexture.GetData(data);

            int nextLine = sheet.Width;
            int nextTile = sheet.TileWidth + (int)sheet.Spacing.X;
            int nextRow = nextLine * (sheet.TileHeight + (int)sheet.Spacing.Y);

            for (int i = 0; i < sheet.Count; ++i)
            {
                Color[] spriteData = new Color[sheet.TileWidth * sheet.TileHeight];

                int x = i % sheet.Columns;
                int y = i / sheet.Columns;

                for (int j = 0; j < sheet.TileHeight; ++j)
                {
                    for (int k = 0; k < sheet.TileWidth; ++k)
                    {
                        try
                        {
                            spriteData[sheet.TileWidth * j + k] = data[nextRow * y + nextTile * x + nextLine * ((int)sheet.StartPosition.Y + j) + ((int)sheet.StartPosition.X + k)];
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            Debug.Print("{0}, {1}, {2}", i, j, k);
                            Debug.Print("{0}, {1}", spriteData.Count(), data.Count());
                            Debug.Print("{0}, {1}", sheet.TileWidth * j + k, nextRow * y + nextTile * x + nextLine * ((int)sheet.StartPosition.Y + j) + (k + (int)sheet.StartPosition.X));
                            throw e;
                        }
                    }
                }

                Texture2D sprite = new Texture2D(graphicsDevice, sheet.TileWidth, sheet.TileHeight);
                sprite.SetData(spriteData);

                sprites[i] = sprite;
            }

            return sprites;
        }
    }
}
